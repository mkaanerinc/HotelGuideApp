using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ReportDetail : Entity<Guid>
{
    public Guid ReportId { get; set; }
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneCount { get; set; }

    public ReportDetail()
    {
        
    }

    public ReportDetail(Guid id, Guid reportId, string location, int hotelCount, int phoneCount)
    {
        Id = id;
        ReportId = reportId;
        Location = location;
        HotelCount = hotelCount;
        PhoneCount = phoneCount;
    }
}
