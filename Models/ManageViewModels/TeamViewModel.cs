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
        [Display (Name = "Schools")]
        public List<Schools> Schools { get; set; } 

        [Required]
        [Display(Name = "Schools")]
        public List<Games> Games { get; set; } 

        [Required]
        [StringLength(100)]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        public string StatusMessage { get; set; }
    }
}