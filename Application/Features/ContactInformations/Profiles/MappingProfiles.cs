using Application.Features.ContactInformations.Commands.Create;
using Application.Features.ContactInformations.Commands.Delete;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ContactInformation,CreateContactInformationCommand>().ReverseMap();
            CreateMap<ContactInformation,CreatedContactInformationResponse>().ReverseMap();

            CreateMap<ContactInformation, DeleteContactInformationCommand>().ReverseMap();
            CreateMap<ContactInformation, DeletedContactInformationResponse>().ReverseMap();
        }
    }
}
