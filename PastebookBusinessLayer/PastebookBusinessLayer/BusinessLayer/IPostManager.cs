using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public interface IPostManager
    {
        List<PASTEBOOK_POST> RetrieveTimelinePost(int id);
        List<PASTEBOOK_POST> RetrieveNewsfeedPost(int id, List<int> listOfFriendIds);
        PASTEBOOK_POST RetrievePost(int postId);
        bool CreatePost(PASTEBOOK_POST post);
    }
}
