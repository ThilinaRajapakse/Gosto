using Gosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.ViewModels
{
    public class ShoppingCartViewModel
    {
        public Order Order { get; set; }

        public IList<OrderMenuItems> Items { get; set; }

        public int Total { get; set; }
    }
}
