package commands

import (
	"encoding/json"
	"log"

	"github.com/gorilla/websocket"
)

type Request struct {
	MessageId string `json:"messageId"`
	Command   string `json:"command"`
	Body   string `json:"body"`
}

type Response struct {
	MessageId string `json:"messageId"`
	Result string `json:"result"`
	Body string `json:"body"`
}

type DeployRequest struct {
	ReleaseId int    `json:"releaseId"`
	Prop1 string `json:"prop1"`
	Prop2 string `json:"prop2"`
}

type DeployResponse struct {
	ReleaseId int    `json:"releaseId"`
	Status string `json:"status"`
}

var c *websocket.Conn
var k string

func Init(conn *websocket.Conn, key string) {
	c = conn
	k = key
}

func ProcessCommand(commandString string) {
	var request Request
	json.Unmarshal([]byte(commandString), &request)

	switch(request.Command) {
	case "deploy":
		var body DeployRequest
		json.Unmarshal([]byte(request.Body), &body)
		b, _:= json.Marshal(body)
		log.Printf("body: %s", string(b))
		processDeployCommand(request, body)
	case "disconnect":
		processDisconnectCommand(request)
	case "ping":
		processPingCommand(request)
	case "register":
		processRegisterCommand(request)
	default:
		log.Println(request.Command)
	}
}

func sendTextResponse(response Response) {
	responseJson, _ := json.Marshal(response)
	c.WriteMessage(websocket.TextMessage, responseJson)
}

func sendCloseResponse(response Response) {
	responseJson, _ := json.Marshal(response)
	c.WriteMessage(websocket.CloseMessage, websocket.FormatCloseMessage(websocket.CloseNormalClosure, string(responseJson)))
}

func processDeployCommand(request Request, body DeployRequest) {
	deployResponse := DeployResponse{ReleaseId: body.ReleaseId, Status: "success"}
	deployResponseJson, _ := json.Marshal(deployResponse)
	response := Response{request.MessageId, "success", string(deployResponseJson)}
	sendTextResponse(response)
}

func processDisconnectCommand(request Request) {
	response := Response{request.MessageId, "disconnecting", ""}
	sendCloseResponse(response)
	c.Close()
}

func processPingCommand(request Request) {
	response := Response{request.MessageId, "pong", ""}
	sendTextResponse(response)
}

func processRegisterCommand(request Request) {
	response := Response{request.MessageId, k, ""}
	sendTextResponse(response)
}
