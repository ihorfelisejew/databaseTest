﻿
using DatabaseTest.Database.Entities;

namespace EFCoreProject.Services.CheckServices
{
    public interface ICheckService
    {
        void Create(CheckEntity buyerEntity);
        CheckEntity GetById(long id);
        List<ProductEntity> GetProductsByCheckId(long checkId);
        bool Update(CheckEntity userEntity);
    }
}
