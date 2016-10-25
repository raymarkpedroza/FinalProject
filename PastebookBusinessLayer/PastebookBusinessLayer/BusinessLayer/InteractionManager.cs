using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess.Repositories;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class InteractionManager : IInteractionManager
    {
        private ILikeRepository _likeRepo;
        private ICommentRepository _commentRepo;
        private IFriendRepository _friendRepo;

        public InteractionManager()
        {
            _likeRepo = new LikeRepository();
            _commentRepo = new CommentRepository();
            _friendRepo = new FriendRepository();
        }

        public bool AddFriendRequest(PASTEBOOK_FRIEND friendRequest)
        {
            bool result = false;
            result = _friendRepo.CreateRecord(friendRequest);

            return result;
        }

        public bool BlockUser(PASTEBOOK_FRIEND friendRequest)
        {
            bool result = false;
            result = _friendRepo.CreateRecord(friendRequest);

            return result;
        }

        public bool CommentOnPost(PASTEBOOK_COMMENT comment)
        {
            bool result = false;
            result = _commentRepo.CreateRecord(comment);

            return result;
        }

        public bool LikePost(PASTEBOOK_LIKE like)
        {
            bool result = false;
            result = _likeRepo.CreateRecord(like);

            return result;
        }

        public List<PASTEBOOK_FRIEND> RetrieveFriends(int userId, out List<int> listOfFriendId)
        {
            List<PASTEBOOK_FRIEND> listOfFriends = new List<PASTEBOOK_FRIEND>();
            listOfFriendId = new List<int>();
            
            listOfFriends = _friendRepo.RetrieveList(x=>x.FRIEND_ID == userId, receiver=> receiver.PASTEBOOK_USER1, sender => sender.PASTEBOOK_USER);
            listOfFriends.AddRange(_friendRepo.RetrieveList(x => x.USER_ID == userId, receiver => receiver.PASTEBOOK_USER1, sender => sender.PASTEBOOK_USER));

            foreach (var friend in listOfFriends)
            {
                if (friend.USER_ID == userId && friend.REQUEST == "Y")
                {
                    listOfFriendId.Add(friend.FRIEND_ID);
                }

                else if(friend.FRIEND_ID == userId && friend.REQUEST == "Y")
                {
                    listOfFriendId.Add(friend.USER_ID);
                }
            }

            return listOfFriends;
        }

        public PASTEBOOK_LIKE RetrieveLike(int likeId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();
            like = _likeRepo.RetrieveSpecificRecord(x=>x.ID == likeId);

            return like;
        }

        public bool UnlikePost(PASTEBOOK_LIKE like)
        {
            bool result = false;
            result = _likeRepo.DeleteRecord(like);

            return result;
        }

        public bool UpdateFriendRequest(PASTEBOOK_FRIEND friendRequest)
        {
            bool result = false;
            result = _friendRepo.UpdateRecord(friendRequest);

            return result;
        }

        public PASTEBOOK_FRIEND RetrieveFriendRequest(int friendRequestId)
        {
            PASTEBOOK_FRIEND friendRequest = new PASTEBOOK_FRIEND();
            friendRequest = _friendRepo.RetrieveSpecificRecord(x=>x.ID.Equals(friendRequestId));

            return friendRequest;
        }

        public List<PASTEBOOK_LIKE> RetrieveLikes(int postId)
        {
            List<PASTEBOOK_LIKE> listOfLikes = new List<PASTEBOOK_LIKE>();
            listOfLikes = _likeRepo.RetrieveList(x=>x.POST_ID == postId, liker=>liker.PASTEBOOK_USER);

            return listOfLikes;
        }
    }
}
