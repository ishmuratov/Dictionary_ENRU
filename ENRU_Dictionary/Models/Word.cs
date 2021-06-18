using System;
using System.Collections.Generic;
using System.Text;

namespace ENRU_Dictionary.Models
{
    [Serializable]
    public class Word
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public DateTime Date { get; set; }
        public string Text { get { return String.Format("{0, -20} - {1, 20}", English, Russian); } }
    }
}
