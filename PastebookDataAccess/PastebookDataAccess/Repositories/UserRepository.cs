using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class UserRepository : Repository<PASTEBOOK_USER>, IUserRepository
    {
        public List<PASTEBOOK_USER> GetUserWithCountry(Func<PASTEBOOK_USER, bool> predicate)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_USER
                    .Include(user=>user.REF_COUNTRY)
                    .Where(predicate)
                    .ToList();
            }
        }
    }
}
