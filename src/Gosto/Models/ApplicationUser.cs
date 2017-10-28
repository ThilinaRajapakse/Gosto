using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gosto.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string ShoppingCartID { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public IList<Order> Orders { get; set; }

        public IList<Reservation> Reservations { get; set; }
    }
}
