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
        [Display (Name = "School ID")]
        public int SchoolID { get; set; }
        public Schools School { get; set; } 


        [ForeignKey("GameID")]
        [Display(Name = "Game ID")]
        public int GameID { get; set; }
        public Games Game { get; set; } 

        [Required]
        [StringLength(100)]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

    }
}