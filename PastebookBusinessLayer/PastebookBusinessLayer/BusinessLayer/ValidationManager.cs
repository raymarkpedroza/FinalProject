using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLayer.BusinessLayer
{
    public class ValidationManager
    {
        public bool CheckIfIsNullOrEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public bool CheckIfWhiteSpace(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public bool CheckIfOutOfStringLimit(string text, int count)
        {
            return text.Length > count;
        }
    }
}
