
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.BuyerServices
{
    public interface IBuyerService
    {
        void Create(BuyerEntity buyerEntity);
        BuyerEntity GetById(long id);
        BuyerEntity GetByNameAndSurname(string name, string surname);
        bool Update(BuyerEntity userEntity);
        List<ProductEntity> GetProductsByBuyerCheckList(string name, string surname, int nuberCheckInList);
        int GetCheckCounts(string name, string surname);
        CheckEntity GetCheckFromBuyerCheckList(string name, string surname, int nuberCheckInList);
    }
}
