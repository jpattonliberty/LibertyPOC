using Microsoft.AspNet.SignalR;

namespace Liberty.POC.UI.Hubs
{
    public class ClientCallCenterHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}