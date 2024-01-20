using Application.Features.Hotels.Queries.GetManagerList;
using Application.Services.Repositories;
using Azure.Core;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Application.Features.Hotels.Queries.GetManagerList.GetListManagerQuery;

namespace Test.ApplicationTests.HotelTests;

public class GetListManagerQueryTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetListManagerQueryHandler _handler;

    public GetListManagerQueryTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new (_mockHotelRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GetManagerListWhenValidQuery_ReturnManagerListWithPages()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageSize = 10,
            PageIndex = 0,
        };

        GetListManagerQuery requestObject = new() { PageRequest = pageRequest };

        CancellationToken cancellationToken = new CancellationToken();

        var paginatedHotels = new Paginate<Hotel>();

        var responseObject = new GetListResponse<GetListManagerListItemDto>();

        _mockHotelRepository.Setup(m => m.GetListAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IOrderedQueryable<Hotel>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedHotels);

        _mockMapper.Setup(m => m.Map<GetListResponse<GetListManagerListItemDto>>(It.IsAny<Paginate<Hotel>>()))
            .Returns(responseObject);

        // Act

        var result = await _handler.Handle(requestObject, cancellationToken);

        // Assert

        _mockHotelRepository.Verify(x => x.GetListAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IOrderedQueryable<Hotel>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()), Times.Once);

        _mockMapper.Verify(x => x.Map<GetListResponse<GetListManagerListItemDto>>(It.IsAny<Paginate<Hotel>>()), Times.Once);
    }
}
