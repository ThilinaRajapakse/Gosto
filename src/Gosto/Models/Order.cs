using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{

    public class Order
    {
        public int ID { get; set; }

        public string ShoppingCartID { get; set; }

        public TakeAway TakeAway { get; set; }

        public int TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public IList<OrderMenuItems> OrderedItems { get; set; }

        public ApplicationUser Customer { get; set; }
    }

    public enum TakeAway
    {
        No, Yes
    }

}
