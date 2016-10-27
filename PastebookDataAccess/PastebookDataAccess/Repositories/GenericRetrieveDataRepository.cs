﻿using PastebookEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess.Repositories
{
    public class GenericRetrieveDataRepository<T> : IGenericRetrieveDataRepository<T> where T : class
    {
        public List<T> RetrieveAllRecords(Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> records = new List<T>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    records = dbQuery.ToList<T>();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return records;
        }

        public List<T> RetrieveList(Func<T, bool> where, Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> recordList = new List<T>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    recordList = dbQuery.Where(where).ToList<T>();
                }
            }

            catch (Exception)
            {
                throw;
            }

            return recordList;
        }

        public T RetrieveSpecificRecord(Func<T, bool> where, Expression<Func<T, object>>[] navigationProperties)
        {
            T record = null;

            using (var context = new PASTEBOOKEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                record = dbQuery.FirstOrDefault(where);
            }

            return record;
        }
    }
}