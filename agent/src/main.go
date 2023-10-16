package main

import (
	"encoding/json"
	"flag"
	"fmt"
	"log"
	"net/url"
	"os"
	"os/signal"
	"strings"

	"github.com/gorilla/websocket"
)

var server = flag.String("server", "", "KwikDeploy Server Address")
var key = flag.String("key", "", "Agent Key")

type RegisterAgent struct {
	Key string `json:"key"`
  }

type DeployCommand struct {
	Id int `json:"id"`
  }

func main() {
	flag.Parse()
	log.SetFlags(0)

	if(*server == "" || *key == "") {
		flag.Usage()
	}

	interrupt := make(chan os.Signal, 1)
	signal.Notify(interrupt, os.Interrupt)

	u := url.URL{Scheme: "ws", Host: *server, Path: "/ws"}
	log.Printf("Connecting to %s", u.String())

	c, _, err := websocket.DefaultDialer.Dial(u.String(), nil)
	if err != nil {
		log.Fatal("dial:", err)
	}
	defer c.Close()

	done := make(chan struct{})

	// Register agent with server
	log.Println("Registering agent with server")
	r := RegisterAgent{Key: *key}
	registerMessage, _ := json.Marshal(r)
	c.WriteMessage(websocket.TextMessage, []byte(fmt.Sprintf("register%s", registerMessage)))

	log.Println("Waiting for server response")
	_, message, err := c.ReadMessage()
	if err != nil {
		log.Println(string(message))
	}

	// Listen for commands
	log.Println("Listening for commands from server...")
	go func() {
		defer close(done)
		for {
			_, messageBytes, err := c.ReadMessage()
			if err != nil {
				log.Println("read:", err)
				return
			}
			log.Printf("recv: %s", messageBytes)

			message := string(messageBytes)
			if(strings.HasPrefix(message, "deploy")) {
				jsonstring := message[len("deploy"):]
				var deployCommand DeployCommand
				json.Unmarshal([]byte(jsonstring), &deployCommand)				
				c.WriteMessage(websocket.TextMessage, []byte(fmt.Sprintf("Deployment complete for Id %d", deployCommand.Id)))
			}
		}
	}()

	for {
		select {
		case <-done:
			return
		
		case <-interrupt:
			log.Println("interrupt")

			// Cleanly close the connection by sending a close message and then
			// waiting (with timeout) for the server to close the connection.
			err := c.WriteMessage(websocket.CloseMessage, websocket.FormatCloseMessage(websocket.CloseNormalClosure, ""))
			if err != nil {
				log.Println("write close:", err)
				return
			}
			select {
			case <-done:
			}
			return
		}
	}
}