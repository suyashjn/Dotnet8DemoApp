using DemoApp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _memberService;

        public MembersController(IMembersService memberService)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }

        [HttpPost]
        [EndpointSummary("Endpoint for uploading csv file contaning members info.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("/uploadmembers")]
        public async Task<IActionResult> UploadMembers(IFormFile file)
        {
            await _memberService.SaveDataFromCsv(file);
            return Ok();
        }

        [HttpGet]
        [EndpointSummary("Endpoint for getting members.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _memberService.GetMembers();
            return Ok(members);
        }
    }
}
