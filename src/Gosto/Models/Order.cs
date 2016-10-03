using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public enum TakeAway
    {
        Yes, No
    }

    public class Order
    {
        public int ID { get; set; }

        public TakeAway IsTakeAway { get; set; }

        public DateTime ReadyAt { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<OrderMenuItems> OrderedItems { get; set; }
    }
}
