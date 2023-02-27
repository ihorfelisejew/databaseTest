
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.BuyerServices
{
    public interface IBuyerService
    {
        void Create(BuyerEntity buyerEntity);
        List<BuyerEntity> GetAllBuyers();
        BuyerEntity GetById(long id);
        BuyerEntity GetByNameAndSurname(string name, string surname);
        bool Update(BuyerEntity userEntity);
        List<ProductEntity> GetBuyedProducts(string name, string surname, int nuberCheckInList);
        int GetCheckCounts(string name, string surname);
        CheckEntity GetCheckFromBuyerCheckList(string name, string surname, int nuberCheckInList);
    }
}
