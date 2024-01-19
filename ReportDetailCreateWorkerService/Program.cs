using Application;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Contexts;
using Persistence.Repositories;
using RabbitMQ.Client;
using ReportDetailCreateWorkerService;
using ReportDetailCreateWorkerService.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetSection("MessageBrokers:RabbitMQ:AMQPUrl").Value),
    DispatchConsumersAsync = true,
});

builder.Services.AddDbContext<BaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelGuideApp"));
},contextLifetime: ServiceLifetime.Singleton);

builder.Services.AddSingleton<IReportRepository,ReportRepository>();
builder.Services.AddSingleton<IReportDetailRepository,ReportDetailRepository>();
builder.Services.AddSingleton<IHotelRepository,HotelRepository>();
builder.Services.AddSingleton<IContactInformationRepository,ContactInformationRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
