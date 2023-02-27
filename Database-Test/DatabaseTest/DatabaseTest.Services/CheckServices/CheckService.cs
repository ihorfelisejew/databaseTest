using DatabaseTest.Database.Entities;
using DatabaseTest.Database.GenericReposotiry;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.CheckServices
{
    public class CheckService : ICheckService
    {
        private readonly IGenericRepository<CheckEntity> _genericRepository;

        public CheckService(IGenericRepository<CheckEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Create(CheckEntity checkEntity)
        {
            _genericRepository.Create(checkEntity);
        }
        public CheckEntity GetById(long id)
        {
            CheckEntity dbRecord = _genericRepository.Table
                .Include(check => check.Products)
                .FirstOrDefault(check => check.Id == id)!;

            if(dbRecord == null)
                return null;

            return dbRecord;
        }

        public CheckEntity GetByBuyerFK(long buyerFK)
        {
            CheckEntity dbRecord = _genericRepository.Table
                .Where(check => check.BuyerFK == buyerFK)
                .FirstOrDefault()!;

            if (dbRecord == null)
                return null;

            return dbRecord;
        }

        public bool Update(CheckEntity checkEntity)
        {
            try
            {
                _genericRepository.Table.Update(checkEntity);
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
