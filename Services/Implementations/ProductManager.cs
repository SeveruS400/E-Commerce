using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Implementations
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Products product = _mapper.Map<Products>(productDto);
            _manager.Product.CreateAsync(product);
            _manager.Save();
        }

        public async Task DeleteProduct(int id)
        {
            await _manager.Product.DeleteAsync(id);
            _manager.Save();
        }

        public Task<IEnumerable<Products>> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Task<Products>? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("Product not found!");
            return product;
        }

        public async Task<ProductDtoForUpdate> GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product = await _manager.Product.GetOneProduct(id, trackChanges);
            if (product == null)
            {
                // Eğer ürün bulunamazsa null kontrolü yapıyoruz
                throw new Exception("Product not found");
            }

            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }


        public async Task UpdateProduct(ProductDtoForUpdate productDto)
        {
            var entity = await _manager.Product.GetOneProduct(productDto.Id, true);
            entity.Id = productDto.Id;
            entity.ProductName = productDto.ProductName;
            entity.Price = productDto.Price;
            entity.CategoryId = productDto.CategoryId;
            entity.ImageUrl = productDto.ImageUrl;
            //var entity = _mapper.Map<Products>(productDto);
            //_manager.Product.UpdateProduct(entity);
            _manager.Save();
        }
    }
}
