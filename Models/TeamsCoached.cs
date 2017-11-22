using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class TeamsCoached: Members
    {
        [Required]
        [ForeignKey("Coaches")]
        public int CoachID { set; get; }
        public Coaches Coach { set; get; }


        [Required]
        [ForeignKey("Teams")]
        public int TeamID { set; get; }
        public Teams Team { set; get; } 

        [Required]
        public bool WasManager { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { set; get; }

    }
}