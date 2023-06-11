using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.Models
{
    [ExcludeFromCodeCoverage]
    public class CustomerModel
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

    }
}
