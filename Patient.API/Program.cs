using MessageBroker.Base;
using MessageBroker.Base.Abstract;
using MessageBroker.Provider;
using Microsoft.EntityFrameworkCore;
using Patient.API.Data;
using Patient.API.Events;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PatientAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PatientAPIContext") ?? throw new InvalidOperationException("Connection string 'PatientAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageBroker>(sp =>
{
    MessageBrokerConfiguration config = new()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "Event",
        SubscriberClientAppName = "Patient",
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

app.Run();