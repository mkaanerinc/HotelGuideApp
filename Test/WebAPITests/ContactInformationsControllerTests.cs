using Application.Features.ContactInformations.Commands.Create;
using Application.Features.ContactInformations.Commands.Delete;
using Application.Features.Hotels.Commands.Create;
using Application.Features.Hotels.Commands.Delete;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Test.WebAPITests;

public class ContactInformationsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly ContactInformationsController _contactInformationsController;

    public ContactInformationsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _contactInformationsController = new()
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
    public async Task Add_CreateContactInformationCommandIsValid_ReturnsOkResultWithCreatedContactInformationResponse()
    {
        // Arrange

        CreateContactInformationCommand createContactInformationCommand = new()
        {
            HotelId = Guid.NewGuid(),
            InfoType = InfoType.Location,
            InfoContent = "Mersin"
        };

        CreatedContactInformationResponse createdContactInformationResponse = new()
        {
            Id = Guid.NewGuid(),
            HotelId = createContactInformationCommand.HotelId,
            InfoType = InfoType.Location,
            InfoContent = "Mersin",
            CreatedDate = DateTime.UtcNow,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateContactInformationCommand>(), default)).ReturnsAsync(createdContactInformationResponse);

        // Act

        var result = await _contactInformationsController.Add(createContactInformationCommand);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CreatedContactInformationResponse>(okResult.Value);

        Assert.Equal(createdContactInformationResponse.Id, response.Id);
        Assert.Equal(createdContactInformationResponse.HotelId, response.HotelId);
        Assert.Equal(createdContactInformationResponse.InfoType, response.InfoType);
        Assert.Equal(createdContactInformationResponse.InfoContent, response.InfoContent);
        Assert.Equal(createdContactInformationResponse.CreatedDate, response.CreatedDate);

        _mockMediator.Verify(m => m.Send(createContactInformationCommand, default), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteContactInformationCommandIsValid_ReturnsOkResultWithDeletedContactInformationResponse()
    {
        // Arrange

        DeleteContactInformationCommand deleteContactInformationCommand = new()
        {
            Id = Guid.NewGuid()
        };

        DeletedContactInformationResponse deletedContactInformationResponse = new()
        {
            Id = deleteContactInformationCommand.Id,
            DeletedDate = DateTime.UtcNow,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteContactInformationCommand>(), default)).ReturnsAsync(deletedContactInformationResponse);

        // Act

        var result = await _contactInformationsController.Delete(deleteContactInformationCommand);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<DeletedContactInformationResponse>(okResult.Value);

        Assert.Equal(deletedContactInformationResponse.Id, response.Id);
        Assert.Equal(deletedContactInformationResponse.DeletedDate, response.DeletedDate);

        _mockMediator.Verify(m => m.Send(deleteContactInformationCommand, default), Times.Once);
    }
}
