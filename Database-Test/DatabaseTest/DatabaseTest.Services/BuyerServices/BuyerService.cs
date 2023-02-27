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
                .FirstOrDefault()!;

            if (dbRecord == null)
                return null;

            return dbRecord;
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
    }
}
