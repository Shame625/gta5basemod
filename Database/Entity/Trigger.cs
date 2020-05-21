using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entity
{
    public class Trigger
    {
        [Key]
        public int TriggerId { get; set; }
        public string JobName { get; set; }
        public string TriggerName { get; set; }
        public string CronExpression { get; set; }
    }
}
