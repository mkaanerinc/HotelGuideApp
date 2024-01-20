using Application.Features.ContactInformations.Commands.Delete;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ContactInformations.Commands.Delete.DeleteContactInformationCommand;

namespace Test.ApplicationTests.ContactInformationTests;

public class DeleteContactInformationCommandTests
{
    private readonly Mock<IContactInformationRepository> _mockContactInformationRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly DeleteContactInformationCommandHandler _handler;

    public DeleteContactInformationCommandTests()
    {
        _mockContactInformationRepository = new Mock<IContactInformationRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new(_mockContactInformationRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_DeleteContactInformationWhenValidCommand_ReturnsDeletedContactInformationResponse()
    {
        // Arrange

        _mockContactInformationRepository.Setup(m => m.GetAsync(
            It.IsAny<Expression<Func<ContactInformation, bool>>>(),
            It.IsAny<Func<IQueryable<ContactInformation>, IIncludableQueryable<ContactInformation, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<ContactInformation?>());

        _mockMapper.Setup(m => m.Map(It.IsAny<DeleteContactInformationCommand>(), It.IsAny<ContactInformation?>()))
            .Returns(It.IsAny<ContactInformation?>());

        _mockContactInformationRepository.Setup(m => m.DeleteAsync(It.IsAny<ContactInformation>(), It.IsAny<bool>()))
            .ReturnsAsync(It.IsAny<ContactInformation>());

        _mockMapper.Setup(m => m.Map<DeletedContactInformationResponse>(It.IsAny<ContactInformation?>()))
            .Returns(It.IsAny<DeletedContactInformationResponse>());

        // Act

        var result = await _handler.Handle(It.IsAny<DeleteContactInformationCommand>(), It.IsAny<CancellationToken>());

        // Assert

        _mockContactInformationRepository.Verify(m => m.GetAsync(
            It.IsAny<Expression<Func<ContactInformation, bool>>>(),
            It.IsAny<Func<IQueryable<ContactInformation>, IIncludableQueryable<ContactInformation, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            ), Times.Once);
        _mockMapper.Verify(m => m.Map(It.IsAny<DeleteContactInformationCommand>(), It.IsAny<ContactInformation?>()), Times.Once);
        _mockContactInformationRepository.Verify(m => m.DeleteAsync(It.IsAny<ContactInformation>(), It.IsAny<bool>()), Times.Once);
        _mockMapper.Verify(m => m.Map<DeletedContactInformationResponse>(It.IsAny<ContactInformation?>()), Times.Once);
    }
}
