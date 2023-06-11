
using EmergencyRoadsideAssistanceService.Helpers;
using EmergencyRoadsideAssistanceService.Interfaces;
using EmergencyRoadsideAssistanceService.Models;
using EmergencyRoadsideAssistanceService.Services;
using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService
{
    [ExcludeFromCodeCoverage]
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

            builder.Services.AddScoped<IRoadsideAssistanceService, RoadsideAssistanceService>();
            builder.Services.AddScoped<IDbService<CustomerModel>, CustomerDbService>();
            builder.Services.AddScoped<IDbService<AssistantTruckModel>, AssistantTruckDbService>();

            builder.Services.AddScoped<SampleDataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SampleDataContext>();
            AddTestData(context);

            app.MapControllers();

            app.Run();
        }

        private static void AddTestData(SampleDataContext context)
        {
            context.AssistantCompanyModels.AddRange(
                new List<AssistantCompanyModel>
                {
                    new AssistantCompanyModel
                    {
                        Address="123 Street address",
                        AssistantCompanyDescription="Some description",
                        BaseLatitude= 10.35,
                        BaseLongitude=15.25,
                        City="SomeCity",
                        Country="USA",
                        Email="test@abc.com",
                        FaxNumber="1112223334",
                        Id=1,
                        Name="Test Company",
                        PhoneNumber="112233445",
                        PostalCode="11111",
                        SecondaryPhoneNumber="999888777",
                        State="SS"
                    }
                });

            context.AssistantTruckModels.AddRange(
                new List<AssistantTruckModel>
                {
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 1",
                        CurrentLatitude=45.04,
                        CurrentLongitude=78.25,
                        Id=1,
                        Name="Truck1",
                        PhoneNumber="1111111111",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 2",
                        CurrentLatitude=54.04,
                        CurrentLongitude=89.25,
                        Id=2,
                        Name="Truck2",
                        PhoneNumber="2222222222",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 3",
                        CurrentLatitude=64.04,
                        CurrentLongitude=29.25,
                        Id=3,
                        Name="Truck3",
                        PhoneNumber="3333333333",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 4",
                        CurrentLatitude=74.04,
                        CurrentLongitude=39.25,
                        Id=4,
                        Name="Truck4",
                        PhoneNumber="4444444444",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 5",
                        CurrentLatitude=84.04,
                        CurrentLongitude=49.25,
                        Id=5,
                        Name="Truck5",
                        PhoneNumber="5555555555",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 6",
                        CurrentLatitude=94.04,
                        CurrentLongitude=59.25,
                        Id=6,
                        Name="Truck6",
                        PhoneNumber="6666666666",
                        ReservedByCustomerId=0
                    },
                    new AssistantTruckModel
                    {
                        AssistantCompanyId=1,
                        AssistantTruckDescription="Test assistant truck description 7",
                        CurrentLatitude=14.04,
                        CurrentLongitude=19.25,
                        Id=7,
                        Name="Truck7",
                        PhoneNumber="7777777777",
                        ReservedByCustomerId=1
                    }
                });

            context.CustomerModels.AddRange(new List<CustomerModel>
            {
                new CustomerModel
                {
                    Address="Address 1",
                    City="City 1",
                    Country="USA",
                    Email="email1@abc.com",
                    Id=1,
                    Name="Name 1",
                    PhoneNumber="111112222",
                    PostalCode="12345",
                    State="TX"
                },
                new CustomerModel
                {
                    Address="Address 2",
                    City="City 2",
                    Country="USA",
                    Email="email2@abc.com",
                    Id=2,
                    Name="Name 2",
                    PhoneNumber="222223333",
                    PostalCode="54321",
                    State="CA"
                }
            });

            context.SaveChanges();
        }
    }
}