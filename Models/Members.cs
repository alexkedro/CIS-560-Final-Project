using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace CIS_560_Final_Project.Models
{
    public class Members 
    {
        public int ID { set; get;}

        [ForeignKey("UserID")]
        public string UserID { get; set; }
        public Users User { get; set; }

        public ICollection<TeamsMembers> TeamsMembers { set; get; }


        [Required]
        [StringLength(50)]
        public string FirstName { set; get; }

        [Required]
        [StringLength(50)]
        public string LastName { set; get; }

        [Required]
        public DateTime Joined { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { set; get; }


        public string FullName
        {
            get 
            {
                return FirstName + ", " + LastName; 
            }
        }
    }

}