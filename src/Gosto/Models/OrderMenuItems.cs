using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Models
{
    public class OrderMenuItems
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public string Comments { get; set; }

        public int MenuItemID { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
