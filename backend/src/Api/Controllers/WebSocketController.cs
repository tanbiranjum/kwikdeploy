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
            var registerMessageString = await ReadStringMessage(webSocket);
            if (registerMessageString.StartsWith("register"))
            {
                var jsonString = registerMessageString.Substring("register".Length);
                var registerMessage = JsonSerializer.Deserialize<RegisterMessage>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (registerMessage!.Key == "123")
                {
                    await WriteStringMessage(webSocket, "Connection successful");
                    Console.WriteLine("Client connected");
                }
                else
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Invalid key", CancellationToken.None);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
            int i = 100;
            while (true)
            {
                await WriteStringMessage(webSocket, $"deploy{JsonSerializer.Serialize(new DeployCommand { Id = i++ })}");
                var message = await ReadStringMessage(webSocket);
                Console.WriteLine(message);

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            //while (true)
            //{
            //    var message = await ReadStringMessage(webSocket);
            //    Console.WriteLine(message);

            //    if (message == "ping")
            //    {
            //        await WriteStringMessage(webSocket, "pong");
            //    }
            //}

        }
        catch (Exception ex)
        {
            if(ex.Message == "Connection closed")
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

public class RegisterMessage
{
    public string Key { get; set; } = null!;
}

public class DeployCommand
{
    public int Id { get; set; }
}

