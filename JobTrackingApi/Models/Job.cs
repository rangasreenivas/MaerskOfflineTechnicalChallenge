using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrackingApi.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public DateTime CreatedDate { get; set; }       
        public int Duration { get; set; }
        public string Status { get; set; }
        
    }
}
