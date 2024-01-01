using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestaurantService.Data;
using RestaurantService.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("restaurantdb"));
}
else
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Connection string was not found.");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(opt =>
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = nameof(RestaurantService),
        Version = "v1"
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using IServiceScope scope = app.Services.CreateScope();
AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (context.Database.IsRelational())
{
    context.Database.Migrate();
}

app.Run();
