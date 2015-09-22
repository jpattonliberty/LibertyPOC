using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Liberty.POC.UI.Models;

namespace Liberty.POC.UI.Helpers
{
    public static class Helper
    {
        public static void InsertSession(ClientDetailsModel clientDetailsModel)
        {
            var dataModel = new Session
            {
                CurrentStep = clientDetailsModel.CurrentStep,
                ClientName = clientDetailsModel.Name,
                Completed = clientDetailsModel.IsCompleted,
                SessionID = clientDetailsModel.Id,
                Address = Json.Encode(clientDetailsModel.AddressDetails),
                Contact = Json.Encode(clientDetailsModel.ContactDetail),
                Personal = Json.Encode(clientDetailsModel.PersonalDetails)
            };

            using (var ctx = new LibertyPocEntities())
            {
                ctx.Sessions.Add(dataModel);
                ctx.SaveChanges();
            }
            clientDetailsModel.Id = dataModel.SessionID;
        }

        public static void UpdateSession(ClientDetailsModel clientDetailsModel)
        {
            using (var ctx = new LibertyPocEntities())
            {
                var session = ctx.Sessions.FirstOrDefault(s => s.SessionID == clientDetailsModel.Id);

                if (session != null)
                {
                    session.CurrentStep = clientDetailsModel.CurrentStep;
                    session.ClientName = clientDetailsModel.Name;
                    session.Completed = clientDetailsModel.IsCompleted;
                    session.Address = Json.Encode(clientDetailsModel.AddressDetails);
                    session.Contact = Json.Encode(clientDetailsModel.ContactDetail);
                    session.Personal = Json.Encode(clientDetailsModel.PersonalDetails);
                }

                ctx.SaveChanges();
            }
        }

        public static List<ClientDetailsModel> GetAllClientDetails()
        {
            List<ClientDetailsModel> clientDetailsModel;

            using (var ctx = new LibertyPocEntities())
            {
                clientDetailsModel = ctx.Sessions.ToList().Select(s => new ClientDetailsModel
                {
                    Id = s.SessionID,
                    Name = s.ClientName,
                    IsCompleted = s.Completed,
                    CurrentStep = s.CurrentStep,
                    AddressDetails = GetDeserialisedObject<AddressModel>(s.Address),
                    PersonalDetails = GetDeserialisedObject<PersonalModel>(s.Personal),
                    ContactDetail = GetDeserialisedObject<ContactModel>(s.Contact)
                }).ToList();
            }
            return clientDetailsModel;
        }

        public static ClientDetailsModel GetClientDetails(long id)
        {
            Session dataModel;

            using (var ctx = new LibertyPocEntities())
            {
                dataModel = ctx.Sessions.FirstOrDefault(s => s.SessionID == id);
            }

            if (dataModel == null)
                dataModel = GetNullSessionObject();

            var clientDetailsModel = new ClientDetailsModel
            {
                Id = dataModel.SessionID,
                Name = dataModel.ClientName,
                IsCompleted = dataModel.Completed,
                CurrentStep = dataModel.CurrentStep,
                AddressDetails = GetDeserialisedObject<AddressModel>(dataModel.Address),
                PersonalDetails = GetDeserialisedObject<PersonalModel>(dataModel.Personal),
                ContactDetail = GetDeserialisedObject<ContactModel>(dataModel.Contact)
            };

            return clientDetailsModel;
        }

        public static void DeleteAllSessions()
        {
            using (var ctx = new LibertyPocEntities())
            {
                ctx.Sessions.RemoveRange(ctx.Sessions);
                ctx.SaveChanges();
            }
        }

        public static SelectList GetNumberOfDependants()
        {
            return new SelectList(Enumerable.Range(0, 11));
        }

        public static List<SelectListItem> GetProvinces()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Gauteng"
                }, 
                 new SelectListItem
                {
                    Text = "Freestate"
                }, 
                  new SelectListItem
                {
                    Text = "Limpopo"
                }, 
                    new SelectListItem
                {
                    Text = "Mpumalanga"
                }, 
                     new SelectListItem
                {
                    Text = "North West"
                }, 
                     new SelectListItem
                {
                    Text = "KwaZulu-Natal"
                }
            };
        }

        public static T GetDeserialisedObject<T>(string serialisedData) where T : new()
        {
            return !String.IsNullOrEmpty(serialisedData)
                ? Json.Decode<T>(serialisedData)
                : new T();
        }

        public static ClientDetailsModel GetClientDetails(string clientName)
        {
            Session dataModel;

            using (var libertyPocEntities = new LibertyPocEntities())
            {
                dataModel = libertyPocEntities
                                        .Sessions
                                        .Where(n => n.ClientName == clientName && !n.Completed)
                                        .OrderByDescending(n => n.SessionID)
                                        .FirstOrDefault();
            }

            if (dataModel == null)
            {
                dataModel = GetNullSessionObject();
            }

            var clientDetailsModel = new ClientDetailsModel
            {
                Id = dataModel.SessionID,
                Name = dataModel.ClientName,
                IsCompleted = dataModel.Completed,
                CurrentStep = dataModel.CurrentStep,
                AddressDetails = GetDeserialisedObject<AddressModel>(dataModel.Address),
                PersonalDetails = GetDeserialisedObject<PersonalModel>(dataModel.Personal),
                ContactDetail = GetDeserialisedObject<ContactModel>(dataModel.Contact)
            };

            return clientDetailsModel;
        }

        private static Session GetNullSessionObject()
        {
            const string defaultCurrentStep = "Personal";

            return new Session
            {
                CurrentStep = defaultCurrentStep,
                Address = string.Empty,
                SessionID = 0,
                Personal = string.Empty,
                Completed = false,
                Contact = string.Empty,
                ClientName = string.Empty
            };
        }
    }
}