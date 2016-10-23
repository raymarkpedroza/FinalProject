using Pastebook.Models;
using PastebookDataAccess.Managers;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Managers
{
    public class _PostManager
    {
        DataAccessPostManager daPostManager = new DataAccessPostManager();
        DataAccessUserManager daUserManager = new DataAccessUserManager();
        DataAccessReactionManager daReactionManager = new DataAccessReactionManager();
        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();

        public List<UserPostModel> RetrieveTimelinePosts(int id)
        {
            List<UserPostModel> listOfPostsWithPoster = new List<UserPostModel>();
            List<PASTEBOOK_POST> listOfPosts = new List<PASTEBOOK_POST>();

            List<UserCommentModel> listOfCommentsWithCommenter;
            List<PASTEBOOK_COMMENT> listOfComment;

            List<UserLikeModel> listOfLikesWithLiker;
            List<PASTEBOOK_LIKE> listOfLikes;

            List<int> listOfFriendsId = null;

            listOfPosts = daPostManager.RetrievePosts(id, listOfFriendsId);

            foreach (var post in listOfPosts)
            {
                PASTEBOOK_USER poster = new PASTEBOOK_USER();
                poster = post.PASTEBOOK_USER;

                PASTEBOOK_USER profileOwner = new PASTEBOOK_USER();
                profileOwner = post.PASTEBOOK_USER1;

                listOfComment = new List<PASTEBOOK_COMMENT>();
                listOfComment = post.PASTEBOOK_COMMENT.ToList();

                listOfCommentsWithCommenter = new List<UserCommentModel>();
                foreach (var comment in listOfComment)
                {
                    PASTEBOOK_USER commenter = new PASTEBOOK_USER();
                    commenter = comment.PASTEBOOK_USER;

                    listOfCommentsWithCommenter.Add(new UserCommentModel() { Comment = comment, Commenter = commenter });
                }

                listOfLikes = new List<PASTEBOOK_LIKE>();
                listOfLikes = post.PASTEBOOK_LIKE.ToList();

                listOfLikesWithLiker = new List<UserLikeModel>();
                foreach (var like in listOfLikes)
                {
                    PASTEBOOK_USER liker = new PASTEBOOK_USER();
                    liker = like.PASTEBOOK_USER;

                    listOfLikesWithLiker.Add(new UserLikeModel() {  Like = like, Liker = liker });
                }

                listOfPostsWithPoster.Add(new UserPostModel() { Poster = poster, ProfileOwner = profileOwner, Post = post, ListOfCommentsWithCommenters = listOfCommentsWithCommenter, ListOfLikesWithLiker = listOfLikesWithLiker });
            }

            return listOfPostsWithPoster.OrderByDescending(x => x.Post.CREATED_DATE).ToList();
        }

        public List<UserPostModel> RetrieveNewsfeedPosts(int id)
        {
            List<UserPostModel> listOfPostsWithPoster = new List<UserPostModel>();
            List<PASTEBOOK_POST> listOfPosts = new List<PASTEBOOK_POST>();

            List<UserCommentModel> listOfCommentsWithCommenter;
            List<PASTEBOOK_COMMENT> listOfComment;

            List<UserLikeModel> listOfLikesWithLiker;
            List<PASTEBOOK_LIKE> listOfLikes;

            List<int> listOfFriendsId = new List<int>();

            List<PASTEBOOK_FRIEND> listOfFriends = new List<PASTEBOOK_FRIEND>();
            listOfFriends = daFriendManager.RetrieveFriends(id);

            foreach (var friend in listOfFriends)
            {
                if ((listOfFriendsId.Any(x => x == friend.USER_ID) || listOfFriendsId.Any(x => x == friend.FRIEND_ID)) == false)
                {
                    if (friend.USER_ID == id && friend.REQUEST == "Y")
                    {
                        listOfFriendsId.Add(friend.FRIEND_ID);
                    }

                    if (friend.FRIEND_ID == id && friend.REQUEST == "Y")
                    {
                        listOfFriendsId.Add(friend.USER_ID);
                    }
                }
            }

            listOfPosts = daPostManager.RetrievePosts(id, listOfFriendsId);

            foreach (var post in listOfPosts)
            {
                PASTEBOOK_USER poster = new PASTEBOOK_USER();
                poster = post.PASTEBOOK_USER;

                PASTEBOOK_USER profileOwner = new PASTEBOOK_USER();
                profileOwner = post.PASTEBOOK_USER1;

                listOfComment = new List<PASTEBOOK_COMMENT>();
                listOfComment = post.PASTEBOOK_COMMENT.ToList();

                listOfCommentsWithCommenter = new List<UserCommentModel>();
                foreach (var comment in listOfComment)
                {
                    PASTEBOOK_USER commenter = new PASTEBOOK_USER();
                    commenter = comment.PASTEBOOK_USER;

                    listOfCommentsWithCommenter.Add(new UserCommentModel() { Comment = comment, Commenter = commenter });
                }

                listOfLikes = new List<PASTEBOOK_LIKE>();
                listOfLikes = post.PASTEBOOK_LIKE.ToList();

                listOfLikesWithLiker = new List<UserLikeModel>();
                foreach (var like in listOfLikes)
                {
                    PASTEBOOK_USER liker = new PASTEBOOK_USER();
                    liker = like.PASTEBOOK_USER;

                    listOfLikesWithLiker.Add(new UserLikeModel() { Like = like, Liker = liker });
                }

                listOfPostsWithPoster.Add(new UserPostModel() { Poster = poster, ProfileOwner = profileOwner, Post = post, ListOfCommentsWithCommenters = listOfCommentsWithCommenter, ListOfLikesWithLiker = listOfLikesWithLiker });
            }

            return listOfPostsWithPoster.OrderByDescending(x => x.Post.CREATED_DATE).ToList();
        }

        public UserPostModel RetrievePost(int postId)
        {
            UserPostModel PostWithPoster = new UserPostModel();

            List<UserCommentModel> listOfCommentsWithCommenter;
            List<PASTEBOOK_COMMENT> listOfComment;

            List<UserLikeModel> listOfLikesWithLiker;
            List<PASTEBOOK_LIKE> listOfLikes;

            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post = daPostManager.RetrievePost(postId).SingleOrDefault();

            PASTEBOOK_USER poster = new PASTEBOOK_USER();
            poster = post.PASTEBOOK_USER;

            PASTEBOOK_USER profileOwner = new PASTEBOOK_USER();
            profileOwner = post.PASTEBOOK_USER1;

            listOfComment = new List<PASTEBOOK_COMMENT>();
            listOfComment = post.PASTEBOOK_COMMENT.ToList();

            listOfCommentsWithCommenter = new List<UserCommentModel>();
            foreach (var comment in listOfComment)
            {
                PASTEBOOK_USER commenter = new PASTEBOOK_USER();
                commenter = comment.PASTEBOOK_USER;

                listOfCommentsWithCommenter.Add(new UserCommentModel() { Comment = comment, Commenter = commenter });
            }

            listOfLikes = new List<PASTEBOOK_LIKE>();
            listOfLikes = post.PASTEBOOK_LIKE.ToList();

            listOfLikesWithLiker = new List<UserLikeModel>();
            foreach (var like in listOfLikes)
            {
                PASTEBOOK_USER liker = new PASTEBOOK_USER();
                liker = like.PASTEBOOK_USER;

                listOfLikesWithLiker.Add(new UserLikeModel() { Like = like, Liker = liker });
            }

            PostWithPoster.Post = post;
            PostWithPoster.Poster = poster;
            PostWithPoster.ProfileOwner = profileOwner;
            PostWithPoster.ListOfCommentsWithCommenters = listOfCommentsWithCommenter;
            PostWithPoster.ListOfLikesWithLiker = listOfLikesWithLiker;

            return PostWithPoster;
        }

    }
}