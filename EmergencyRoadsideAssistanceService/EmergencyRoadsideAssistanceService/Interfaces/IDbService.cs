namespace EmergencyRoadsideAssistanceService.Interfaces
{
    public interface IDbService<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T?> GetById(int id);

        Task Create(T model);

        Task Update(T model);

        Task Delete(int id);
    }
}
