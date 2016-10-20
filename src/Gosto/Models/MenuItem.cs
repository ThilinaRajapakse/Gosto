using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class MenuItem
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int MenuSectionID { get; set; }

        public MenuSection MenuSection { get; set; }
    }
}
