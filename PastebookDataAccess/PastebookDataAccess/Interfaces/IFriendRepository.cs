using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IFriendRepository:IRepository<PASTEBOOK_FRIEND>
    {
        List<PASTEBOOK_FRIEND> GetListOfFriend(int id);
        List<PASTEBOOK_FRIEND> GetListOfFriendRequest(int id);
    }
}
