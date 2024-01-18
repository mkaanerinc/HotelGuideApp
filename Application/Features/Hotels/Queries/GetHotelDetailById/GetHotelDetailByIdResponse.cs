using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Queries.GetHotelDetailById;

public class GetHotelDetailByIdResponse
{
    public Guid Id { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string CompanyName { get; set; }
    public InfoType InfoType { get; set; }
    public string InfoContent { get; set; }
}
