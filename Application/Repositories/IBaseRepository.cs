using Domain;

namespace Application.Repositories
{
  public interface IBaseRepository<T> where T : class
  {
    Task<List<T>> GetAll();
    void Add(T item);
    Task SaveChangesAsync();
  }
}