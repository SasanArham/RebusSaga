
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System.Reflection;

namespace Saga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var messgaeBroakerConnectionString = builder.Configuration.GetConnectionString("MessageBroaker");
            var connectionString = builder.Configuration.GetConnectionString("connectionstring");

            builder.Services.AddRebus(rebus => rebus
                .Routing(r =>
                    r.TypeBased().MapAssemblyOf<Program>("saga-queue"))
                .Transport(tr =>
                    tr.UseRabbitMq(messgaeBroakerConnectionString, "saga-queue"))
                .Sagas(s => s.StoreInPostgres(connectionString, "sagas", "saga_indexes")));



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