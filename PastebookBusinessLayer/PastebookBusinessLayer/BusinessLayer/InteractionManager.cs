using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class InteractionManager
    {
        ILikeRepository _likeRepository;
        IFriendRepository _friendRepository;
        ICommentRepository _commentRepository;

        public InteractionManager()
        {
            _likeRepository = new LikeRepository();
            _friendRepository = new FriendRepository();
            _commentRepository = new CommentRepository();
        }
        #region[Like]
        public bool Like(PASTEBOOK_LIKE like)
        {
            return _likeRepository.Create(like);
        }

        public bool Unlike(PASTEBOOK_LIKE like)
        {
            return _likeRepository.Delete(like);
        }

        public PASTEBOOK_LIKE GetLike(int id)
        {
            return _likeRepository.Get(id);
        }

        public List<PASTEBOOK_LIKE> GetLikeWithUser(Func<PASTEBOOK_LIKE, bool> predicate)
        {
            return _likeRepository.GetLikeWithUser(predicate);
        }
        #endregion

        #region[Comment]
        public bool AddComment(PASTEBOOK_COMMENT comment)
        {
            return _commentRepository.Create(comment);
        }

        public PASTEBOOK_COMMENT GetCommentWithUser(Func<PASTEBOOK_COMMENT, bool> predicate)
        {
            return _commentRepository.GetCommentWithUser(predicate);
        }

        public PASTEBOOK_COMMENT GetComment(int id)
        {
            return _commentRepository.Get(id);
        }
        #endregion

        #region[Friend]
        public bool AddFriendRequest(PASTEBOOK_FRIEND friendRequest)
        {
            return _friendRepository.Create(friendRequest);
        }

        public bool RejectFriendRequest(PASTEBOOK_FRIEND friendRequest)
        {
            return _friendRepository.Delete(friendRequest);
        }

        public bool AcceptFriendRequest(PASTEBOOK_FRIEND friendRequest)
        {
            return _friendRepository.Update(friendRequest);
        }

        public PASTEBOOK_FRIEND GetFriendRequest(int id)
        {
            return _friendRepository.Get(id);
        }

        public List<PASTEBOOK_FRIEND> GetFriendList(int id)
        {
            return _friendRepository.GetListOfFriend(id);
        }

        public List<PASTEBOOK_FRIEND> GetListOfFriendRequest(int id)
        {
            return _friendRepository.GetListOfFriendRequest(id);
        }
        #endregion
    }
}
