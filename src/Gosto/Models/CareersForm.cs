using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class CareersForm
    {
        [Required, Display(Name = "Position:")]
        public string Position { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        [Required, Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required, Display(Name = "Email:"), EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Date of Birth (yyyy-mm-dd):")]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Address:")]
        public string Address { get; set; }

        [Required, Display(Name = "Phone:")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Incorrect phone number")]
        public string Phone { get; set; }

        [Required, Display(Name = "NIC number:")]
        [RegularExpression(@"^(\d{9})+[a-zA-Z]$", ErrorMessage = "Incorrect NIC number")]
        public string Nic { get; set; }

        [Required, Display(Name = "Qualifications:")]
        public string Qualifications { get; set; }

        [Display(Name = "Experience:")]
        public string Experience { get; set; }

        [Display(Name = "Referees (Please enter 2):")]
        public string Referees { get; set; }

        [Display(Name = "Interests:")]
        public string Interests { get; set; }

        [Display(Name = "Ambition:")]
        public string Future { get; set; }

        [Display(Name = "I certify that the above particulars are accurate and true to the best of my knowledge.")]
        [MustBeTrue(ErrorMessage = "You must certify that your information is true.")]
        public bool Accurate { get; set; }
    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}
