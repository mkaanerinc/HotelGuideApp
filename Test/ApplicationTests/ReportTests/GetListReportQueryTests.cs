using Application.Features.Reports.Queries.GetList;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Reports.Queries.GetList.GetListReportQuery;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Test.ApplicationTests.ReportTests;

public class GetListReportQueryTests
{
    private readonly Mock<IReportRepository> _mockReportRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetListReportQueryHandler _handler;

    public GetListReportQueryTests()
    {
        _mockReportRepository = new Mock<IReportRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new(_mockReportRepository.Object,_mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GetReportWhenValidQuery_ReturnReportWithPages()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageSize = 10,
            PageIndex = 0,
        };

        GetListReportQuery requestObject = new() { PageRequest = pageRequest };

        CancellationToken cancellationToken = new CancellationToken();

        var paginatedReports = new Paginate<Report>();

        var responseObject = new GetListResponse<GetListReportListItemDto>();

        _mockReportRepository.Setup(m => m.GetListAsync(
            It.IsAny<Expression<Func<Report, bool>>>(),
            It.IsAny<Func<IQueryable<Report>, IOrderedQueryable<Report>>>(),
            It.IsAny<Func<IQueryable<Report>, IIncludableQueryable<Report, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedReports);

        _mockMapper.Setup(m => m.Map<GetListResponse<GetListReportListItemDto>>(It.IsAny<Paginate<Report>>()))
            .Returns(responseObject);

        // Act

        var result = await _handler.Handle(requestObject, cancellationToken);

        // Assert

        _mockReportRepository.Verify(x => x.GetListAsync(
            It.IsAny<Expression<Func<Report, bool>>>(),
            It.IsAny<Func<IQueryable<Report>, IOrderedQueryable<Report>>>(),
            It.IsAny<Func<IQueryable<Report>, IIncludableQueryable<Report, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()), Times.Once);

        _mockMapper.Verify(x => x.Map<GetListResponse<GetListReportListItemDto>>(It.IsAny<Paginate<Report>>()), Times.Once);
    }
}
