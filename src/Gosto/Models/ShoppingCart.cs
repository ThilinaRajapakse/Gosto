using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class ShoppingCart
    {
        public string ID { get; set; }

        public int OrderID { get; set; }

        public Order Order { get; set; }

        public ApplicationUser Customer { get; set; }
    }
}
