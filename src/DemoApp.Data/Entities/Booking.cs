namespace DemoApp.Data.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid InventoryItemId { get; set; }
        public DateTime TimeStamp { get; set; }

        // Linked Entities
        public virtual Member Member { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
