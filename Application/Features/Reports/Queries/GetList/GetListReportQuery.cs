using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportQuery : IRequest<GetListResponse<GetListReportListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReportQueryHandler : IRequestHandler<GetListReportQuery, GetListResponse<GetListReportListItemDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetListReportQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListReportListItemDto>> Handle(GetListReportQuery request, CancellationToken cancellationToken)
        {
            Paginate<Report> reports = await _reportRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetListReportListItemDto> response = _mapper.Map<GetListResponse<GetListReportListItemDto>>(reports);
            return response;
        }
    }
}
