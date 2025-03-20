namespace DemoApp.Models.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid InventoryItemId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
