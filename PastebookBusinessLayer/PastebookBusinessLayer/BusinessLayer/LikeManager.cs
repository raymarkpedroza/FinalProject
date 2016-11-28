using PastebookDataAccess;
using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class LikeManager
    {
        ILikeRepository _likeRepository;

        public LikeManager()
        {
            _likeRepository = new LikeRepository();
        }

        public bool Like(LIKE like)
        {
            return _likeRepository.Create(like);
        }

        public bool Unlike(LIKE like)
        {
            return _likeRepository.Delete(like);
        }

        public LIKE GetLike(int id)
        {
            return _likeRepository.Get(id);
        }

        public List<LIKE> GetLikeWithUser(Func<LIKE, bool> predicate)
        {
            return _likeRepository.GetLikeWithUser(predicate);
        }
    }
}
