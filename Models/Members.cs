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
    public class Members
    {
        public int ID { set; get; }

        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        [StringLength(100)]
        public string Username { set; get; }

        [Required]
        [StringLength(50)]
        public string FirstName { set; get; }

        [Required]
        [StringLength(50)]
        public string LastName { set; get; }

        [Required]
        public DateTime Joined { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { set; get; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }


         public string FullName
        {
            get 
            {
                return FirstName + ", " + LastName; 
            }
        }
    }

}