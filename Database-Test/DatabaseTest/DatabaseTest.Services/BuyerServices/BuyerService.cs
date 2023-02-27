using DatabaseTest.Database.Entities;
using DatabaseTest.Database.GenericReposotiry;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.BuyerServices
{
    public class BuyerService : IBuyerService
    {
        private readonly IGenericRepository<BuyerEntity> _genericRepository;
        public BuyerService(IGenericRepository<BuyerEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void Create(BuyerEntity buyerEntity)
        {
            _genericRepository.Create(buyerEntity);
        }
        public BuyerEntity GetById(long id)
        {
            BuyerEntity dbRecord = _genericRepository.Table
                .Include(buyer => buyer.Checks)
                .FirstOrDefault(buyer => buyer.Id == id)!;

            if(dbRecord == null)
                return null;

            return dbRecord;
        }

        public BuyerEntity GetByNameAndSurname(string name, string surname)
        {
            BuyerEntity dbRecord = _genericRepository.Table
                .Where(buyer => buyer.Name == name && 
                    buyer.Surname == surname)
                .Include(buyer => buyer.Checks)
                .ThenInclude(check => check.Products)
                .FirstOrDefault()!;

            if (dbRecord == null)
                return null;

            return dbRecord;
        }

        public int GetCheckCounts(string name, string surname)
        {
            BuyerEntity buyer = GetByNameAndSurname(name, surname);
            if (buyer == null)
                return 0;
            else
                return buyer.Checks.Count;
        }

        public CheckEntity GetCheckFromBuyerCheckList(string name, string surname, int nuberCheckInList)
        {
            BuyerEntity buyer = GetByNameAndSurname(name, surname);
            if (buyer == null)
                return null;
            return buyer.Checks.ToList()[nuberCheckInList-1];
        }

        public List<ProductEntity> GetBuyedProducts(string name, string surname, int nuberCheckInList)
        {
            BuyerEntity buyer = GetByNameAndSurname(name, surname);
            if (buyer == null)
                return null;
            return buyer.Checks.ToList()[nuberCheckInList-1]
                .Products.ToList();
        }

        public bool Update(BuyerEntity buyerEntity)
        {
            try
            {
                _genericRepository.Table.Update(buyerEntity);
                _genericRepository.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<BuyerEntity> GetAllBuyers()
        {
            List<BuyerEntity> buyers = _genericRepository.Table
                .Include(buyers => buyers.Checks)
                .ThenInclude(check => check.Products)
                .ToList();

            return buyers;
        }
    }
}
