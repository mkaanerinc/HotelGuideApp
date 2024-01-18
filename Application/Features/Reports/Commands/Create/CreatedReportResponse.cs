using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Commands.Create;

public class CreatedReportResponse
{
    public Guid Id { get; set; }
    public ReportStatus ReportStatus { get; set; }
    public DateTime CreatedDate { get; set; }
}
