using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class Aliases
    {
        public int ID { get; set; }

        [ForeignKey("Members")]
        public int MembersID { get; set; }
        public Members Member { get; set; } 

        [StringLength(40)]
        public string IGN { get; set; }



    }
}