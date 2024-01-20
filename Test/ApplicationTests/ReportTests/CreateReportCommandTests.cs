using Application.Features.Reports.Commands.Create;
using Application.Services.MessageBrokers;
using Application.Services.Repositories;
using Application.Services.MessageBrokers.RabbitMQ.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Application.Features.Reports.Commands.Create.CreateReportCommand;

namespace Test.ApplicationTests.ReportTests;

public class CreateReportCommandTests
{
    private readonly Mock<IReportRepository> _mockReportRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMessageBrokerHelper> _mockMessageBrokerHelper;
    private readonly CreateReportCommandHandler _handler;

    public CreateReportCommandTests()
    {
        _mockReportRepository = new Mock<IReportRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockMessageBrokerHelper = new Mock<IMessageBrokerHelper>();
        _handler = new(_mockReportRepository.Object,
            _mockMapper.Object,
            _mockMessageBrokerHelper.Object);
    }

    [Fact]
    public async Task Handle_CreateReportWhenValidCommand_ReturnsCreatedReportResponse()
    {
        // Arrange

        CreateReportCommand requestObject = new() { Location = "Mersin" };

        CancellationToken cancellationToken = new();

        Report report = new() { Id = Guid.NewGuid(), ReportStatus = ReportStatus.InProgress};

        CreateReportDetailMessage createReportDetailMessage = new()
        {
            Location = requestObject.Location,
            ReportId = report.Id
        };

        CreatedReportResponse responseObject = new()
        {
            Id = report.Id,
            CreatedDate = DateTime.UtcNow,
            ReportStatus = ReportStatus.InProgress
        };

        _mockReportRepository.Setup(m => m.AddAsync(It.IsAny<Report>())).ReturnsAsync(report);
        _mockMessageBrokerHelper.Setup(m => m.Publish(createReportDetailMessage));
        _mockMapper.Setup(m => m.Map<CreatedReportResponse>(It.IsAny<Report>())).Returns(responseObject);

        // Act

        var result = await _handler.Handle(requestObject, cancellationToken);

        // Assert

        _mockReportRepository.Verify(x => x.AddAsync(It.IsAny<Report>()), Times.Once);
        _mockMapper.Verify(x => x.Map<CreatedReportResponse>(It.IsAny<Report>()), Times.Once);
    }
}
