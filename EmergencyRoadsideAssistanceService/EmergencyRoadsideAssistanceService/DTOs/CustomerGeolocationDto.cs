using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CustomerGeolocationDto
    {

        public Customer? Customer { get; set; }

        public Geolocation? Geolocation { get; set; }

    }
}
