using Application.Features.Hotels.Commands.Create;
using Application.Features.Hotels.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateHotelCommand createHotelCommand)
        {
            CreatedHotelResponse response = await Mediator.Send(createHotelCommand);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteHotelCommand deleteHotelCommand)
        {
            DeletedHotelResponse response = await Mediator.Send(deleteHotelCommand);

            return Ok(response);
        }
    }
}
