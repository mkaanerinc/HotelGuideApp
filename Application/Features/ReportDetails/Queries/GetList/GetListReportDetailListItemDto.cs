using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReportDetails.Queries.GetList;

public class GetListReportDetailListItemDto
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneCount { get; set; }
}
