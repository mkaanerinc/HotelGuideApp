using Application.Features.Hotels.Queries.GetHotelDetailById;
using Application.Services.Repositories;
using Core.Application.Responses;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class HotelRepository : EfRepositoryBase<Hotel,Guid,BaseDbContext>, IHotelRepository
{
    private readonly BaseDbContext _baseDbContext;
    public HotelRepository(BaseDbContext context) : base(context)
    {
        _baseDbContext = context;
    }

    public async Task<List<GetHotelDetailByIdResponse>> GetHotelDetailById(Guid hotelId, CancellationToken cancellationToken = default)
    {
            var result = await (from hotel in _baseDbContext.Hotels
                         join contactInformation in _baseDbContext.ContactInformations
                         on hotel.Id equals contactInformation.HotelId
                         where hotel.Id == hotelId
                         select new GetHotelDetailByIdResponse
                         {
                             Id = hotel.Id,
                             ManagerFirstName = hotel.ManagerFirstName,
                             ManagerLastName = hotel.ManagerLastName,
                             CompanyName = hotel.CompanyName,
                             InfoType = contactInformation.InfoType,
                             InfoContent = contactInformation.InfoContent
                         }).ToListAsync();

            return result;      
    }
}
