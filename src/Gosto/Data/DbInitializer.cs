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
                new MenuSection{Name="Salads"},
                new MenuSection{Name="Wraps"},
                new MenuSection{Name="Sandwiches"},
                new MenuSection{Name="Waffle Treats"},
                new MenuSection{Name="Pastas"},
                new MenuSection{Name="Pizza - Authentic Italian"},
                new MenuSection{Name="Main Courses"},
                new MenuSection{Name="Desserts"},
                new MenuSection{Name="Add-ons"},
                new MenuSection{Name="Fresh Juices"},
                new MenuSection{Name="Mocktails"},
                new MenuSection{Name="Coffee"},
                new MenuSection{Name="Tea"},
                new MenuSection{Name="Milk Shakes"},
                new MenuSection{Name="Cakes and Muffins"}
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

                new MenuItem{Name="Sausage & Cheese", Price=350, MenuSectionID=3},
                new MenuItem{Name="CEILAO GOSTO Special Wrap", Price=550, MenuSectionID=3},
                new MenuItem{Name="Tuna", Price=340, MenuSectionID=3},
                new MenuItem{Name="Garlic Prawn", Price=620, MenuSectionID=3},
                new MenuItem{Name="Mutton", Price=690, MenuSectionID=3},
                new MenuItem{Name="Chicken & Cheese", Price=500, MenuSectionID=3},
                new MenuItem{Name="Pickled Fruit", Price=600, MenuSectionID=3},

                new MenuItem{Name="Tuna", Price=380, MenuSectionID=4},
                new MenuItem{Name="Chicken", Price=350, MenuSectionID=4},
                new MenuItem{Name="Egg", Price=250, MenuSectionID=4},
                new MenuItem{Name="Cheese & Sun Blushed Tomato", Price=300, MenuSectionID=4},
                new MenuItem{Name="Ham & Cheese with Mustard", Price=325, MenuSectionID=4},
                new MenuItem{Name="Chicken & Cheese", Price=390, MenuSectionID=4},
                new MenuItem{Name="Club Sandwich", Price=750, MenuSectionID=4},

                new MenuItem{Name="Spicy Chicken", Price=400, MenuSectionID=5},
                new MenuItem{Name="Spicy Mutton", Price=800, MenuSectionID=5},
                new MenuItem{Name="Bacon & Cheese", Price=450, MenuSectionID=5},
                new MenuItem{Name="Waffle with Whipped Cream & Chocolate Sauce", Price=200, MenuSectionID=5},
                new MenuItem{Name="Waffle with Whipped Cream & Strawberry Topping", Price=250, MenuSectionID=5},
                new MenuItem{Name="Waffle with Treacle Honey", Price=180, MenuSectionID=5},
                new MenuItem{Name="Waffle with Natural Bee Honey", Price=300, MenuSectionID=5},

                new MenuItem{Name="Tagliatelle with Spicy Mutton", Price=825, MenuSectionID=6},
                new MenuItem{Name="Spaghetti Bolognese", Price=550, MenuSectionID=6},
                new MenuItem{Name="Creamy Chicken with Spinach", Price=420, MenuSectionID=6},
                new MenuItem{Name="Creamy Prawn with Spinach", Price=490, MenuSectionID=6},
                new MenuItem{Name="Spaghetti & Sausage in Chicken Sauce", Price=350, MenuSectionID=6},
                new MenuItem{Name="Mutton Lasagna", Price=1540, MenuSectionID=6},
                new MenuItem{Name="Bacon Lasagna", Price=1100, MenuSectionID=6},
                new MenuItem{Name="Chicken Lasagna", Price=950, MenuSectionID=6},

                new MenuItem{Name="Roast Chicken - Regular", Price=650, MenuSectionID=7},
                new MenuItem{Name="Roast Chicken - Large", Price=1300, MenuSectionID=7},
                new MenuItem{Name="Garlic Prawn - Regular", Price=740, MenuSectionID=7},
                new MenuItem{Name="Garlic Prawn - Large", Price=1460, MenuSectionID=7},
                new MenuItem{Name="Mutton - Regular", Price=990, MenuSectionID=7},
                new MenuItem{Name="Mutton - Large", Price=1900, MenuSectionID=7},
                new MenuItem{Name="Cheese & Onion - Regular", Price=400, MenuSectionID=7},
                new MenuItem{Name="Cheese & Onion - Large", Price=775, MenuSectionID=7},
                new MenuItem{Name="Bacon - Regular", Price=850, MenuSectionID=7},
                new MenuItem{Name="Bacon - Large", Price=1700, MenuSectionID=7},
                new MenuItem{Name="Sausage - Regular", Price=600, MenuSectionID=7},
                new MenuItem{Name="Sausage - Large", Price=1100, MenuSectionID=7},

                new MenuItem{Name="Grilled Seer Fish with, Butter Fried Herb Vegetables, Mashed Potato and Lemon Butter Sauce", Price=800, MenuSectionID=8},
                new MenuItem{Name="Grilled Chicken with Butter Fried Herb Vegetables, Mashed Potato and Pepper Sauce", Price=550, MenuSectionID=8},
                new MenuItem{Name="Garlic Toast with Two Jumbo Sausages & French Fries", Price=450, MenuSectionID=8},
                new MenuItem{Name="Teriyaki Rice", Price=650, MenuSectionID=8},

                new MenuItem{Name="Chocolate Mousse", Price=250, MenuSectionID=9},
                new MenuItem{Name="Orange Mousse", Price=250, MenuSectionID=9},
                new MenuItem{Name="Ice Cream(Vanilla/Strawberry/Chocolate) topped with Roasted Cashews & Topping", Price=200, MenuSectionID=9},

                new MenuItem{Name="French Fries", Price=250, MenuSectionID=10},
                new MenuItem{Name="Ham and Cheese Omelette", Price=250, MenuSectionID=10},
                new MenuItem{Name="Garlic Toast", Price=25, MenuSectionID=10},
                new MenuItem{Name="Devilled Cashew", Price=350, MenuSectionID=10},

                new MenuItem{Name="Watermelon", Price=200, MenuSectionID=11},
                new MenuItem{Name="Pineapple", Price=200, MenuSectionID=11},
                new MenuItem{Name="Mango", Price=250, MenuSectionID=11},
                new MenuItem{Name="Papaya", Price=200, MenuSectionID=11},
                new MenuItem{Name="Passion Fruit", Price=200, MenuSectionID=11},
                new MenuItem{Name="Lime", Price=200, MenuSectionID=11},
                new MenuItem{Name="Mixed Fruit", Price=250, MenuSectionID=11},
                new MenuItem{Name="Karapincha (Curry Leaves)", Price=200, MenuSectionID=11},
                new MenuItem{Name="Carrot", Price=200, MenuSectionID=11},
                new MenuItem{Name="Cucumber", Price=200, MenuSectionID=11},
                new MenuItem{Name="Carrot and Orange", Price=250, MenuSectionID=11},

                new MenuItem{Name="Mojito", Price=400, MenuSectionID=12},
                new MenuItem{Name="Pineapple Rock", Price=400, MenuSectionID=12},

                new MenuItem{Name="Cappucino", Price=290, MenuSectionID=13},
                new MenuItem{Name="Latte Macchiato", Price=290, MenuSectionID=13},
                new MenuItem{Name="Americano", Price=290, MenuSectionID=13},
                new MenuItem{Name="Espresso", Price=210, MenuSectionID=13},
                new MenuItem{Name="Lungo", Price=290, MenuSectionID=13},
                new MenuItem{Name="Cocoa Hot Milk", Price=300, MenuSectionID=13},
                new MenuItem{Name="Greek Frappe", Price=250, MenuSectionID=13},

                new MenuItem{Name="Green Tea", Price=200, MenuSectionID=14},
                new MenuItem{Name="Flavoured Tea (Black currant, lemon, mint, raspberry, wild strawberry, winter wine, cherry lift, chocolate)", Price=170, MenuSectionID=14},
                new MenuItem{Name="Lemon Iced Tea", Price=175, MenuSectionID=14},
                new MenuItem{Name="Cardamom and Vanilla Tea", Price=150, MenuSectionID=14},
                new MenuItem{Name="Lemon and Cardamom Plain Tea", Price=100, MenuSectionID=14},

                new MenuItem{Name="Chocolate", Price=250, MenuSectionID=15},
                new MenuItem{Name="Vanilla", Price=250, MenuSectionID=15},
                new MenuItem{Name="Strawberry", Price=250, MenuSectionID=15},

                new MenuItem{Name="Coffee Cake", Price=100, MenuSectionID=16},
                new MenuItem{Name="Chocolate Fudge Cake", Price=250, MenuSectionID=16},
                new MenuItem{Name="Vanilla Muffin with Strawberry topping", Price=120, MenuSectionID=16},


















            };
            foreach (MenuItem i in MenuItems)
            {
                context.MenuItems.Add(i);
            }
            context.SaveChanges();
        }
    }
}
