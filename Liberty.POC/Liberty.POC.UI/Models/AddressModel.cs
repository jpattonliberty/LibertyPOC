using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liberty.POC.UI.Models
{
    public class AddressModel
    {
        public string PhysicalAddressStreet { get; set; }
        public string PhysicalAddressSuburb { get; set; }
        public string PhysicalAddressCity { get; set; }
        public string PhysicalAddressProvince { get; set; }
        public string PhysicalAddressCode { get; set; }
    }
}