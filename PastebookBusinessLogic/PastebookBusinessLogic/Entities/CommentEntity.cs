using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int PosterId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
