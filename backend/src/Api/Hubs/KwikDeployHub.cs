using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class KwikDeployHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Client connected");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Client disconneted");
    }

    // Agent calls this
    public async Task RegisterAgent(string token)
    {
        if (token != "123")
        {
            await Clients.Caller.SendAsync("RegistrationFailed", "Invalid token");
            Context.Abort();
            Console.WriteLine("Client tried connecting with invalid token. Connection aborted");
        }
        else
        {
            await Clients.Caller.SendAsync("RegistrationSuccessful");
            Console.WriteLine("Client registered");
        }
    }
}
