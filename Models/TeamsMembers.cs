using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class TeamsMembers
    {
        public int ID { get; set; }

        [ForeignKey("Teams")]
        public int TeamsID { get; set; }
        public Teams Team { get; set; } 

        [ForeignKey("Members")]
        public int MemberID { get; set; }

        public Members Member { get; set; } 

    }
}
 