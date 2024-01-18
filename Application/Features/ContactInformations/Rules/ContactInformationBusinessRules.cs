using Application.Features.ContactInformations.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Rules;

public class ContactInformationBusinessRules : BaseBusinessRules
{
    private readonly IHotelRepository _hotelRepository;

    public ContactInformationBusinessRules(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task HotelIdMustBeExistsWhenInserted(Guid hotelId)
    {
        bool isHotelIdExists = await _hotelRepository.AnyAsync(h => h.Id == hotelId);

        if (!isHotelIdExists)
        {
            throw new BusinessException(ContactInformationsMessages.HotelIdNotFound);
        }
    }
}
