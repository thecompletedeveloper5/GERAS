using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.DTOs
{
    [ExcludeFromCodeCoverage]
    public class Customer
    {

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPhoneNumber { get; set; }

        public string? CustomerEmail { get; set; }


    }
}
