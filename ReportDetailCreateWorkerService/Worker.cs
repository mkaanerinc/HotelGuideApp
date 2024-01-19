using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportDetailCreateWorkerService.Services;
using ReportDetailCreateWorkerService.Services.Messages;
using System.Text;
using System.Text.Json;

namespace ReportDetailCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        private readonly IHotelRepository _hotelRepository;
        private readonly IReportDetailRepository _reportDetailRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public Worker(RabbitMQClientService rabbitMQClientService, IReportRepository reportRepository,IHotelRepository hotelRepository,
            IContactInformationRepository contactInformationRepository , IReportDetailRepository reportDetailRepository)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _hotelRepository = hotelRepository;
            _reportDetailRepository = reportDetailRepository;
            _reportRepository = reportRepository;
            _contactInformationRepository = contactInformationRepository;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(
                queue: RabbitMQClientService.QueueName, 
                autoAck: false, 
                consumer: consumer);

            consumer.Received += Consumer_Received; ;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            CreateReportDetailMessage? createReportDetailMessage = JsonSerializer.Deserialize<CreateReportDetailMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            List<Hotel> hotels = await _hotelRepository.GetListHotelByLocation(createReportDetailMessage.Location);

            int totalHotel = hotels.Count;
            int totalPhone = await FindPhoneCount(hotels);

            ReportDetail reportDetail = new() 
            {
                Id = Guid.NewGuid(),
                ReportId = createReportDetailMessage.ReportId,
                Location = createReportDetailMessage.Location,
                HotelCount = totalHotel,
                PhoneCount = totalPhone
            };

            await _reportDetailRepository.AddAsync(reportDetail);

            Report? report = await _reportRepository.GetAsync(r => r.Id == createReportDetailMessage.ReportId);
            report.ReportStatus = ReportStatus.Completed;

            await _reportRepository.UpdateAsync(report);

            _channel.BasicAck(@event.DeliveryTag, false);
        }

        private async Task<int> FindPhoneCount(List<Hotel> hotels)
        {
            int count = 0;

            foreach (var hotel in hotels)
            {
                bool isExists = await _contactInformationRepository.AnyAsync(c => c.HotelId == hotel.Id && c.InfoType == InfoType.Phone);
                if(isExists)
                    count++;
            }

            return count;
        }
    }
}
