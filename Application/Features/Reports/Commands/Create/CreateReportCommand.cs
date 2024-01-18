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

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommand : IRequest<CreatedReportResponse>
{
    public string Location { get; set; }

    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreatedReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public CreateReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<CreatedReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            Report report = new();
            report.Id = Guid.NewGuid();
            report.ReportStatus = ReportStatus.InProgress;

            await _reportRepository.AddAsync(report);

            CreatedReportResponse response = _mapper.Map<CreatedReportResponse>(report);
            return response;
        }
    }
}
