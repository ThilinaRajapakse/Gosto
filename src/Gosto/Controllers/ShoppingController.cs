using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gosto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Gosto.ViewModels;

namespace Gosto.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Order CreateOrder(Order order)
        {
            order.ShoppingCartID = GetShoppingCartID();
            order.OrderDate = DateTime.Now;
            order.Customer = GetUser();
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public string GetShoppingCartID()
        {
            var user = _userManager.GetUserId(User);
            return (user);
        }

        public ApplicationUser GetUser()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.ShoppingCartID != null)
            {
                if (_context.ShoppingCart.Any(c => c.ID == user.ShoppingCartID))
                {
                    user.ShoppingCart = _context.ShoppingCart.Where(c => c.ID == user.ShoppingCartID)
                    .Include(c => c.Order)
                    .ThenInclude(o => o.OrderedItems)
                    .SingleOrDefault();
                    foreach (var item in user.ShoppingCart.Order.OrderedItems)
                    {
                        item.MenuItem = _context.MenuItems.Where(i => i.ID == item.MenuItemID).SingleOrDefault();
                    }
                }
            }
            else
            {
                //user.ShoppingCartID = user.Id;
            }
            return user;
        }

        public void AddToCart(int ID)
        {
            
            var user = GetUser();
            var order = new Order();
            var currentShoppingCart = new ShoppingCart();

            if(user.ShoppingCart == null)
            {
                CreateOrder(order);
                var shoppingCart = new ShoppingCart
                {
                    ID = order.ShoppingCartID,
                    Customer = user,
                    Order = order,
                    OrderID = order.ID
                };

                //user.ShoppingCart = shoppingCart;
                //user.ShoppingCartID = _userManager.GetUserId(User);
                currentShoppingCart = shoppingCart;
                _context.ShoppingCart.Add(currentShoppingCart);
            }
            else
            {
                order = user.ShoppingCart.Order;
                currentShoppingCart = user.ShoppingCart;
            }

            var orderedMenuItem = _context.OrderMenuItems.SingleOrDefault(
                o => o.ShoppingCartID == GetShoppingCartID() && o.MenuItemID == ID);

            if(orderedMenuItem == null)
            {
                orderedMenuItem = new OrderMenuItems
                {
                    MenuItemID = ID,
                    MenuItem = _context.MenuItems.Where(i => i.ID == ID).SingleOrDefault(),
                    ShoppingCartID = GetShoppingCartID(),
                    DateCreated = DateTime.Now,
                    Quantity = 1,
                    Price = _context.MenuItems.Where(i => i.ID == ID).SingleOrDefault().Price,
                    Order = order
                };
                _context.OrderMenuItems.Add(orderedMenuItem);
            }
            else
            {
                orderedMenuItem.Quantity++;
            }

            if(order.OrderedItems.Any(i => i.MenuItemID == ID))
            {
                order.OrderedItems.Remove(_context.OrderMenuItems.SingleOrDefault(i => i.MenuItemID == ID));
                order.OrderedItems.Add(orderedMenuItem);
            }
            else
            {
                order.OrderedItems.Add(orderedMenuItem);
            }

            _context.Order.Update(order);
            currentShoppingCart.Order = order;
            currentShoppingCart.OrderID = order.ID;
            currentShoppingCart.Customer = user;
            //user.ShoppingCart = currentShoppingCart;
           // _userManager.UpdateAsync(user);
            //_context.ApplicationUser.Update(user);
            //_context.ShoppingCart.Update(currentShoppingCart);
            _context.SaveChanges();
        }

       
    }
}