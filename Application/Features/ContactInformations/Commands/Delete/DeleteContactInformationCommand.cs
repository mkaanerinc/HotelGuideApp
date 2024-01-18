using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Commands.Delete;

public class DeleteContactInformationCommand : IRequest<DeletedContactInformationResponse>
{
    public Guid Id { get; set; }

    public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformationCommand, DeletedContactInformationResponse>
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IMapper _mapper;

        public DeleteContactInformationCommandHandler(IContactInformationRepository contactInformationRepository, IMapper mapper)
        {
            _contactInformationRepository = contactInformationRepository;
            _mapper = mapper;
        }

        public async Task<DeletedContactInformationResponse> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken)
        {
            ContactInformation? contactInformation = await _contactInformationRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);

            contactInformation = _mapper.Map(request,contactInformation);

            await _contactInformationRepository.DeleteAsync(contactInformation);

            DeletedContactInformationResponse response = _mapper.Map<DeletedContactInformationResponse>(contactInformation);
            return response;
        }
    }
}
