using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsa_Model
{
    public class Search
    {
        public string search_string {get; set;}
        public DateTime search_datetime { get; set; }
        public int positive_score { get; set; }
        public int negative_score { get; set; }
        public int neutral_score { get; set; }
    }
}
