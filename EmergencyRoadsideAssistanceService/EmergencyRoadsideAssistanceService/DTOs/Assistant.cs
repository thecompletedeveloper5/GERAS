
using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.DTOs
{
    [ExcludeFromCodeCoverage]
    public class Assistant
    {

        public int AssistantId { get; set; }

        public string? AssistantName { get; set; }

        public string? AssistantDescription { get; set; }

        public string AssistantType => "Roadside Assistance";

        public string? PhoneNumber { get; set; }

        public double ClosenessIndex { get; set; }

    }
}
