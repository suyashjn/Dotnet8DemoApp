namespace DemoApp.Data.Entities
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemaningCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
