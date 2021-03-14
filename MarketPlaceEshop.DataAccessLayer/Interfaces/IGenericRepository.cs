using MarketPlaceEshop.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        // جهت دریافت کلیه کوئری های مربوط به عملیات جستجو، فیلتر کردن داده ها و غیره
        IQueryable<TEntity> GetQuery();
        // ********************************************************************************
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEntityById(long entityId);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntity(long entityId);

        // جهت حذف فیزیکی رکورد در پایگاه داده
        void DeletePermanent(TEntity entity);
        Task DeletePermanent(long entityId);
        Task SaveChanges();
    }
}
