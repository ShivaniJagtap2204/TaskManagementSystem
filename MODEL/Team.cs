using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? ManagerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
