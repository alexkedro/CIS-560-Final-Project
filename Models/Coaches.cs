using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CIS_560_Final_Project.Models
{
    public class Coaches: Members
    {

        [Required]
        public bool IsManager { set; get; }

        [Required]
        public int YearsCoaching { set; get; }
    }

}