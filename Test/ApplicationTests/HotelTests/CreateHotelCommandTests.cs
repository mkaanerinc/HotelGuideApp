using Application.Features.Hotels.Rules;
using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Hotels.Commands.Create;
using static Application.Features.Hotels.Commands.Create.CreateHotelCommand;

namespace Test.ApplicationTests.HotelTests;

public class CreateHotelCommandTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly HotelBusinessRules _hotelBusinessRules;
    private readonly CreateHotelCommandHandler _handler;

    public CreateHotelCommandTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockMapper = new Mock<IMapper>();
        _hotelBusinessRules = new(_mockHotelRepository.Object);
        _handler = new CreateHotelCommandHandler(
            _mockHotelRepository.Object,
            _mockMapper.Object,
            _hotelBusinessRules);
    }

    [Fact]
    public async Task Handle_CreateHotelWhenValidCommand_ReturnsCreatedHotelResponse()
    {
        // Arrange

        CreateHotelCommand requestObject = new()
        {
            ManagerFirstName = "John",
            ManagerLastName = "Doe",
            CompanyName = "NeredeKal"
        };

        CancellationToken cancellationToken = new();

        Hotel expectedHotel = new()
        {
            Id = Guid.NewGuid(),
            ManagerFirstName = "John",
            ManagerLastName = "Doe",
            CompanyName = "NeredeKal",
            CreatedDate = DateTime.UtcNow,
        };

        CreatedHotelResponse expectedResponseObject = new()
        {
            Id = expectedHotel.Id,
            ManagerFirstName = "John",
            ManagerLastName = "Doe",
            CompanyName = "NeredeKal",
            CreatedDate = expectedHotel.CreatedDate,
        };

        _mockMapper.Setup(m => m.Map<Hotel>(It.IsAny<CreateHotelCommand>())).Returns(expectedHotel);
        _mockHotelRepository.Setup(m => m.AddAsync(It.IsAny<Hotel>())).ReturnsAsync(expectedHotel);
        _mockMapper.Setup(m => m.Map<CreatedHotelResponse>(It.IsAny<Hotel>())).Returns(expectedResponseObject);

        // Act

        var result = await _handler.Handle(requestObject,cancellationToken);

        // Assert
        Assert.Equal(expectedHotel.ManagerFirstName, expectedResponseObject.ManagerFirstName);
        Assert.Equal(expectedHotel.ManagerLastName, expectedResponseObject.ManagerLastName);
        Assert.Equal(expectedHotel.CompanyName, expectedResponseObject.CompanyName);
        Assert.Equal(expectedHotel.CreatedDate, expectedResponseObject.CreatedDate);

        _mockMapper.Verify(x => x.Map<Hotel>(requestObject), Times.Once);
        _mockHotelRepository.Verify(x => x.AddAsync(expectedHotel), Times.Once);
        _mockMapper.Verify(x => x.Map<CreatedHotelResponse>(expectedHotel), Times.Once);

    }
}
