using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.DTOs
{

    [ExcludeFromCodeCoverage]
    public class AssistantGeolocationDto
    {

        public Assistant? Assistant { get; set; }

        public Geolocation? Geolocation { get; set; }

    }
}
