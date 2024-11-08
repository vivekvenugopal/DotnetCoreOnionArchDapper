using AutoMapper;
using Demo.Definitions.DataAccess;
using Demo.Definitions.Service;
using Demo.DTO;
using Demo.Common;

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

        public async Task<IEnumerable<Product>> GetAllAsync() 
        {
            var products = await  _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<Product>(product);
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
