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
    public class TeamsTournaments
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Teams")]
        public int TeamID { get; set; }
        public Teams Team { get; set; }

        [Required]
        [ForeignKey("Tournaments")]
        public int TournamentID { get; set; }
        public Tournaments Tournament { get; set; }

    }
}
 