// -----------------------------------------------------------------------
// <copyright file="IDBContext.cs" company="Logiciells">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Skarpline.PersistenceLayer.Repository.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IDataContext : IDisposable
    {
        DbSet<T> GetEntitySet<T>() where T : class;
        int SaveChanges();
        DbEntityEntry GetEntityEntry(object entity);
        Database GetDatabase();
    }
}