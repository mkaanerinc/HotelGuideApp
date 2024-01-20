using Application.Features.Hotels.Queries.GetHotelDetailById;
using Application.Services.Repositories;
using Azure;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Hotels.Queries.GetHotelDetailById.GetHotelDetailByIdQuery;

namespace Test.ApplicationTests.HotelTests;

public class GetHotelDetailByIdQueryTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetHotelDetailByIdQueryHandler _handler;

    public GetHotelDetailByIdQueryTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new(_mockHotelRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_GetHotelDetailByIdWhenValidQuery_ReturnsListHotelDetails()
    {
        // Arrange

        GetHotelDetailByIdQuery requestObject = new() { Id = Guid.NewGuid()};

        CancellationToken cancellationToken = new();

        List<GetHotelDetailByIdResponse> responseObject = new()
        {
            new GetHotelDetailByIdResponse() {CompanyName = "NeredeKal"}
        };

        _mockHotelRepository.Setup(x => x.GetHotelDetailById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseObject);

        // Act

        var result = await _handler.Handle(requestObject, cancellationToken);

        // Assert

        Assert.Equal(responseObject, result);
        _mockHotelRepository.Verify(x => x.GetHotelDetailById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
