using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CIS_560_Final_Project.Entities;

namespace CIS_560_Final_Project.Models
{
    public class Schools
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set;}

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        public States State { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Population { get; set; }

    }

}