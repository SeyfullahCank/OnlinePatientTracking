using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notification.API.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using MessageBroker.Base.Abstract;
using MessageBroker.Provider;
using MessageBroker.Base;
using Notification.API.Events;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotificationAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotificationAPIContext") ?? throw new InvalidOperationException("Connection string 'NotificationAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<PatientCreatedEventHandler>();
builder.Services.AddTransient<AppointmentCreatedEventHandler>();

builder.Services.AddSingleton<IMessageBroker>(sp =>
{
    MessageBrokerConfiguration config = new()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "Event",
        SubscriberClientAppName = "Notification",
        DefaultTopicName = Assembly.GetExecutingAssembly().GetName().Name,
        MessageBrokerType = MessageBrokerTypes.RabbitMQ,
    };

    return MessageBrokerProvider.Provide(config, sp);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AddEventSubscriptions(app);

app.Run();

void AddEventSubscriptions(WebApplication app)
{
    var eventBus = app.Services.GetRequiredService<IMessageBroker>();

    eventBus.Subscribe<PatientCreatedEvent, PatientCreatedEventHandler>("Patient");
    eventBus.Subscribe<AppointmentCreatedEvent, AppointmentCreatedEventHandler>("Appointment");
}