using CsvHelper.Configuration.Attributes;

namespace DemoApp.Models.CsvFiles
{
    public class Member
    {
        [Name("name")]
        public string Name { get; set; }

        [Name("surname")]
        public string Surname { get; set; }

        [Name("booking_count")]
        public int BookingCount { get; set; }

        [Name("date_joined")]
        public DateTime DateJoined { get; set; }
    }
}
