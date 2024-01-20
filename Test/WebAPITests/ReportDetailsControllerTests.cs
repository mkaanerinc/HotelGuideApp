using Application.Features.Hotels.Queries.GetManagerList;
using Application.Features.ReportDetails.Queries.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Test.WebAPITests;

public class ReportDetailsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ReportDetailsController _reportDetailsController;

    public ReportDetailsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _reportDetailsController = new()
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
    public async Task GetListReportDetail_ActionExecutes_ReturnsOkResultWithReportDetailList()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageIndex = 0,
            PageSize = 10,
        };

        GetListResponse<GetListReportDetailListItemDto> getListReportDetailResponse = new()
        {
            Items = new List<GetListReportDetailListItemDto>
            {
                new GetListReportDetailListItemDto() {Id = Guid.NewGuid(), ReportId = Guid.NewGuid(), HotelCount = 3, PhoneCount = 3, Location = "Mersin"},
                new GetListReportDetailListItemDto() {Id = Guid.NewGuid(), ReportId = Guid.NewGuid(), HotelCount = 3, PhoneCount = 3, Location = "Mersin"},
                new GetListReportDetailListItemDto() {Id = Guid.NewGuid(), ReportId = Guid.NewGuid(), HotelCount = 3, PhoneCount = 3, Location = "Mersin"}
            },
            Index = 0,
            Size = 10,
            Pages = 1,
            Count = 3,
            HasNext = false,
            HasPrevious = false,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetListReportDetailQuery>(), default)).ReturnsAsync(getListReportDetailResponse);

        // Act

        var result = await _reportDetailsController.GetList(pageRequest);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetListResponse<GetListReportDetailListItemDto>>(okResult.Value);

        Assert.Equal(getListReportDetailResponse.Index, response.Index);
        Assert.Equal(getListReportDetailResponse.Size, response.Size);
        Assert.Equal(getListReportDetailResponse.Pages, response.Pages);
        Assert.Equal(getListReportDetailResponse.Count, response.Count);
        Assert.Equal(getListReportDetailResponse.HasNext, response.HasNext);
        Assert.Equal(getListReportDetailResponse.HasPrevious, response.HasPrevious);
        Assert.Equal(getListReportDetailResponse.Items, response.Items);
        Assert.Equal(getListReportDetailResponse.Items.Count, response.Items.Count);

        _mockMediator.Verify(m => m.Send(It.Is<GetListReportDetailQuery>(q => q.PageRequest.PageIndex == pageRequest.PageIndex && q.PageRequest.PageSize == pageRequest.PageSize), default), Times.Once);

    }
}
