using Application.Features.Hotels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Commands.Create;

public class CreateHotelCommand : IRequest<CreatedHotelResponse>
{
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string CompanyName { get; set; }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, CreatedHotelResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly HotelBusinessRules _hotelBusinessRules;
        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper, HotelBusinessRules hotelBusinessRules)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _hotelBusinessRules = hotelBusinessRules;
        }

        public async Task<CreatedHotelResponse> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            await _hotelBusinessRules.CompanyNameCannotBeDuplicatedWhenInserted(request.CompanyName);

            Hotel hotel = _mapper.Map<Hotel>(request);
            hotel.Id = Guid.NewGuid();

            await _hotelRepository.AddAsync(hotel);

            CreatedHotelResponse response = _mapper.Map<CreatedHotelResponse>(hotel);

            return response;
        }
    }
}
