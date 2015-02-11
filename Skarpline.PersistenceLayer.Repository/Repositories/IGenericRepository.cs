﻿// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Logiciells">
// </copyright>
// -----------------------------------------------------------------------

#region Using directives

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Skarpline.PersistenceLayer.Repository.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Save entity to the repository
        ///</summary>
        /// <param name="entity">The entity to save</param>
        bool Save(TEntity entity);

        ///<summary>
        /// Update entity to the repository
        ///</summary>
        /// <param name="entity">The entity to update</param>
        bool Update(TEntity entity);

        /// <summary>
        /// Save and update list of entity to the repository
        /// </summary>
        /// <param name="list">list to save and update</param>
        /// <returns>returns true if updated successfully, false otherwise</returns>
        bool SaveAndUpdateList(IList<TEntity> list);

        ///<summary>
        /// Mark entity to be deleted within the repository
        ///</summary>
        /// <param name="entity">The entity to delete</param>
        bool Delete(TEntity entity);

        /// <summary>
        /// This method is used to get the list of available records in entity table from database
        /// </summary>
        /// <returns>returns list of entities</returns>
        IQueryable<TEntity> GetList();

        /// <summary>
        /// Gets the list of records on the basis of the criteria expression passed
        /// </summary>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <returns>returns list of entities</returns>
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> lambdaExpression);

        /// <summary>
        /// Gets the list of records with name of table which need to be included
        /// </summary>
        /// <param name="includeTableName">name of table to include</param>
        /// <returns>returns list of entity</returns>
        IQueryable<TEntity> GetList(string includeTableName);

        /// <summary>
        /// Gets the list of records on the basis of the criteria expression passed and name of table to include
        /// </summary>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <param name="includeTableName">name of table to include</param>
        /// <returns>returns list of entities</returns>
        IQueryable<TEntity> GetList(string includeTableName, Expression<Func<TEntity, Boolean>> lambdaExpression);

        /// <summary>
        /// This method is used to get the list of records on the basis of the criteria expression passed
        /// </summary>
        /// <param name="lambdaExpression">lambda expression for where clause</param>
        /// <param name="orderByexpr"> </param>
        /// <param name="orderByDesc"> </param>
        /// <returns>
        /// returns list of entities
        /// </returns>
        IQueryable<TEntity> GetList(Expression<Func<TEntity, Boolean>> lambdaExpression, Expression<Func<TEntity, int>> orderByexpr, bool orderByDesc = false);

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        TEntity GetSingle(Expression<Func<TEntity, bool>> whereCondition);

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="includeTableName">name of table to include.</param>
        /// <param name="lambdaExpression">lambda expression for where clause.</param>
        /// <returns></returns>
        TEntity GetSingle(string includeTableName, Expression<Func<TEntity, Boolean>> lambdaExpression);

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <returns></returns>
        TEntity GetSingle();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// This method is used to count total records in entity collection on the basis of the lambda expression
        /// </summary>
        /// <param name="lambdaExpression">lambda expression</param>
        /// <returns>returns count</returns>
        int Count(Expression<Func<TEntity, bool>> lambdaExpression);

        /// <summary>
        /// This method is used to count total records in entity collection on the basis of the lambda expression
        /// </summary>
        /// <param name="includeTableName">comma seprated list of tables to include</param>
        /// <param name="lambdaExpression">lambda expression</param>
        /// <returns>returns count</returns>
        int Count(string includeTableName, Expression<Func<TEntity, Boolean>> lambdaExpression);

        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <param name="whereCondition">where condition</param>
        /// <returns>the loaded entity</returns>
        TEntity Load(Expression<Func<TEntity, bool>> whereCondition);

        /// <summary>
        /// Loads the list.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        IQueryable<TEntity> LoadList(Expression<Func<TEntity, bool>> whereCondition);

        Task<List<TEntity>> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}