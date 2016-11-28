using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;
using PastebookDataAccess;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class CommentManager
    {
        ICommentRepository _commentRepository;

        public CommentManager()
        {
            _commentRepository = new CommentRepository();
        }

        public bool AddComment(COMMENT comment)
        {
            return _commentRepository.Create(comment);
        }

        public COMMENT GetCommentWithUser(Func<COMMENT, bool> predicate)
        {
            return _commentRepository.GetCommentWithUser(predicate);
        }

        public COMMENT GetComment(int id)
        {
            return _commentRepository.Get(id);
        }
    }
}
