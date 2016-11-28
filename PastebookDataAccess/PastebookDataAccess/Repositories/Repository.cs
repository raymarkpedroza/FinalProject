using PastebookEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public T Get(int id)
        {
            using (var context = new PastebookEntities())
            {
                return context.Set<T>().Find(id);
            }
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            using (var context = new PastebookEntities())
            {
                return context.Set<T>().Where(predicate).ToList();
            }
        }

        public List<T> GetAll()
        {
            using (var context = new PastebookEntities())
            {
                return context.Set<T>().ToList<T>();
            }
        }

        public bool Create(T record)
        {
            using (var context = new PastebookEntities())
            {
                context.Entry(record).State = EntityState.Added;
                return context.SaveChanges() != 0;
            }
        }

        public bool Update(T record)
        {
            using (var context = new PastebookEntities())
            {
                context.Entry(record).State = EntityState.Modified;
                return context.SaveChanges() != 0;
            }
        }

        public bool Delete(T record)
        {
            using (var context = new PastebookEntities())
            {
                context.Entry(record).State = EntityState.Deleted;
                return context.SaveChanges() != 0;
            } 
        }
    }
}
