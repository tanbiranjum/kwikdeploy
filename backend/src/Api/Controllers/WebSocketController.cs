using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class WebSocketController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await HandleClient(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task HandleClient(WebSocket webSocket)
    {
        try
        {
            Console.WriteLine("Connnection initiated from agent");

            var registerRequest = new Request { MessageId = Guid.NewGuid().ToString(), Command = "register", Body = "" };
            await WriteStringMessage(webSocket, JsonSerializer.Serialize(registerRequest));

            var registerResponseJson = await ReadStringMessage(webSocket);
            Console.WriteLine($"recv: {registerResponseJson}");
            var response = JsonSerializer.Deserialize<Response>(registerResponseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var key = response.Result;
            if (key != "123")
            {
                var disconnectRequest = new Request { MessageId = Guid.NewGuid().ToString(), Command = "disconnect", Body = "" };
                await WriteStringMessage(webSocket, JsonSerializer.Serialize(disconnectRequest));
            }

            int i = 100;
            while (true)
            {
                if (i++ % 2 == 0)
                {
                    var pingRequest = new Request { MessageId = Guid.NewGuid().ToString(), Command = "ping", Body = "" };
                    await WriteStringMessage(webSocket, JsonSerializer.Serialize(pingRequest));

                    var pingResponseJson = await ReadStringMessage(webSocket);
                    var r = JsonSerializer.Deserialize<Response>(pingResponseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    Console.WriteLine($"Received Response: {r.Result}");
                }
                else
                {
                    var deployRequest = new Request
                    {
                        MessageId = Guid.NewGuid().ToString(),
                        Command = "deploy",
                        Body = JsonSerializer.Serialize(new DeployRequest { ReleaseId = i, Prop1 = "aaa", Prop2 = DateTime.Now.ToString() })
                    };
                    Console.WriteLine($"Sending: {deployRequest.Body}");
                    await WriteStringMessage(webSocket, JsonSerializer.Serialize(deployRequest));

                    var deployResponseJson = await ReadStringMessage(webSocket);
                    var r = JsonSerializer.Deserialize<Response>(deployResponseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    var b = JsonSerializer.Deserialize<DeployResponse>(r.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    Console.WriteLine($"Received Response: {r.Result}, Body: {JsonSerializer.Serialize(b)}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Connection closed")
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Server connection closed", CancellationToken.None);
            }
            Console.WriteLine("Connection closed");
        }
    }

    private static async Task<string> ReadStringMessage(WebSocket webSocket)
    {
        using (var ms = new MemoryStream())
        {
            var buffer = WebSocket.CreateServerBuffer(1024);
            WebSocketReceiveResult receiveResult;
            do
            {
                receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                ms.Write(buffer.Array, buffer.Offset, receiveResult.Count);
            } while (!receiveResult.EndOfMessage);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                var messageString = Encoding.UTF8.GetString(ms.ToArray());
                return messageString;
            }
            else if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                throw new Exception("Connection closed");
            }
            else
            {
                throw new Exception("Unsupported message type");
            }
        }
    }

    private static async Task WriteStringMessage(WebSocket webSocket, string message)
    {
        await webSocket.SendAsync(
                        new ArraySegment<byte>(
                            Encoding.UTF8.GetBytes(message)),
                            WebSocketMessageType.Text, true,
                            CancellationToken.None);
    }
}

public class Request
{
    public string MessageId { get; set; } = null!;
    public string Command { get; set; } = null!;
    public string Body { get; set; } = null!;
}

public class Response
{
    public string MessageId { get; set; } = null!;
    public string Result { get; set; } = null!;
    public string Body { get; set; } = null!;
}


public class RegisterMessage
{
    public string Key { get; set; } = null!;
}

public class DeployRequest
{
    public int ReleaseId { get; set; }
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
}

public class DeployResponse
{
    public int ReleaseId { get; set; }
    public string Status { get; set; }
}

