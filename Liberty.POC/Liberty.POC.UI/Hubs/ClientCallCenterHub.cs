using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Liberty.POC.UI.Hubs
{
    [HubName("clientCallCenterHub")]
    public class ClientCallCenterHub : Hub
    {
        public void ClientMessage(long id)
        {
            Clients.All.ClientMessage(id);
        }

        public void CallCenterMessage(bool accept)
        {
            Clients.All.CallCenterMessage(accept);
        }
    }
}