using EmergencyRoadsideAssistanceService.Common;
using EmergencyRoadsideAssistanceService.DTOs;
using EmergencyRoadsideAssistanceService.Helpers;
using EmergencyRoadsideAssistanceService.Interfaces;
using EmergencyRoadsideAssistanceService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyRoadsideAssistanceService.Services
{
    public class RoadsideAssistanceService : IRoadsideAssistanceService
    {
        private readonly IDbService<AssistantTruckModel> _assistantDbService;
        private readonly IDbService<CustomerModel> _customerDbService;

        public RoadsideAssistanceService(IDbService<AssistantTruckModel> assistantDbService, IDbService<CustomerModel> customerDbService)
        {
            _assistantDbService = assistantDbService;
            _customerDbService = customerDbService;
        }

        public async Task<SortedSet<Assistant>> FindNearestAssistants(Geolocation geolocation, int limit)
        {
            var assistants = await _assistantDbService.GetAll()
                .Where(x => x.ReservedByCustomerId == 0) //Get only the non-reserved ones here
                .Select(x =>
                new Assistant
                {
                    AssistantId = x.Id,
                    AssistantDescription = x.AssistantTruckDescription,
                    AssistantName = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    ClosenessIndex = Math.Abs(x.CurrentLatitude - geolocation.Latitude) + Math.Abs(x.CurrentLongitude - geolocation.Longitude)
                }).OrderBy(x => x.ClosenessIndex).ToListAsync();

            if (assistants == null || !assistants.Any())
            {
                return new SortedSet<Assistant>();
            }
            else if (assistants.Count == 1)
            {
                return new SortedSet<Assistant>
                {
                    assistants.First()
                };
            }

            var sortedResults = new SortedSet<Assistant>(assistants, new AssistantComparer());
            return new SortedSet<Assistant>(sortedResults.Take(limit), new AssistantComparer());
        }

        public async Task ReleaseAssistant(Customer customer, Assistant assistant)
        {
            var mdlAssistant = await _assistantDbService.GetById(assistant.AssistantId);
            if (mdlAssistant == null)
            {
                throw new ApplicationException($"Cannot find Assistant by id: {assistant.AssistantId}");
            }

            if (mdlAssistant.ReservedByCustomerId != customer.CustomerId)
            {
                throw new ApplicationException($"Customer {customer.CustomerName} - {customer.CustomerId} has not reserved {mdlAssistant.Name} - {mdlAssistant.Id}");
            }

            mdlAssistant.ReservedByCustomerId = 0;
            await _assistantDbService.Update(mdlAssistant);
        }

        public async Task<Optional<Assistant>> ReserveAssistant(Customer customer, Geolocation customerLocation)
        {

            var existingCustomer = await _customerDbService.GetById(customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new ApplicationException($"Customer does not exist with {customer.CustomerName} - {customer.CustomerId}");
            }

            var alreadyReservedAssistant = _assistantDbService.GetAll().FirstOrDefault(x => x.ReservedByCustomerId == customer.CustomerId);
            if (alreadyReservedAssistant != null)
            {
                throw new ApplicationException($"Customer {customer.CustomerName} - {customer.CustomerId} has already reserved assistant {alreadyReservedAssistant.Name} - {alreadyReservedAssistant.Id} !");
            }

            var lstAssistant = await FindNearestAssistants(customerLocation, 1);
            if (lstAssistant == null || !lstAssistant.Any())
            {
                return Optional<Assistant>.CreateEmpty();
            }

            var availableAssistant = lstAssistant.First();

            var assistant = await _assistantDbService.GetById(availableAssistant.AssistantId);
            assistant.ReservedByCustomerId = customer.CustomerId;
            await _assistantDbService.Update(assistant);

            return Optional<Assistant>.Create(availableAssistant);

        }

        public async Task UpdateAssistantLocation(Assistant assistant, Geolocation assistantLocation)
        {
            var mdlAssistant = await _assistantDbService.GetById(assistant.AssistantId);
            if (mdlAssistant == null)
            {
                throw new ApplicationException($"Cannot find Assistant by id: {assistant.AssistantId}");
            }

            mdlAssistant.CurrentLatitude = assistantLocation.Latitude;
            mdlAssistant.CurrentLongitude = assistantLocation.Longitude;
            await _assistantDbService.Update(mdlAssistant);

        }
    }
}
