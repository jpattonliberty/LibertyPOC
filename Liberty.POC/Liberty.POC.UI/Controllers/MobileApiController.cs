using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace Liberty.POC.UI.Controllers
{
    public class MobileApiController : ApiController
    {
        [HttpGet()]
        public bool Authenticate(string clientName, string password)
        {
            return true;
        }

        [HttpGet()]
        public Session Session(string clientName)
        {
            using (var libertyPocEntities = new LibertyPocEntities())
            {
                var clientSession = libertyPocEntities.Sessions
                    .Where(xm => xm.ClientName.Equals(clientName))
                    .FirstOrDefault();
                if (object.Equals(clientSession, default(Session)))
                {
                    return null;
                }
                return clientSession;
            }
        }
    }
}
