using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IFriendRepository:IRepository<FRIEND>
    {
        List<FRIEND> GetListOfFriend(int id);
        List<FRIEND> GetListOfFriendRequest(int id);
    }
}
