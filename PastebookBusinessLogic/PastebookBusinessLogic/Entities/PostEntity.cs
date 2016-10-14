using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int ProfileOwnerId { get; set; }
        public int PosterId { get; set; }
    }
}
