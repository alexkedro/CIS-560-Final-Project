using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CIS_560_Final_Project.Models
{
    public class Matches
    {
        public int ID { get; set; }

        public int MatchNumber { get; set; }

        [Required]
        [ForeignKey("Teams")]
        public int Team1ID { get; set; }
        public Teams Team1 { get; set; }

        [Required]
        [ForeignKey("Teams")]
        public int Team2ID { get; set; }
        public Teams Team2 { get; set; }

        // Int representing the winner team 1 or team 2
        [Range(1,2)]
        public int Winner { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Datetime { set; get; }


    }
}
 