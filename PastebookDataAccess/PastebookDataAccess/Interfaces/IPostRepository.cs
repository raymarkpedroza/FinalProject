using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
namespace PastebookDataAccess
{
    public interface IPostRepository: IRepository<POST>
    {
        List<POST> GetNewsfeedPost(List<int> listOfPosterId);
        List<POST> GetTimelinePost(int id);
        POST GetPost(int id);
    }
}
