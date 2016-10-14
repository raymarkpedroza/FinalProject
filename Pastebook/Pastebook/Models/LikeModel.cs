using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class LikeModel
    {
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public int LikedBy { get; set; }
    }
}