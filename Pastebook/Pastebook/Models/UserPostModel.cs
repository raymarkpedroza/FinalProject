using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserPostModel
    {
        public PostModel Post = new PostModel();
        public UserModel Poster = new UserModel();
        public UserModel ProfileOwner = new UserModel();
        public List<UserCommentModel> ListOfCommentsWithCommenters = new List<UserCommentModel>();
        public List<LikeModel> ListOfLikes = new List<LikeModel>();
    }
}