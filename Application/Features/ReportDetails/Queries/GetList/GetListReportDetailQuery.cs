using Application.Features.Reports.Queries.GetList;
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

namespace Application.Features.ReportDetails.Queries.GetList;

public class GetListReportDetailQuery : IRequest<GetListResponse<GetListReportDetailListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReportDetailQueryHandler : IRequestHandler<GetListReportDetailQuery, GetListResponse<GetListReportDetailListItemDto>>
    {
        private readonly IReportDetailRepository _reportDetailRepository;
        private readonly IMapper _mapper;

        public GetListReportDetailQueryHandler(IReportDetailRepository reportDetailRepository, IMapper mapper)
        {
            _reportDetailRepository = reportDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListReportDetailListItemDto>> Handle(GetListReportDetailQuery request, CancellationToken cancellationToken)
        {
            Paginate<ReportDetail> reportDetails = await _reportDetailRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetListReportDetailListItemDto> response = _mapper.Map<GetListResponse<GetListReportDetailListItemDto>>(reportDetails);
            return response;
        }
    }
}
