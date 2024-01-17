using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ContactInformation : Entity<Guid>
{
    public Guid HotelId { get; set; }
    public InfoType InfoType { get; set; }
    public string InfoContent { get; set; }

    public ContactInformation()
    {
        
    }

    public ContactInformation(Guid id, Guid hotelId, InfoType infoType, string infoContent)
    {
        Id = id;
        HotelId = hotelId;
        InfoType = infoType;
        InfoContent = infoContent;
    }
}
