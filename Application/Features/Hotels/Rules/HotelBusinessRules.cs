using Application.Features.Hotels.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Rules;

public class HotelBusinessRules : BaseBusinessRules
{
    private readonly IHotelRepository _hotelRepository;

    public HotelBusinessRules(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task CompanyNameCannotBeDuplicatedWhenInserted(string companyName)
    {
        Hotel? result = await _hotelRepository.GetAsync(h => h.CompanyName.ToLower() == companyName.ToLower());

        if (result != null)
        {
            throw new BusinessException(HotelsMessages.CompanyNameExists);
        }
    }
}
