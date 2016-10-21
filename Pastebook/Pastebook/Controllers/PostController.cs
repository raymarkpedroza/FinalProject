using Pastebook.Managers;
using Pastebook.Models;
using PastebookDataAccess.Managers;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PostController : Controller
    {
        _PostManager postManager = new _PostManager();
        DataAccessPostManager daPostManager = new DataAccessPostManager();
        DataAccessFriendManager daFriendManager = new DataAccessFriendManager();

        public JsonResult CreatePost(string content, int profileOwner)
        {
            PASTEBOOK_POST post = new PASTEBOOK_POST();
            post.CONTENT = content;
            post.POSTER_ID = (int)Session["UserId"];
            post.PROFILE_OWNER_ID = profileOwner;
            post.CREATED_DATE = DateTime.Now;

            int result = 0;
            result = daPostManager.CreatePost(post);

            return Json(new { result = result });
        }

        public PartialViewResult GetTimelinePosts(int id)
        {
            NewsfeedViewModel newsfeedViewModel = new NewsfeedViewModel();
            newsfeedViewModel.listOfPostsWithPoster = postManager.RetrieveTimelinePosts(id);

            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", newsfeedViewModel);
        }

        public PartialViewResult GetNewsfeedPosts(int id)
        {
            NewsfeedViewModel newsfeedViewModel = new NewsfeedViewModel();

            newsfeedViewModel.listOfPostsWithPoster = postManager.RetrieveNewsfeedPosts(id);

            return PartialView("~/Views/Pastebook/_PostPartialView.cshtml", newsfeedViewModel);
        }
    }
}