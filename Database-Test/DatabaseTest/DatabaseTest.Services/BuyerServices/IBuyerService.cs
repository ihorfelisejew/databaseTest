
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.BuyerServices
{
    public interface IBuyerService
    {
        void Create(BuyerEntity buyerEntity);
        BuyerEntity GetById(long id);
        BuyerEntity GetByNameAndSurname(string name, string surname);
        bool Update(BuyerEntity userEntity);
        List<ProductEntity> GetProductsByCheckId(string name, string surname, int nuberCheckInList);
    }
}
