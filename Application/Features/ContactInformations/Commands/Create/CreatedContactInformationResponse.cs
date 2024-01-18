using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactInformations.Commands.Create;

public class CreatedContactInformationResponse
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public InfoType InfoType { get; set; }
    public string InfoContent { get; set; }
    public DateTime CreatedDate { get; set; }
}
