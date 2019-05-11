using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IR_Interface.Models
{
    public class Results
    {
        public bool Search { get; set; }
        public bool Spelling { get; set; }
        public string Query { get; set; }
        public SortedDictionary<int, List<string>> SpellCheck { get; set; }
        public Dictionary<string, List<Tuple<string, int>>> SoundexSearch { get; set; }
        public Results()
        {
            Search = false;
            Spelling = false;
            Query = "";
        }
    }
}