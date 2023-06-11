using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.Models
{
    [ExcludeFromCodeCoverage]
    public class AssistantTruckModel
    {

        public int Id { get; set; }

        public int AssistantCompanyId { get; set; }

        public string? Name { get; set; }

        public string AssistantTruckDescription { get; set; } = "Standard Assistant Truck";

        public double CurrentLatitude { get; set; }

        public double CurrentLongitude { get; set; }

        public string? PhoneNumber { get; set; }

        public int ReservedByCustomerId { get; set; }

    }
}
