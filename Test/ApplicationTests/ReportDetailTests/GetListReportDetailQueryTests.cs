using Application.Features.ReportDetails.Queries.GetList;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ReportDetails.Queries.GetList.GetListReportDetailQuery;

namespace Test.ApplicationTests.ReportDetailDetailTests;

public class GetListReportDetailQueryTests
{
    private readonly Mock<IReportDetailRepository> _mockReportDetailRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetListReportDetailQueryHandler _handler;

    public GetListReportDetailQueryTests()
    {
        _mockReportDetailRepository = new Mock<IReportDetailRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new(_mockReportDetailRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GetReportDetailWhenValidQuery_ReturnReportDetailWithPages()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageSize = 10,
            PageIndex = 0,
        };

        GetListReportDetailQuery requestObject = new() { PageRequest = pageRequest };

        CancellationToken cancellationToken = new CancellationToken();

        var paginatedReportDetails = new Paginate<ReportDetail>();

        var responseObject = new GetListResponse<GetListReportDetailListItemDto>();

        _mockReportDetailRepository.Setup(m => m.GetListAsync(
            It.IsAny<Expression<Func<ReportDetail, bool>>>(),
            It.IsAny<Func<IQueryable<ReportDetail>, IOrderedQueryable<ReportDetail>>>(),
            It.IsAny<Func<IQueryable<ReportDetail>, IIncludableQueryable<ReportDetail, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedReportDetails);

        _mockMapper.Setup(m => m.Map<GetListResponse<GetListReportDetailListItemDto>>(It.IsAny<Paginate<ReportDetail>>()))
            .Returns(responseObject);

        // Act

        var result = await _handler.Handle(requestObject, cancellationToken);

        // Assert

        _mockReportDetailRepository.Verify(x => x.GetListAsync(
            It.IsAny<Expression<Func<ReportDetail, bool>>>(),
            It.IsAny<Func<IQueryable<ReportDetail>, IOrderedQueryable<ReportDetail>>>(),
            It.IsAny<Func<IQueryable<ReportDetail>, IIncludableQueryable<ReportDetail, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()), Times.Once);

        _mockMapper.Verify(x => x.Map<GetListResponse<GetListReportDetailListItemDto>>(It.IsAny<Paginate<ReportDetail>>()), Times.Once);
    }
}
