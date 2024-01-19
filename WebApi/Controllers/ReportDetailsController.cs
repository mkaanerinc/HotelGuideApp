using Application.Features.ReportDetails.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportDetailsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReportDetailQuery getListReportDetailQuery = new() {PageRequest = pageRequest };
        GetListResponse<GetListReportDetailListItemDto> response = await Mediator.Send(getListReportDetailQuery);

        return Ok(response);
    }
}
