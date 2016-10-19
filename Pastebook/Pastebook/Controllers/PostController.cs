using Pastebook.Managers;
using Pastebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PostController : Controller
    {
        PostManager postManager = new PostManager();
        public JsonResult CreatePost(string content)
        {
            PostModel post = new PostModel();
            post.Content = content;
            post.PosterId = (int)Session["UserId"];
            post.ProfileOwnerId = (int)Session["UserId"];
            post.CreatedDate = DateTime.Now;

            int result = 0;
            result = postManager.CreatePost(post);

            return Json( new {result = result });
        }

        public PartialViewResult GetUserPosts()
        {
            NewsfeedViewModel newsfeedViewModel = new NewsfeedViewModel();
            newsfeedViewModel.listOfPostsWithPoster = postManager.RetrieveUserPosts((int)Session["UserId"]);

            return PartialView("~/Views/User/_NewsfeedPartialView.cshtml", newsfeedViewModel);
        }

        //public PartialViewResult GetComments()
        //{

        //}
    }
}