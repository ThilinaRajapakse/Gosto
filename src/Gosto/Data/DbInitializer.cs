using Gosto.Data;
using System;
using System.Linq;

namespace Gosto.Models
{
    public static class DbInitializer
    {

        public static void Initialize(MenuContext context)
        {
            context.Database.EnsureCreated();

            if (context.MenuItems.Any())
            {
                return;   // DB has been seeded
            }

            var MenuSections = new MenuSection[]
            {
                new MenuSection{Name="Soups"},
                new MenuSection{Name="Salads"}
            };
            foreach (MenuSection s in MenuSections)
            {
                context.MenuSections.Add(s);
            }
            context.SaveChanges();

            var MenuItems = new MenuItem[]
            {
                new MenuItem{Name="Chicken & Mushroom Soup", Price=350, MenuSectionID=1},
                new MenuItem{Name="Mutton Soup", Price=550, MenuSectionID=1},
                new MenuItem{Name="Mix Salad", Price=350, MenuSectionID=2},
                new MenuItem{Name="Ceilao Gosto Chef's Salad", Price=550, MenuSectionID=2},
                new MenuItem{Name="Cashew & Pickled Mango Salad", Price=550, MenuSectionID=2},
                new MenuItem{Name="Shrimp & Fish Salad", Price=650, MenuSectionID=2},
                new MenuItem{Name="Apple, Cashew & Chicken Salad", Price=600, MenuSectionID=2},
            };
            foreach (MenuItem i in MenuItems)
            {
                context.MenuItems.Add(i);
            }
            context.SaveChanges();
        }
    }
}
