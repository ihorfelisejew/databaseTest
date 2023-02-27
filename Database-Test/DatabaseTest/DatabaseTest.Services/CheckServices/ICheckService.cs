
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.CheckServices
{
    public interface ICheckService
    {
        void Create(CheckEntity buyerEntity);
        CheckEntity GetById(long id);
        CheckEntity GetByBuyerFK(long buyerFK);
        bool Update(CheckEntity userEntity);
    }
}
