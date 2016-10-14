using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class FriendModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public char Request { get; set; }
        public char IsBLOCKED { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}