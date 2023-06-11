using EmergencyRoadsideAssistanceService.Helpers;
using EmergencyRoadsideAssistanceService.Interfaces;
using EmergencyRoadsideAssistanceService.Models;

namespace EmergencyRoadsideAssistanceService.Services
{
    public class CustomerDbService : IDbService<CustomerModel>
    {
        private readonly SampleDataContext _sampleDataContext;

        public CustomerDbService(SampleDataContext sampleDataContext)
        {
            _sampleDataContext = sampleDataContext;
        }

        public async Task Create(CustomerModel model)
        {
            _sampleDataContext.CustomerModels.Add(model);
            await _sampleDataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var customer = await GetById(id);
            if (customer == null)
            {
                throw new ApplicationException($"Cannot delete {nameof(CustomerModel)} - no entity found !");
            }
            _sampleDataContext.CustomerModels.Remove(customer);
            await _sampleDataContext.SaveChangesAsync();
        }

        public IQueryable<CustomerModel> GetAll()
        {
            return _sampleDataContext.CustomerModels;
        }

        public async Task<CustomerModel?> GetById(int id)
        {
            var result = _sampleDataContext.CustomerModels.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(result);
        }

        public async Task Update(CustomerModel model)
        {
            var customer = await GetById(model.Id);

            if (customer == null)
            {
                throw new ApplicationException($"Cannot update {nameof(CustomerModel)} - no entity found !");
            }

            customer.PhoneNumber = model.PhoneNumber;
            customer.Address = model.Address;
            customer.Email = model.Email;
            customer.City = model.City;
            customer.PostalCode = model.PostalCode;
            customer.Country = model.Country;
            customer.Name = model.Name;
            customer.State = model.State;

        }
    }
}
