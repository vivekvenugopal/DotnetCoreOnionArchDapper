using Demo.DTO; 

namespace Demo.Definitions.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllAsync();

        Product GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);
    }
}
