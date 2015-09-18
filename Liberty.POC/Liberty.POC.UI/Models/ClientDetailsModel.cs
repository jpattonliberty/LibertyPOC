namespace Liberty.POC.UI.Models
{
    public class ClientDetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CurrentStep { get; set; }
        public AddressModel AddressDetails { get; set; }
        public ContactModel ContactDetail { get; set; }
        public PersonalModel PersonalDetails { get; set; }
        public bool IsCompleted{ get; set; }
    }
}