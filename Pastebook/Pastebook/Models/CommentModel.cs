using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int PosterId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}