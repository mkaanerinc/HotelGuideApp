using Application.Features.Hotels.Commands.Create;
using Application.Features.Hotels.Commands.Delete;
using Application.Features.Hotels.Queries.GetHotelDetailById;
using Application.Features.Hotels.Queries.GetManagerList;
using Core.Application.Requests;
using Core.Application.Responses;
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

        [HttpGet("GetListManager")]
        public async Task<IActionResult> GetListManager([FromQuery] PageRequest pageRequest)
        {
            GetListManagerQuery getListManagerQuery = new() { PageRequest = pageRequest};
            GetListResponse<GetListManagerListItemDto> response = await Mediator.Send(getListManagerQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelDetailById([FromRoute] Guid id)
        {
            GetHotelDetailByIdQuery getHotelDetailByIdQuery = new() { Id = id };
            List<GetHotelDetailByIdResponse> response = await Mediator.Send(getHotelDetailByIdQuery);

            return Ok(response);
        }
    }
}
