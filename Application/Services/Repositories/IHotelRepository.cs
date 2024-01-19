using Application.Features.Hotels.Queries.GetHotelDetailById;
using Core.Application.Responses;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories;

public interface IHotelRepository : IAsyncRepository<Hotel,Guid>, IRepository<Hotel,Guid>
{
    Task<List<GetHotelDetailByIdResponse>> GetHotelDetailById(Guid hotelId, CancellationToken cancellationToken = default);
    Task<List<Hotel>> GetListHotelByLocation(string location, CancellationToken cancellationToken = default);
}
