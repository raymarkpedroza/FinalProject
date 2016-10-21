using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookDataAccess.Managers
{
    public class DataAccessPostManager
    {
        public List<PASTEBOOK_POST> RetrievePosts(int userId, List<int> listOfFriendId)
        {
            List<PASTEBOOK_POST> listOfPosts = new List<PASTEBOOK_POST>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    //retrieve user post
                    var retrieveUserPost = context.PASTEBOOK_POST.Where(x => x.POSTER_ID == userId);

                    //add to list
                    foreach (var post in retrieveUserPost)
                    {
                        listOfPosts.Add(post);
                    }

                    //retrieve posts posted on users page
                    var retrievePostsPostedOnUserPage = context.PASTEBOOK_POST.Where(x => x.PROFILE_OWNER_ID == userId);
                    foreach (var post in retrievePostsPostedOnUserPage)
                    {
                        if (!listOfPosts.Any(x=>x.ID == post.ID))
                            listOfPosts.Add(post);
                    }

                    //friendspost
                    if (listOfFriendId.Count > 0)
                    {
                        foreach (int friendId in listOfFriendId)
                        {
                            var retrieveFriendsPost = context.PASTEBOOK_POST.Where(x => x.POSTER_ID == friendId);

                            foreach (var post in retrieveFriendsPost)
                            {
                                listOfPosts.Add(post);
                            }
                        }
                    }
                }
            }

            catch
            {
            }

            return listOfPosts;
        }

        public int CreatePost(PASTEBOOK_POST post)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_POST.Add(post);
                    result = context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }

            return result;
        }


        public PASTEBOOK_POST RetrievePost(int id)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    post = context.PASTEBOOK_POST.Where(x=>x.ID==id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return post;
        }
    }
}