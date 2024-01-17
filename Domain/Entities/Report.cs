using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Report : Entity<Guid>
{
    public ReportStatus ReportStatus { get; set; }

    public virtual ICollection<ReportDetail> ReportDetails { get; set; }
    public Report()
    {
        ReportDetails = new HashSet<ReportDetail>();
    }

    public Report(Guid id, ReportStatus reportStatus)
    {
        Id = id;
        ReportStatus = reportStatus;
    }
}