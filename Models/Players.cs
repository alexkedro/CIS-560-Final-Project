using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Entities;

namespace CIS_560_Final_Project.Models
{
    public class Players : Members
    {
        [Required]
        public YearClassification Year { set; get; }

        //refrences TeamsPlaying
        public ICollection<TeamsMembers> TeamsMembers { set; get; }

        [Required]
        [Display (Name = "In-Game Name")]
        public string IGN { set; get; }

    }

}