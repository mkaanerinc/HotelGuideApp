using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateReportCommand createReportCommand)
        {
            CreatedReportResponse response = await Mediator.Send(createReportCommand);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListReportQuery getListReportQuery = new() { PageRequest = pageRequest};
            GetListResponse<GetListReportListItemDto> response = await Mediator.Send(getListReportQuery);

            return Ok(response);
        }
    }
}
