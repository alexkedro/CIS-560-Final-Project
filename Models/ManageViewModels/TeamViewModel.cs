using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_560_Final_Project.Models.ManageViewModels
{
    public class TeamViewModel
    {
        [Required]
        [Display (Name = "School")]
        public Schools School { get; set; } 

        [Required]
        [Display(Name = "Game")]
        public Games Game { get; set; } 

        [Required]
        [StringLength(100)]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        public string StatusMessage { get; set; }
    }
}