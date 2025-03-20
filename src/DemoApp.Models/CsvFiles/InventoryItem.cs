using CsvHelper.Configuration.Attributes;

namespace DemoApp.Models.CsvFiles
{
    public class InventoryItem
    {
        [Name("title")]
        public string Title { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Name("remaining_count")]
        public int RemaningCount { get; set; }

        [Name("expiration_date")]
        [Format("dd/MM/yyyy")]
        public DateTime ExpirationDate { get; set; }
    }
}
