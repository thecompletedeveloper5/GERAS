using EmergencyRoadsideAssistanceService.Helpers;
using EmergencyRoadsideAssistanceService.Interfaces;
using EmergencyRoadsideAssistanceService.Models;

namespace EmergencyRoadsideAssistanceService.Services
{
    public class AssistantTruckDbService : IDbService<AssistantTruckModel>
    {

        private readonly SampleDataContext _sampleDataContext;

        public AssistantTruckDbService(SampleDataContext sampleDataContext)
        {
            _sampleDataContext = sampleDataContext;
        }

        public async Task Create(AssistantTruckModel model)
        {
            _sampleDataContext.AssistantTruckModels.Add(model);
            await _sampleDataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var assistant = await GetById(id);
            if (assistant == null)
            {
                throw new ApplicationException($"Cannot delete {nameof(AssistantTruckModel)} - no entity found !");
            }
            _sampleDataContext.AssistantTruckModels.Remove(assistant);
            await _sampleDataContext.SaveChangesAsync();
        }

        public IQueryable<AssistantTruckModel> GetAll()
        {
            return _sampleDataContext.AssistantTruckModels;
        }

        public async Task<AssistantTruckModel?> GetById(int id)
        {
            var result = _sampleDataContext.AssistantTruckModels.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(result);
        }

        public async Task Update(AssistantTruckModel model)
        {
            var assistant = await GetById(model.Id);

            if (assistant == null)
            {
                throw new ApplicationException($"Cannot update {nameof(AssistantTruckModel)} - no entity found !");
            }

            assistant.CurrentLatitude = model.CurrentLatitude;
            assistant.CurrentLongitude = model.CurrentLongitude;
            assistant.AssistantTruckDescription = model.AssistantTruckDescription;
            assistant.PhoneNumber = model.PhoneNumber;
            assistant.ReservedByCustomerId = model.ReservedByCustomerId;
            assistant.Name = model.Name;

            _sampleDataContext.AssistantTruckModels.Update(assistant);
            await _sampleDataContext.SaveChangesAsync();

        }

    }
}
