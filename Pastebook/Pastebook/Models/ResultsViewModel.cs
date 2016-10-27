using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class ResultsViewModel
    {
        public List<PASTEBOOK_USER> searchResults = new List<PASTEBOOK_USER>();
        public string searchQuery { get; set; }
    }
}