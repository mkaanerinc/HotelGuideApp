using Application.Features.Hotels.Commands.Create;
using Application.Features.ReportDetails.Queries.GetList;
using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Queries.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Test.WebAPITests;

public class ReportsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ReportsController _reportsController;

    public ReportsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _reportsController = new()
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = new ServiceCollection()
                    .AddSingleton<IMediator>(_mockMediator.Object)
                    .BuildServiceProvider()
                }
            }
        };
    }

    [Fact]
    public async Task Add_CreateReportCommandIsValid_ReturnsOkResultWithCreatedReportResponse()
    {
        // Arrange

        CreateReportCommand createReportCommand = new()
        {
            Location = "Mersin"
        };

        CreatedReportResponse createdReportResponse = new()
        {
            Id = Guid.NewGuid(),
            ReportStatus = ReportStatus.InProgress,
            CreatedDate = DateTime.UtcNow,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateReportCommand>(), default)).ReturnsAsync(createdReportResponse);

        // Act

        var result = await _reportsController.Add(createReportCommand);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CreatedReportResponse>(okResult.Value);

        Assert.Equal(createdReportResponse.Id, response.Id);
        Assert.Equal(createdReportResponse.ReportStatus, response.ReportStatus);
        Assert.Equal(createdReportResponse.CreatedDate, response.CreatedDate);

        _mockMediator.Verify(m => m.Send(createReportCommand, default), Times.Once);
    }

    [Fact]
    public async Task GetListReport_ActionExecutes_ReturnsOkResultWithReportList()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageIndex = 0,
            PageSize = 10,
        };

        GetListResponse<GetListReportListItemDto> getListReportResponse = new()
        {
            Items = new List<GetListReportListItemDto>
            {
                new GetListReportListItemDto() {Id = Guid.NewGuid(), ReportStatus = ReportStatus.Completed, CreatedDate = DateTime.UtcNow},
                new GetListReportListItemDto() {Id = Guid.NewGuid(), ReportStatus = ReportStatus.Completed, CreatedDate = DateTime.UtcNow},
                new GetListReportListItemDto() {Id = Guid.NewGuid(), ReportStatus = ReportStatus.Completed, CreatedDate = DateTime.UtcNow}
            },
            Index = 0,
            Size = 10,
            Pages = 1,
            Count = 3,
            HasNext = false,
            HasPrevious = false,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetListReportQuery>(), default)).ReturnsAsync(getListReportResponse);

        // Act

        var result = await _reportsController.GetList(pageRequest);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetListResponse<GetListReportListItemDto>>(okResult.Value);

        Assert.Equal(getListReportResponse.Index, response.Index);
        Assert.Equal(getListReportResponse.Size, response.Size);
        Assert.Equal(getListReportResponse.Pages, response.Pages);
        Assert.Equal(getListReportResponse.Count, response.Count);
        Assert.Equal(getListReportResponse.HasNext, response.HasNext);
        Assert.Equal(getListReportResponse.HasPrevious, response.HasPrevious);
        Assert.Equal(getListReportResponse.Items, response.Items);
        Assert.Equal(getListReportResponse.Items.Count, response.Items.Count);

        _mockMediator.Verify(m => m.Send(It.Is<GetListReportQuery>(q => q.PageRequest.PageIndex == pageRequest.PageIndex && q.PageRequest.PageSize == pageRequest.PageSize), default), Times.Once);

    }
}
