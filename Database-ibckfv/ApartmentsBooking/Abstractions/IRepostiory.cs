namespace ApartmentsBooking.Data;

public interface IRepostiory<T> : IDisposable where T: class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    Task CreateAsync(T item); 
    Task UpdateAsync(T item); 
    Task DeleteAsync(int id); 
    Task SaveAsync(); 
}