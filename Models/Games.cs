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
    public class Games
    {
        public int ID { set; get; }

        [StringLength(50)]
        public string name { set; get; }

        [StringLength(8)]
        public string Abv { set; get; }

        [StringLength(50)]
        public string Company { set; get; }
    }

}