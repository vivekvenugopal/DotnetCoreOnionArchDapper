using AutoMapper;
using Demo.Definitions.DataAccess;
using Demo.Definitions.Service;
using Demo.DTO;

namespace Demo.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAllAsync() 
        {
            return _mapper.Map<IEnumerable<Product>>(_productRepository.GetAllAsync());
        }

        public Product GetByIdAsync(int id) {
            
           return _mapper.Map<Product>(_productRepository.GetByIdAsync(id)); 
        
        }

        public Task AddAsync(Product product)
        {
           return _productRepository.AddAsync(_mapper.Map<ProductDTO>(product));
        }

        public Task UpdateAsync(Product product)
        {
           return _productRepository.UpdateAsync(_mapper.Map<ProductDTO>(product));
        }

        public Task DeleteAsync(int id) => _productRepository.DeleteAsync(id);
    }
}
