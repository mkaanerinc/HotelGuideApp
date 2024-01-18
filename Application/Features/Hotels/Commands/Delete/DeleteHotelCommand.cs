using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Commands.Delete;

public class DeleteHotelCommand : IRequest<DeletedHotelResponse>
{
    public Guid Id { get; set; }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, DeletedHotelResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedHotelResponse> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            Hotel? hotel = await _hotelRepository.GetAsync(predicate: h => h.Id == request.Id,cancellationToken: cancellationToken);

            hotel = _mapper.Map(request,hotel);

            await _hotelRepository.DeleteAsync(hotel);

            DeletedHotelResponse response = _mapper.Map<DeletedHotelResponse>(hotel);
            return response;
        }
    }
}
