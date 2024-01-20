using Application.Features.Hotels.Commands.Delete;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Hotels.Commands.Delete.DeleteHotelCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Test.ApplicationTests.HotelTests;

public class DeleteHotelCommandTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly DeleteHotelCommandHandler _handler;

    public DeleteHotelCommandTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new(_mockHotelRepository.Object,_mockMapper.Object);
    }

    [Fact]
    public async Task Handle_DeleteHotelWhenValidCommand_ReturnsDeletedHotelResponse()
    {
        // Arrange

        _mockHotelRepository.Setup(m => m.GetAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Hotel?>());

        _mockMapper.Setup(m => m.Map(It.IsAny<DeleteHotelCommand>(), It.IsAny<Hotel?>()))
            .Returns(It.IsAny<Hotel?>());

        _mockHotelRepository.Setup(m => m.DeleteAsync(It.IsAny<Hotel>(), It.IsAny<bool>()))
            .ReturnsAsync(It.IsAny<Hotel>());

        _mockMapper.Setup(m => m.Map<DeletedHotelResponse>(It.IsAny<Hotel?>()))
            .Returns(It.IsAny<DeletedHotelResponse>());

        // Act

        var result = await _handler.Handle(It.IsAny<DeleteHotelCommand>(), It.IsAny<CancellationToken>());

        // Assert

        _mockHotelRepository.Verify(m => m.GetAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            ),Times.Once);
        _mockMapper.Verify(m => m.Map(It.IsAny<DeleteHotelCommand>(), It.IsAny<Hotel?>()),Times.Once);
        _mockHotelRepository.Verify(m => m.DeleteAsync(It.IsAny<Hotel>(), It.IsAny<bool>()), Times.Once);
        _mockMapper.Verify(m => m.Map<DeletedHotelResponse>(It.IsAny<Hotel?>()),Times.Once);
    }
}
