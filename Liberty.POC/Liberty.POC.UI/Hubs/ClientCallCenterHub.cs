using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Liberty.POC.UI.Hubs
{
    [HubName("clientCallCenterHub")]
    public class ClientCallCenterHub : Hub
    {
        public void Send(string source, string message)
        {
            if (string.Compare(source, "client", StringComparison.CurrentCultureIgnoreCase) == 0)
                Clients.All.callCenterBroadcast(source, message);
            else if (string.Compare(source, "callcenter", StringComparison.CurrentCultureIgnoreCase) == 0)
                Clients.All.clientBroadcast(source, message);
        }
    }
}