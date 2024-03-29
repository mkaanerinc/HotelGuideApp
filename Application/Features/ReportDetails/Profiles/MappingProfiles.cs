﻿using Application.Features.ReportDetails.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReportDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ReportDetail,GetListReportDetailListItemDto>().ReverseMap();
        CreateMap<Paginate<ReportDetail>,GetListResponse<GetListReportDetailListItemDto>>().ReverseMap();
    }
}
