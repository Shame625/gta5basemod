using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entity
{
    public class Session
    {
        [Key]
        [MaxLength(40)]
        public string UserId { get; set; }
        public string SessionData { get; set; }
    }
}
