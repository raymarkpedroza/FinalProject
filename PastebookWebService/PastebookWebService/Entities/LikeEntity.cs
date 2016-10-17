using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookWebService.Entities
{
    public class LikeEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int LikedBy { get; set; }
    }
}
