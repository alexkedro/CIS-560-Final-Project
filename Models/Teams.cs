using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class Teams
    {
        public int ID { get; set; }

        [ForeignKey("Schools")]
        public int SchoolID { get; set; }
        public Schools School { get; set; } 


        [ForeignKey("GameID")]
        public int GameID { get; set; }
        public Games Game { get; set; } 

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Division { get; set; }

    }
}