using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Reservation time (Please use dd-MM-yyyy HH:mm format)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Display(Name = "Requests")]
        public string Comments { get; set; }

        public ApplicationUser Customer { get; set; }
    }
}
