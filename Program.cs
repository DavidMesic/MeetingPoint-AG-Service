using MeetingPoint_AG_Service.Models;
using MeetingRoomReservation.API.Services;

namespace MeetingPoint_AG_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Swagger hinzufügen
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Konfiguration der MongoDB-Einstellungen aus appsettings.json
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDB"));

            // Registrierung der Services für den Datenbankzugriff
            builder.Services.AddSingleton<RoomService>();
            builder.Services.AddSingleton<ReservationService>();

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
        }
    }
}