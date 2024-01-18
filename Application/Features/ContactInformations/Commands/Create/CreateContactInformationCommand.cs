using Application.Features.ContactInformations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Commands.Create;

public class CreateContactInformationCommand : IRequest<CreatedContactInformationResponse>
{
    public Guid HotelId { get; set; }
    public InfoType InfoType { get; set; }
    public string InfoContent { get; set; }

    public class CreateContactInformationCommandHandler : IRequestHandler<CreateContactInformationCommand, CreatedContactInformationResponse>
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IMapper _mapper;
        private readonly ContactInformationBusinessRules _contactInformationBusinessRules;

        public CreateContactInformationCommandHandler(IContactInformationRepository contactInformationRepository, IMapper mapper, ContactInformationBusinessRules contactInformationBusinessRules)
        {
            _contactInformationRepository = contactInformationRepository;
            _mapper = mapper;
            _contactInformationBusinessRules = contactInformationBusinessRules;
        }

        public async Task<CreatedContactInformationResponse> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
        {
            await _contactInformationBusinessRules.HotelIdMustBeExistsWhenInserted(request.HotelId);

            ContactInformation? contactInformation = _mapper.Map<ContactInformation>(request);
            contactInformation.Id = Guid.NewGuid();

            await _contactInformationRepository.AddAsync(contactInformation);

            CreatedContactInformationResponse response = _mapper.Map<CreatedContactInformationResponse>(contactInformation);
            return response;
        }
    }
}
