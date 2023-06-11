using EmergencyRoadsideAssistanceService.Interfaces;
using EmergencyRoadsideAssistanceService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyRoadsideAssistanceService.Helpers
{
    public class SampleDataContext : DbContext
    {

        protected readonly IConfiguration _configuration;
        public SampleDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //using inmemory db for POC - change to a real DB for PROD
            optionsBuilder.UseInMemoryDatabase("EmergencyRoadsideAssistanceService");
        }

        public DbSet<AssistantTruckModel> AssistantTruckModels { get; set; }

        public DbSet<CustomerModel> CustomerModels { get; set; }

        public DbSet<AssistantCompanyModel> AssistantCompanyModels { get; set; }

    }
}
