package agent

import (
	"flag"
	"kwikdeploy/agent/commands"
	"log"
	"net/url"
	"os"
	"os/signal"

	"github.com/gorilla/websocket"
)

var server = flag.String("server", "", "KwikDeploy Server Address")
var key = flag.String("key", "", "Agent Key")

func Main() {
	parseArgs()

	interrupt := make(chan os.Signal, 1)
	signal.Notify(interrupt, os.Interrupt)

	c := open()
	defer c.Close()

	done := make(chan struct{})
	go func() {
		defer close(done)
		listen(c)
	}()

	for {
		select {
		case <-done:
			return
		case <-interrupt:
			err := c.WriteMessage(websocket.CloseMessage, websocket.FormatCloseMessage(websocket.CloseNormalClosure, ""))
			if err != nil {
				log.Println("write close:", err)
				return
			}
			select {
			case <-done:
				return
			}
		}
	}
}

func parseArgs() {
	flag.Parse()
	log.SetFlags(0)

	if(*server == "" || *key == "") {
		flag.Usage()
	}
}

func open() *websocket.Conn {
	u := url.URL{Scheme: "ws", Host: *server, Path: "/ws"}
	log.Printf("Connecting to %s", u.String())

	conn, _, err := websocket.DefaultDialer.Dial(u.String(), nil)
	if err != nil {
		log.Fatal(err)
	}

	return conn
}

func listen(c *websocket.Conn)  {
	commands.Init(c, *key)

	commandChan := make(chan string, 100)
	// TODO: Multiple workers
	// https://www.opsdash.com/blog/job-queues-in-go.html
	go worker(commandChan)

	for {
		_, message, err := c.ReadMessage()
			if err != nil {
				log.Println("read:", err)
				return
			}
			log.Printf("recv: %s", message)
			commandChan <- string(message)
	}
}

func worker(commandChan <-chan string) {
	for command := range commandChan {
        commands.ProcessCommand(command)
    }
}
