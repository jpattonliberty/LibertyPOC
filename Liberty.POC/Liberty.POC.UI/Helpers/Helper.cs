using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Liberty.POC.UI.Helpers
{
    public static class UserInterfacehHelper
    {
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
    }
}