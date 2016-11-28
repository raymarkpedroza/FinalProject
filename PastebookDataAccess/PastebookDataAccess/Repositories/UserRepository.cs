using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class UserRepository : Repository<USER>, IUserRepository
    {
        public List<USER> GetUserWithCountry(Func<USER, bool> predicate)
        {
            using (var context = new PastebookEntities())
            {
                return context.USERs
                    .Include(user=>user.REF_COUNTRY)
                    .Where(predicate)
                    .ToList();
            }
        }
    }
}
