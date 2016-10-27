using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public interface IInteractionManager
    {
        bool LikePost(PASTEBOOK_LIKE like);
        bool UnlikePost(PASTEBOOK_LIKE like);
        PASTEBOOK_LIKE RetrieveLike(int likeId);
        List<PASTEBOOK_LIKE> RetrieveLikes(int postId);

        bool CommentOnPost(PASTEBOOK_COMMENT comment);

        bool AddFriendRequest(PASTEBOOK_FRIEND friendRequest);
        bool UpdateFriendRequest(PASTEBOOK_FRIEND friendRequest);
        bool RejectFriendRequest(PASTEBOOK_FRIEND friendRequest);
        bool BlockUser(PASTEBOOK_FRIEND friendRequest);
        PASTEBOOK_FRIEND RetrieveFriendRequest(int friendRequestId);
        List<PASTEBOOK_FRIEND> RetrieveFriends(int userId, out List<int> listOfFriendsId);
    }
}
