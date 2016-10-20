using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{

    public class Order
    {
        public int ID { get; set; }

        public TakeAway TakeAway { get; set; }

        public DateTime ReadyAt { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<OrderMenuItems> OrderedItems { get; set; }

        public Customer Customer { get; set; }
    }

    public enum TakeAway
    {
        No, Yes
    }

}
