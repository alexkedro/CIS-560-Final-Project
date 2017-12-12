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
        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { set; get; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date Joined")]
        public DateTime Joined { set; get; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { set; get; }
        
        [Required]
        public CoachOrPlayer cop { set; get;}

        [Display(Name = "In-Game Name")]
        public string IGN { set; get; }

        [Display(Name = "Team Manager")]
        public bool IsManager { set; get; }

        [Display(Name = "Years Coached")]
        public int YearsCoaching { set; get; }

        [Display(Name = "School Year")]
        public YearClassification Year { set; get; }

        [Display(Name = "Status Message")]
        public string StatusMessage { get; set; }
    }
}