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

namespace Application.Features.Hotels.Queries.GetManagerList;

public class GetListManagerQuery : IRequest<GetListResponse<GetListManagerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListManagerQueryHandler : IRequestHandler<GetListManagerQuery, GetListResponse<GetListManagerListItemDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetListManagerQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListManagerListItemDto>> Handle(GetListManagerQuery request, CancellationToken cancellationToken)
        {
            Paginate<Hotel> hotels = await _hotelRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetListManagerListItemDto> response = _mapper.Map<GetListResponse<GetListManagerListItemDto>>(hotels);
            return response;
        }
    }
}
