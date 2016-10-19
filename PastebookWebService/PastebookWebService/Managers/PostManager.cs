using PastebookEF;
using PastebookWebService.Entities;
using PastebookWebService.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PastebookWebService.Managers
{
    public class PostManager
    {
        public List<PostEntity> RetrieveNewsfeed(int userId, List<int> listOfFriendId)
        {
            List<PostEntity> listOfPosts = new List<PostEntity>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    //retrieve user post
                    var retrieveUserPost = context.PASTEBOOK_POST.Where(x => x.POSTER_ID == userId);

                    //add to list
                    foreach (var post in retrieveUserPost)
                    {
                        listOfPosts.Add(Mapper.MapDBPostTableToWCFPostEntity(post));
                    }

                    //retrieve posts posted on users page
                    var retrievePostsPostedOnUserPage = context.PASTEBOOK_POST.Where(x => x.PROFILE_OWNER_ID == userId);
                    foreach (var post in retrievePostsPostedOnUserPage)
                    {
                        if(!listOfPosts.Any(x=>x.Id == post.ID))
                            listOfPosts.Add(Mapper.MapDBPostTableToWCFPostEntity(post));
                    }

                    //friendspost
                    if (listOfFriendId.Count > 0)
                    {
                        foreach (int friendId in listOfFriendId)
                        {
                            var retrieveFriendsPost = context.PASTEBOOK_POST.Where(x => x.POSTER_ID == friendId);

                            foreach (var post in retrieveFriendsPost)
                            {
                                listOfPosts.Add(Mapper.MapDBPostTableToWCFPostEntity(post));
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

        public int CreatePost(PostEntity post)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PASTEBOOK_POST.Add(Mapper.MapWCFPostEntityToDBPostTable(post));
                    result = context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }

            return result;
        }
    }
}