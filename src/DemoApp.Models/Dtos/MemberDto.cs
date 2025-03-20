namespace DemoApp.Models.Dtos
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookingCount { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
