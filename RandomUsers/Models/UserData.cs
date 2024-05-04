using CsvHelper.Configuration.Attributes;

namespace RandomUsers.Models
{
    public class UserData
    {
        [Name("Id")]
        public string Id { get; set; }

        [Name("FullName")]
        public string FullName { get; set; }

        [Name("Address")]
        public string Address { get; set; }

        [Name("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}