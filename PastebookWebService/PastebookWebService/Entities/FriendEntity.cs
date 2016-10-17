using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookWebService.Entities
{
    public class FriendEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public char Request { get; set; }
        public char IsBlocked { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
