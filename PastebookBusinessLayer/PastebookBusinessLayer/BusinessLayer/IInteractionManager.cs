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
        PASTEBOOK_LIKE RetrieveLikedPost(int postId);

        bool CommentOnPost(PASTEBOOK_COMMENT comment);

        bool AddFriendRequest(PASTEBOOK_FRIEND friendRequest);
        bool UpdateFriendRequest(PASTEBOOK_FRIEND friendRequest);
        bool BlockUser(PASTEBOOK_FRIEND friendRequest);
        List<PASTEBOOK_FRIEND> RetrieveFriends(int userId);
    }
}
