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

        public List<PASTEBOOK_FRIEND> RetrieveFriends(int userId)
        {
            List<PASTEBOOK_FRIEND> listOfFriends = new List<PASTEBOOK_FRIEND>();

            listOfFriends = _friendRepo.RetrieveList(x=>x.FRIEND_ID == userId);
            listOfFriends.AddRange(_friendRepo.RetrieveList(x => x.USER_ID == userId));

            return listOfFriends;
        }

        public PASTEBOOK_LIKE RetrieveLikedPost(int postId)
        {
            PASTEBOOK_LIKE like = new PASTEBOOK_LIKE();
            like = _likeRepo.RetrieveSpecificRecord(x=>x.POST_ID == postId);

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
    }
}
