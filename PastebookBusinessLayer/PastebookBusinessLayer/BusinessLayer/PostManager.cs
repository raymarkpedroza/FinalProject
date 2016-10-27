using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess.Repositories;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class PostManager : IPostManager
    {
        private IPostRepository _postRepo;

        public PostManager()
        {
            _postRepo = new PostRepository();
        }

        public bool CreatePost(PASTEBOOK_POST post)
        {
            bool result = false;
            result = _postRepo.CreateRecord(post);

            return result;
        }

        public List<PASTEBOOK_POST> RetrieveNewsfeedPost(int id, List<int> listOfFriendIds)
        {
            List<PASTEBOOK_POST> listOfUsersPost = new List<PASTEBOOK_POST>();

            listOfUsersPost = _postRepo.RetrieveList(x => x.PROFILE_OWNER_ID.Equals(id), poster => poster.PASTEBOOK_USER, profileOwner => profileOwner.PASTEBOOK_USER1, comments => comments.PASTEBOOK_COMMENT.Select(commentator => commentator.PASTEBOOK_USER), likes => likes.PASTEBOOK_LIKE.Select(liker => likes.PASTEBOOK_USER));


            foreach (var comparePost in _postRepo.RetrieveList(x => x.POSTER_ID.Equals(id), poster => poster.PASTEBOOK_USER, profileOwner => profileOwner.PASTEBOOK_USER1, comments => comments.PASTEBOOK_COMMENT.Select(commentator => commentator.PASTEBOOK_USER), likes => likes.PASTEBOOK_LIKE.Select(liker => likes.PASTEBOOK_USER)))
            {
                if (!(listOfUsersPost.Any(x => x.ID.Equals(comparePost.ID))))
                {
                    listOfUsersPost.Add(comparePost);
                }
            }

            foreach (var friendId in listOfFriendIds)
            {
                listOfUsersPost.AddRange(_postRepo.RetrieveList(x => x.POSTER_ID.Equals(friendId), poster => poster.PASTEBOOK_USER, profileOwner => profileOwner.PASTEBOOK_USER1, comments => comments.PASTEBOOK_COMMENT.Select(commentator => commentator.PASTEBOOK_USER), likes => likes.PASTEBOOK_LIKE.Select(liker => likes.PASTEBOOK_USER)));
            }

            return listOfUsersPost.OrderByDescending(x=>x.CREATED_DATE).ToList();
        }

        public PASTEBOOK_POST RetrievePost(int postId)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post = _postRepo.RetrieveSpecificRecord(x=>x.ID.Equals(postId), poster=>poster.PASTEBOOK_USER, profileOwner=>profileOwner.PASTEBOOK_USER1, comments=>comments.PASTEBOOK_COMMENT.Select(commentator=>commentator.PASTEBOOK_USER), likes=> likes.PASTEBOOK_LIKE.Select(x=>x.PASTEBOOK_USER));

            return post;
        }

        public List<PASTEBOOK_POST> RetrieveTimelinePost(int id)
        {
            List<PASTEBOOK_POST> listOfUsersPost = new List<PASTEBOOK_POST>();
            listOfUsersPost = _postRepo.RetrieveList(x => x.PROFILE_OWNER_ID.Equals(id), poster => poster.PASTEBOOK_USER, profileOwner => profileOwner.PASTEBOOK_USER1, comments => comments.PASTEBOOK_COMMENT.Select(commentator => commentator.PASTEBOOK_USER), likes => likes.PASTEBOOK_LIKE.Select(liker => likes.PASTEBOOK_USER));

            foreach (var comparePost in _postRepo.RetrieveList(x => x.POSTER_ID.Equals(id), poster => poster.PASTEBOOK_USER, profileOwner => profileOwner.PASTEBOOK_USER1, comments => comments.PASTEBOOK_COMMENT.Select(commentator => commentator.PASTEBOOK_USER), likes => likes.PASTEBOOK_LIKE.Select(liker => likes.PASTEBOOK_USER)))
            {
                if (!(listOfUsersPost.Any(x => x.ID.Equals(comparePost.ID))))
                {
                    listOfUsersPost.Add(comparePost);
                }
            }

            return listOfUsersPost.OrderByDescending(x => x.CREATED_DATE).ToList();
        }
    }
}
