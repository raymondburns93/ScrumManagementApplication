using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ScrumManagementApp.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ScrumManagementAppContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        /// <summary>
        /// Author : Raymond Burns
        /// Description : Retrieves list of projects that match the where clause with optional navigation properties included 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ScrumManagementAppContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {

            T item = null;
            using (var context = new ScrumManagementAppContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                // var a = System.AppDomain.CurrentDomain.BaseDirectory;
                // var v = AppDomain.CurrentDomain.GetData("DataDirectory");

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }


        public virtual void Add(params T[] items)
        {
            using (var context = new ScrumManagementAppContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
                context.SaveChanges();
            }
        }
        public virtual void Update(params T[] items)
        {
            using (var context = new ScrumManagementAppContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new ScrumManagementAppContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
    }
}
