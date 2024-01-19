using Application.Services.MessageBrokers.RabbitMQ.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MessageBrokers;

public interface IMessageBrokerHelper
{
    void Publish(CreateReportDetailMessage createReportDetailMessage);
}
