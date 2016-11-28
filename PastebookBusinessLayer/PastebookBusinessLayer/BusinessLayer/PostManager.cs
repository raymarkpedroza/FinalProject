using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class PostManager 
    {
        IPostRepository _postRepository;

        public PostManager()
        {
            _postRepository = new PostRepository();
        }

        public bool CreatePost(POST post)
        {
            return _postRepository.Create(post);
        }

        public List<POST> GetNewsfeedPost(List<int> listOfPostersId)
        {
            return _postRepository.GetNewsfeedPost(listOfPostersId);
        }

        public List<POST> GetTimelinePost(int id)
        {
            return _postRepository.GetTimelinePost(id);
        }

        public POST GetPost(int id)
        {
            return _postRepository.GetPost(id);
        }
    }
}
