using Application.Features.Hotels.Commands.Create;
using Application.Features.Hotels.Commands.Delete;
using Application.Features.Hotels.Queries.GetManagerList;
using Application.Features.Hotels.Queries.GetHotelDetailById;

namespace Test.WebAPITests;

public class HotelsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly HotelsController _hotelsController;

    public HotelsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _hotelsController = new()
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
    public async Task Add_CreateHotelCommandIsValid_ReturnsOkResultWithCreatedHotelResponse()
    {
        // Arrange

        CreateHotelCommand createHotelCommand = new() { 
            ManagerFirstName = "John", 
            ManagerLastName = "Doe" ,
            CompanyName = "NeredeKal" 
        };

        CreatedHotelResponse createdHotelResponse = new()
        {
            Id = Guid.NewGuid(),
            ManagerFirstName = "John",
            ManagerLastName = "Doe",
            CompanyName = "NeredeKal",
            CreatedDate = DateTime.UtcNow,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateHotelCommand>(),default)).ReturnsAsync(createdHotelResponse);

        // Act

        var result = await _hotelsController.Add(createHotelCommand);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CreatedHotelResponse>(okResult.Value);

        Assert.Equal(createdHotelResponse.Id, response.Id);
        Assert.Equal(createdHotelResponse.ManagerFirstName, response.ManagerFirstName);
        Assert.Equal(createdHotelResponse.ManagerLastName, response.ManagerLastName);
        Assert.Equal(createdHotelResponse.CompanyName, response.CompanyName);
        Assert.Equal(createdHotelResponse.CreatedDate, response.CreatedDate);

        _mockMediator.Verify(m => m.Send(createHotelCommand, default), Times.Once);
    }

    [Fact]
    public async Task Delete_DeleteHotelCommandIsValid_ReturnsOkResultWithDeletedHotelResponse()
    {
        // Arrange

        DeleteHotelCommand deleteHotelCommand = new()
        {
            Id = Guid.NewGuid()
        };

        DeletedHotelResponse deletedHotelResponse = new()
        {
            Id = deleteHotelCommand.Id,
            DeletedDate = DateTime.UtcNow
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteHotelCommand>(),default)).ReturnsAsync(deletedHotelResponse);

        // Act

        var result = await _hotelsController.Delete(deleteHotelCommand);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<DeletedHotelResponse>(okResult.Value);

        Assert.Equal(deletedHotelResponse.Id, response.Id);
        Assert.Equal(deletedHotelResponse.DeletedDate, response.DeletedDate);

        _mockMediator.Verify(m => m.Send(deleteHotelCommand, default), Times.Once);
    }

    [Fact]
    public async Task GetListManager_ActionExecutes_ReturnsOkResultWithManagerList()
    {
        // Arrange

        PageRequest pageRequest = new()
        {
            PageIndex = 0,
            PageSize = 10,
        };     

        GetListResponse<GetListManagerListItemDto> getListManagerResponse = new()
        {
            Items = new List<GetListManagerListItemDto>
            {
                new GetListManagerListItemDto() {ManagerFirstName = "John", ManagerLastName = "Doe", CompanyName = "NeredeKal"},
                new GetListManagerListItemDto() {ManagerFirstName = "John", ManagerLastName = "Doe", CompanyName = "NeredeKal"},
                new GetListManagerListItemDto() {ManagerFirstName = "John", ManagerLastName = "Doe", CompanyName = "NeredeKal"}
            },
            Index = 0,
            Size = 10,
            Pages = 1,
            Count = 3,
            HasNext = false,
            HasPrevious = false,
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetListManagerQuery>(), default)).ReturnsAsync(getListManagerResponse);

        // Act

        var result = await _hotelsController.GetListManager(pageRequest);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetListResponse<GetListManagerListItemDto>>(okResult.Value);

        Assert.Equal(getListManagerResponse.Index, response.Index);
        Assert.Equal(getListManagerResponse.Size, response.Size);
        Assert.Equal(getListManagerResponse.Pages, response.Pages);
        Assert.Equal(getListManagerResponse.Count, response.Count);
        Assert.Equal(getListManagerResponse.HasNext, response.HasNext);
        Assert.Equal(getListManagerResponse.HasPrevious, response.HasPrevious);
        Assert.Equal(getListManagerResponse.Items, response.Items);
        Assert.Equal(getListManagerResponse.Items.Count, response.Items.Count);     

        _mockMediator.Verify(m => m.Send(It.Is<GetListManagerQuery>(q => q.PageRequest.PageIndex == pageRequest.PageIndex && q.PageRequest.PageSize == pageRequest.PageSize), default), Times.Once);

    }

    [Fact]
    public async Task GetHotelDetailById_ActionExecutes_ReturnsOkResultWithHotelDetail()
    {
        // Arrange

        Guid id = Guid.NewGuid();

        List<GetHotelDetailByIdResponse> getHotelDetailByIdResponse = new()
        {
            new GetHotelDetailByIdResponse()
            {
                Id= id,
                ManagerFirstName = "John",
                ManagerLastName = "Doe",
                CompanyName = "NeredeKal",
                InfoType = InfoType.Phone,
                InfoContent = "+905333333333"
            },
            new GetHotelDetailByIdResponse()
            {
                Id= id,
                ManagerFirstName = "John",
                ManagerLastName = "Doe",
                CompanyName = "NeredeKal",
                InfoType = InfoType.Location,
                InfoContent = "Mersin"
            },
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetHotelDetailByIdQuery>(), default)).ReturnsAsync(getHotelDetailByIdResponse);

        // Act

        var result = await _hotelsController.GetHotelDetailById(id);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<List<GetHotelDetailByIdResponse>>(okResult.Value);

        Assert.Equal(id, response[0].Id);
        Assert.Equal(id, response[1].Id);
        Assert.Equal(InfoType.Phone, response[0].InfoType);
        Assert.Equal(InfoType.Location, response[1].InfoType);
        Assert.Equal("+905333333333", response[0].InfoContent);
        Assert.Equal("Mersin", response[1].InfoContent);
        Assert.Equal("John", response[0].ManagerFirstName);
        Assert.Equal("Doe", response[0].ManagerLastName);
        Assert.Equal("John", response[1].ManagerFirstName);
        Assert.Equal("Doe", response[1].ManagerLastName);

        Assert.Equal(getHotelDetailByIdResponse.Count, response.Count);

        _mockMediator.Verify(m => m.Send(It.Is<GetHotelDetailByIdQuery>(q => q.Id == id), default), Times.Once);
    }
}
