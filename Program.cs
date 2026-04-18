using Microsoft.EntityFrameworkCore;
using reservations_api.Data;
using reservations_api.Repositories;
using reservations_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var provider = builder.Configuration.GetValue<string>("DatabaseProvider");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (provider == "MySql")
    {
        var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else if (provider == "SqlServer")
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
        options.UseSqlServer(connectionString);
    }
    else
    {
        throw new InvalidOperationException($"El proveedor '{provider}' no está configurado o no es soportado.");
    }
});

builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();