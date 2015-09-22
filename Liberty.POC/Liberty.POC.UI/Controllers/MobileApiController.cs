using System.Collections.Generic;
using System.Web.Http;
using Liberty.POC.UI.Helpers;
using Liberty.POC.UI.Models;

namespace Liberty.POC.UI.Controllers
{
    public class MobileApiController : ApiController
    {
        [HttpGet]
        public List<ClientDetailsModel> Index()
        {
            var allSessions = Helper.GetAllClientDetails();
            return allSessions;
        }

        [HttpGet]
        public ClientDetailsModel Index(long? id)
        {
            var clientDetailsModel = new ClientDetailsModel();

            if (id.HasValue)
            {
                clientDetailsModel = Helper.GetClientDetails(id.Value);
            }

            return clientDetailsModel;
        }

        [HttpGet]
        public ClientDetailsModel Index(string clientName)
        {
            var clientDetailsModel = new ClientDetailsModel();

            if (!string.IsNullOrEmpty(clientName))
            {
                clientDetailsModel = Helper.GetClientDetails(clientName);
            }

            return clientDetailsModel;
        }

        [HttpPost]
        public void Save(ClientDetailsModel clientDetailsModel)
        {
            if (clientDetailsModel == null)
                return;

            if (clientDetailsModel.Id > 0)
                Helper.UpdateSession(clientDetailsModel);
            else
                Helper.InsertSession(clientDetailsModel);
        }

        [HttpGet]
        public bool Authenticate(string clientName, string password)
        {
            return true;
        }

        [HttpGet]
        public ClientDetailsModel GetClientSession(string clientName)
        {
            var clientDetailsModel = new ClientDetailsModel();

            if (!string.IsNullOrEmpty(clientName))
            {
                clientDetailsModel = Helper.GetClientDetails(clientName);
            }

            if (clientDetailsModel.Id == 0)
            {
                clientDetailsModel.Name = clientName;
                Helper.InsertSession(clientDetailsModel);
            }
            return clientDetailsModel;
        }
    }
}