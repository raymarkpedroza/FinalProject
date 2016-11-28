using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class FriendRepository : Repository<FRIEND>, IFriendRepository
    {
        public List<FRIEND> GetListOfFriendRequest(int id)
        {
            using (var context = new PastebookEntities())
            {
                return context.FRIENDs
                    .Include(friend => friend.USER)
                    .Include(friend => friend.USER1)
                    .Where(friend => friend.USER_ID == id || friend.FRIEND_ID == id)
                    .ToList();
            }
        }

        public List<FRIEND> GetListOfFriend(int id)
        {
            using (var context = new PastebookEntities())
            {
                return context.FRIENDs
                    .Include(friend => friend.USER)
                    .Include(friend => friend.USER1)
                    .Where(friend => friend.USER_ID == id && friend.REQUEST == "Y")
                    .OrderBy(friend => friend.USER1.FIRST_NAME)
                    .ToList();
            }
        }

    }
}
