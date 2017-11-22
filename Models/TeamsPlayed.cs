using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class TeamsPlayed: Members
    {
        [Required]
        [ForeignKey("Players")]
        public int PlayerID { set; get; }
        public Players Players { set; get; }

        [ForeignKey("Teams")]
        public int TeamID { set; get; }
        public Teams Teams { set; get; } 

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { set; get; }

    }

}