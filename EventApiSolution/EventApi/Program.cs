using NHibernate;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton(factory =>
{
    var configuration = factory.GetRequiredService<IConfiguration>();
    return EventApi.NHibernateHelper.SessionFactory(configuration);
});

builder.Services.AddScoped(factory =>
    factory.GetRequiredService<ISessionFactory>().OpenSession()
);

builder.Services.AddScoped<EventApi.Repositories.Interfaces.IEventRepository, EventApi.Repositories.EventRepository>();
builder.Services.AddScoped<EventApi.Services.Interfaces.IEventService, EventApi.Services.EventService>();

builder.WebHost.UseUrls("http://0.0.0.0:80");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();