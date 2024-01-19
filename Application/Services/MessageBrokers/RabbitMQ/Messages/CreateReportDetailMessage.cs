using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MessageBrokers.RabbitMQ.Messages;

public class CreateReportDetailMessage
{
    public Guid ReportId { get; set; }
    public string Location { get; set; }
}
