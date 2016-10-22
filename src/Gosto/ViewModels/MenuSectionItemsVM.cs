using Gosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.ViewModels
{
    public class MenuSectionItemsVM
    {
        public int MenuSectionID { get; set; }

        public IList<MenuSection> MenuSections { get; set; }

        public IList<MenuItem> MenuItems { get; set; }
    }
}
