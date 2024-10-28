using Demo.DTO;

namespace Demo.Definitions.DataAccess
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        
        Task<ProductDTO> GetByIdAsync(int id);
        
        Task AddAsync(ProductDTO product);
        
        Task UpdateAsync(ProductDTO product);

        Task DeleteAsync(int id);
    }
}
