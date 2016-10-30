using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
namespace PastebookDataAccess
{
    public interface IPostRepository: IRepository<PASTEBOOK_POST>
    {
        List<PASTEBOOK_POST> GetNewsfeedPost(List<int> listOfPosterId);
        List<PASTEBOOK_POST> GetTimelinePost(int id);
        PASTEBOOK_POST GetPost(int id);
    }
}
