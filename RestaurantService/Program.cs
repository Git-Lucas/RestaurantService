using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestaurantService.Data;
using RestaurantService.ItemServiceCommunication;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Connection string was not found.");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddHttpClient<IItemServiceHttpClient, ItemServiceHttpClient>();
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
context.Database.Migrate();

app.Run();
