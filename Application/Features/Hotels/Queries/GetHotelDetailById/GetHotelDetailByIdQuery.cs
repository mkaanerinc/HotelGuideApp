using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Queries.GetHotelDetailById;

public class GetHotelDetailByIdQuery : IRequest<List<GetHotelDetailByIdResponse>>
{
    public Guid Id { get; set; }

    public class GetHotelDetailByIdQueryHandler : IRequestHandler<GetHotelDetailByIdQuery, List<GetHotelDetailByIdResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelDetailByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<List<GetHotelDetailByIdResponse>> Handle(GetHotelDetailByIdQuery request, CancellationToken cancellationToken)
        {
            List<GetHotelDetailByIdResponse> response = await _hotelRepository.GetHotelDetailById(request.Id,cancellationToken);
            return response;
        }
    }
}
