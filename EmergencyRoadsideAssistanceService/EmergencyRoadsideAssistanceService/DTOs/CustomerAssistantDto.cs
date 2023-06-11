using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CustomerAssistantDto
    {

        public Customer? Customer { get; set; }

        public Assistant? Assistant { get; set; }

    }
}
