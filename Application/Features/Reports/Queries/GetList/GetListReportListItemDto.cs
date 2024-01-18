using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto
{
    public Guid Id { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public DateTime CreatedDate { get; set; }
}
