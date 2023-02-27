
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.ProductServices
{
    public interface IProductService
    {
        void Create(ProductEntity buyerEntity);
        ProductEntity GetById(long id);
        ProductEntity GetByName(string name);
        bool Update(ProductEntity productEntity);
        List<ProductEntity> GetAllNoBuy();

        List<ProductEntity> GetAllProducts();
    }
}
