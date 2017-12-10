using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CIS_560_Final_Project.Entities;

namespace CIS_560_Final_Project.Models.ManageViewModels
{
    public class PlayerViewModel
    {
        [StringLength(50)]
        [Required]
        public string FirstName { set; get; }

        [StringLength(50)]
        [Required]
        public string LastName { set; get; }

        [Required]
        public DateTime Joined { set; get; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { set; get; }
        
        [Required]
        public CoachOrPlayer cop { set; get;}

        public string IGN { set; get; }
        public bool IsManager { set; get; }

        public int YearsCoaching { set; get; }

        public YearClassification Year { set; get; }

        public string StatusMessage { get; set; }
    }
}