using System.Diagnostics.CodeAnalysis;

namespace EmergencyRoadsideAssistanceService.Models
{
    [ExcludeFromCodeCoverage]
    public class AssistantCompanyModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string AssistantCompanyType => "Roadside Assistance Company";

        public string? AssistantCompanyDescription { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; } = "USA";

        public double BaseLatitude { get; set; }

        public double BaseLongitude { get; set; }

        public string? SecondaryPhoneNumber { get; set; }

        public string? FaxNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }


    }
}
