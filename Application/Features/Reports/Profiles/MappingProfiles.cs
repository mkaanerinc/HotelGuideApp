using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Report, CreatedReportResponse>().ReverseMap();

        CreateMap<Report,GetListReportListItemDto>().ReverseMap();
        CreateMap<Paginate<Report>, GetListResponse<GetListReportListItemDto>>().ReverseMap();
    }
}
