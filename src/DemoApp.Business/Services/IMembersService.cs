using Microsoft.AspNetCore.Http;
using DemoApp.Models.Dtos;

namespace DemoApp.Business.Services
{
    public interface IMembersService
    {
        Task SaveDataFromCsv(IFormFile file);
        Task<IEnumerable<MemberDto>> GetMembers();
    }
}
