using AutoMapper;
using DemoApp.Models.CsvFiles;
using DemoApp.Models.Dtos;

namespace DemoApp.Business.MapperConfigs
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, Data.Entities.Member>();
            CreateMap<InventoryItem, Data.Entities.InventoryItem>();
            CreateMap<Data.Entities.Member, MemberDto>();
            CreateMap<Data.Entities.InventoryItem, InventoryItemDto>();
            CreateMap<Data.Entities.Booking, BookingDto>(); 
        }
    }
}
