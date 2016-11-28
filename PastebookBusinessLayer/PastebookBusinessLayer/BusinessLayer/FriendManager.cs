using PastebookDataAccess;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class FriendManager
    {
        IFriendRepository _friendRepository;

        public FriendManager() {
            _friendRepository = new FriendRepository();
        }

        public bool AddFriendRequest(FRIEND friendRequest)
        {
            return _friendRepository.Create(friendRequest);
        }

        public bool RejectFriendRequest(FRIEND friendRequest)
        {
            return _friendRepository.Delete(friendRequest);
        }

        public bool AcceptFriendRequest(FRIEND friendRequest)
        {
            return _friendRepository.Update(friendRequest);
        }

        public FRIEND GetFriendRequest(int id)
        {
            return _friendRepository.Get(id);
        }

        public List<FRIEND> GetFriendList(int id)
        {
            return _friendRepository.GetListOfFriend(id);
        }

        public List<FRIEND> GetListOfFriendRequest(int id)
        {
            return _friendRepository.GetListOfFriendRequest(id);
        }
    }
}
