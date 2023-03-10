using Domain;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        void Add(T item);
        Task Update(T item);
        Task Delete(Guid id);
        Task SaveChangesAsync();
    }
}