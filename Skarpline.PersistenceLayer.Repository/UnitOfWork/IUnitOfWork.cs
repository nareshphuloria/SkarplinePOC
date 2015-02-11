// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Logiciells">
// </copyright>
// -----------------------------------------------------------------------

namespace Skarpline.PersistenceLayer.Repository.UnitOfWork
{
    #region Using directives

    using System;
    using System.Data;
    using Skarpline.PersistenceLayer.Repository.Repositories;

    #endregion

    public interface IUnitOfWork : IDisposable
    {
        int Save();
        void Dispose(bool disposing);
        void CommitTransaction();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Rollback();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}