using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Commands.Create;

public class CreatedHotelResponse
{
    public Guid Id { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string CompanyName { get; set; }
    public DateTime CreatedDate { get; set; }
}
