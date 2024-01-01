using ItemService.Data;
using ItemService.Event;
using ItemService.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(opt =>
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = nameof(ItemService),
        Version = "v1"
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
