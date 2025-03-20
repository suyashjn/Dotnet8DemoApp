namespace DemoApp.Models.Requests
{
    public record BookingRequest
    (
        Guid MemberId,
        Guid InventoryItemId
    );
}
