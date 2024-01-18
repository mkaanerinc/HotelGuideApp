using Application.Features.Hotels.Commands.Create;
using Application.Features.Hotels.Commands.Delete;
using Application.Features.Hotels.Queries.GetManagerList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Hotel, CreateHotelCommand>().ReverseMap();
        CreateMap<Hotel, CreatedHotelResponse>().ReverseMap();

        CreateMap<Hotel, DeleteHotelCommand>().ReverseMap();
        CreateMap<Hotel, DeletedHotelResponse>().ReverseMap();

        CreateMap<Hotel, GetListManagerListItemDto>().ReverseMap();
        CreateMap<Paginate<Hotel>, GetListResponse<GetListManagerListItemDto>>().ReverseMap();
    }
}
