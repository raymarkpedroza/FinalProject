using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PastebookDataAccess
{
    public class FriendRepository : Repository<PASTEBOOK_FRIEND>, IFriendRepository
    {
        public List<PASTEBOOK_FRIEND> GetListOfFriendRequest(int id)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_FRIEND
                    .Include(friend => friend.PASTEBOOK_USER)
                    .Include(friend => friend.PASTEBOOK_USER1)
                    .Where(friend => friend.USER_ID == id || friend.FRIEND_ID == id)
                    .ToList();
            }
        }

        public List<PASTEBOOK_FRIEND> GetListOfFriend(int id)
        {
            using (var context = new PASTEBOOKEntities())
            {
                return context.PASTEBOOK_FRIEND
                    .Include(friend => friend.PASTEBOOK_USER)
                    .Include(friend => friend.PASTEBOOK_USER1)
                    .Where(friend => friend.USER_ID == id && friend.REQUEST == "Y")
                    .ToList();
            }
        }

    }
}
