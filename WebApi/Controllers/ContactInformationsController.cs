using Application.Features.ContactInformations.Commands.Create;
using Application.Features.ContactInformations.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateContactInformationCommand createContactInformationCommand)
        {
            CreatedContactInformationResponse response = await Mediator.Send(createContactInformationCommand);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteContactInformationCommand deleteContactInformationCommand)
        {
            DeletedContactInformationResponse response = await Mediator.Send(deleteContactInformationCommand);

            return Ok(response);
        }
    }
}
