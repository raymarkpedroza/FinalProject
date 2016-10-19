using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookWebService.Entities
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public string NotificationType { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public char Seen { get; set; }
    }
}
