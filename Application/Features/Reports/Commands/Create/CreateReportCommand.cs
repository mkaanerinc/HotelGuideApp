using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using Domain.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.MessageBrokers;

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommand : IRequest<CreatedReportResponse>
{
    public string Location { get; set; }

    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreatedReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBrokerHelper _messageBrokerHelper;

        public CreateReportCommandHandler(IReportRepository reportRepository, IMapper mapper, IMessageBrokerHelper messageBrokerHelper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _messageBrokerHelper = messageBrokerHelper;
        }

        public async Task<CreatedReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            Report report = new();
            report.Id = Guid.NewGuid();
            report.ReportStatus = ReportStatus.InProgress;

            await _reportRepository.AddAsync(report);

            _messageBrokerHelper.Publish(request.Location);

            CreatedReportResponse response = _mapper.Map<CreatedReportResponse>(report);
            return response;
        }
    }
}
