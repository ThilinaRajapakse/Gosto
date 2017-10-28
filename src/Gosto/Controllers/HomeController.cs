using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gosto.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gosto.Data;
using Microsoft.EntityFrameworkCore;
using Gosto.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Gosto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task<ActionResult> Menu()
        {
            var model = await GetMenuSectionItemsVM();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetMenuSectionItems(int ID)
        {
            var model = await GetMenuSectionItemsVM(ID);
            return PartialView("MenuSectionContent", model);
        }

        private async Task<MenuSectionItemsVM> GetMenuSectionItemsVM(int lookupID = 1)
        {
            var menuSectionItemsVM = new MenuSectionItemsVM();
            menuSectionItemsVM.MenuSections = await _context.MenuSections
                .Include(i => i.MenuItems)
                .AsNoTracking()
                .ToListAsync();

            MenuSection menuSection = menuSectionItemsVM.MenuSections.Where(i => i.ID == lookupID).Single();
            menuSectionItemsVM.MenuItems = menuSection.MenuItems;

            return menuSectionItemsVM;
        }

        private async Task<MenuSectionItemsVM> GetMenuSectionItemsVM()
        {
            var menuSectionItemsVM = new MenuSectionItemsVM();
            menuSectionItemsVM.MenuSections = await _context.MenuSections
                .Include(i => i.MenuItems)
                .AsNoTracking()
                .ToListAsync();

            MenuSection menuSection = menuSectionItemsVM.MenuSections.Where(i => i.ID == 0).SingleOrDefault();
           // menuSectionItemsVM.MenuItems = menuSection.MenuItems;

            return menuSectionItemsVM;
        }

        public IActionResult ShoppingCart()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var shoppingCart = GetShoppingCartViewModel(user);

            return View(shoppingCart);
        }

        public Order CreateOrder(Order order)
        {
            order.ShoppingCartID = _userManager.GetUserId(User);
            order.OrderDate = DateTime.Now;
            order.Customer = _userManager.GetUserAsync(User).Result;
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        private ShoppingCartViewModel GetShoppingCartViewModel(ApplicationUser user)
        {
            if (user.ShoppingCartID != null)
            {
                user.ShoppingCart = _context.ShoppingCart.Where(c => c.ID == user.ShoppingCartID)
                    .Include(cart => cart.Order)
                    .ThenInclude(order => order.OrderedItems)
                    .SingleOrDefault();
            }
            else
            {
                var order = new Order();
                CreateOrder(order);

                var shoppingCart = new ShoppingCart
                {
                    ID = order.ShoppingCartID,
                    Customer = user,
                    Order = order,
                    OrderID = order.ID
                };

                user.ShoppingCart = shoppingCart;
                user.ShoppingCartID = _userManager.GetUserId(User);
            }

            //var shoppingCart = user.ShoppingCart;
            var shoppingCartViewModel = new ShoppingCartViewModel();
            shoppingCartViewModel.Order = user.ShoppingCart.Order;
            shoppingCartViewModel.Items = user.ShoppingCart.Order.OrderedItems;
            shoppingCartViewModel.Total = 0;

            if (user.ShoppingCart.Order.OrderedItems != null)
            {
                foreach (var item in user.ShoppingCart.Order.OrderedItems)
                {
                    item.MenuItem = _context.MenuItems.Where(i => i.ID == item.MenuItemID).SingleOrDefault();
                    shoppingCartViewModel.Total = shoppingCartViewModel.Total + (item.Price * item.Quantity);
                }
            }

            return shoppingCartViewModel;
        }

        public ApplicationUser GetUser()
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user.ShoppingCartID != null)
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
            else
            {
                user.ShoppingCartID = user.Id;
            }
            return user;
        }

        [HttpGet]
        public ActionResult RemoveFromCart(int ID)
        {
            var user = GetUser();
            var deleteItem = user.ShoppingCart.Order.OrderedItems.Where(i => i.MenuItemID == ID).SingleOrDefault();
            user.ShoppingCart.Order.OrderedItems.Remove(deleteItem);
            foreach (var item in _context.OrderMenuItems)
            {
                if(item.MenuItemID == ID)
                {
                    item.Quantity = 0;
                    break;
                }
            }
            _context.SaveChanges();

            var shoppingCartViewModel = new ShoppingCartViewModel();
            shoppingCartViewModel.Order = user.ShoppingCart.Order;
            shoppingCartViewModel.Items = user.ShoppingCart.Order.OrderedItems;
            shoppingCartViewModel.Total = 0;

            foreach (var item in user.ShoppingCart.Order.OrderedItems)
            {
                item.MenuItem = _context.MenuItems.Where(i => i.ID == item.MenuItemID).SingleOrDefault();
                shoppingCartViewModel.Total = shoppingCartViewModel.Total + (item.Price * item.Quantity);
            }

            return PartialView("ShoppingCartContent", shoppingCartViewModel);
        }

        [HttpGet]
        public ActionResult EmptyCart()
        {
            var user = GetUser();

            for(int i = user.ShoppingCart.Order.OrderedItems.Count - 1; i >= 0; i--)
            {
                var item = user.ShoppingCart.Order.OrderedItems[i];
                user.ShoppingCart.Order.OrderedItems.Remove(item);
            }

            foreach (var item in _context.OrderMenuItems)
            {
                    item.Quantity = 0;
            }

            _context.SaveChanges();

            var shoppingCartViewModel = new ShoppingCartViewModel();
            shoppingCartViewModel.Order = user.ShoppingCart.Order;
            shoppingCartViewModel.Items = user.ShoppingCart.Order.OrderedItems;
            shoppingCartViewModel.Total = 0;

            return PartialView("ShoppingCartContent", shoppingCartViewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Promotions()
        {
            ViewData["Message"] = "Promotions Page";
            var model = new PromotionsViewModel()
            {
                Images = Directory.EnumerateFiles(Path.Combine(_environment.WebRootPath, "uploads"))
                                    .Select(img => "~/uploads/" + Path.GetFileName(img)),
            };
            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact Page";

            return View();
        }

        public IActionResult SocialMedia()
        {
            ViewData["Message"] = "Social Media";

            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["Message"] = "About Us";

            return View();
        }

        public IActionResult FairTrade()
        {
            ViewData["Message"] = "Fair Trade";

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Reservations()
        {
            ViewData["Message"] = "Reservations";

            return View();

        }

        [HttpPost]
        public IActionResult Reservations(Reservation reservation)
        {
            ViewData["Message"] = "Reservations";
            var user = _userManager.GetUserId(User);
            reservation.Customer = _userManager.FindByIdAsync(user).Result;

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                _context.SaveChangesAsync();
            }

            return View("~/Views/Home/ReservationSuccesful.cshtml");
        }

        [HttpGet]
        public IActionResult Careers()
        {
            ViewData["Message"] = "Careers";

            var positions = GetAllPositions();

            var model = new CareersForm();

            model.Positions = GetSelectListItems(positions);

            return View(model);
        }

        private IEnumerable<string> GetAllPositions()
        {
            return new List<string>
            {
                "Kitchen Helper",
                "Marketing Officer",
                "Steward",
                "Cashier"
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        public IActionResult CareersSent()
        {
            ViewData["Message"] = "Careers sent";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Careers(CareersForm mail)
        {
            ViewData["Message"] = "Careers";

            var positions = GetAllPositions();

            mail.Positions = GetSelectListItems(positions);

            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Careers, Ceilao Gosto", "gostocareers@gmail.com"));
                message.To.Add(new MailboxAddress("Ceilao Gosto", "ceilaogosto@gmail.com"));
                message.Subject = "Ceilao Gosto Career Application for position " + mail.Position;

                message.Body = new TextPart("plain")
                {
                    Text = "Applying for the position: " + mail.Position + Environment.NewLine
                    + "Applicant name: " + mail.FirstName + " " + mail.LastName + Environment.NewLine
                    + "Email: " + mail.Email + Environment.NewLine
                    + "Date of Birth: " + mail.DateOfBirth + Environment.NewLine
                    + "Address: " + mail.Address + Environment.NewLine
                    + "Phone: " + mail.Phone + Environment.NewLine
                    + "NIC number: " + mail.Nic + Environment.NewLine
                    + "Qualifications: " + mail.Qualifications + Environment.NewLine
                    + "Experience: " + mail.Qualifications + Environment.NewLine
                    + "Interests: " + mail.Interests + Environment.NewLine
                    + "Ambition: " + mail.Future + Environment.NewLine
                    + "Referees: " + mail.Referees + Environment.NewLine
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("gostocareers", "jobs@gosto");

                    client.Send(message);
                    client.Disconnect(true);
                }

                return View("~/Views/Home/CareersSent.cshtml");
            }

            return View();
        }

        public IActionResult Checkout()
        {
            var user = GetUser();
            string orderedItems = null;
            if (user.ShoppingCart.Order.OrderedItems.Count > 0)
            {
                foreach (var item in user.ShoppingCart.Order.OrderedItems)
                {
                    orderedItems = orderedItems + item.MenuItem.Name + " x " + item.Quantity + Environment.NewLine;
                }

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Orders, Ceilao Gosto", "gostocareers@gmail.com"));
                    message.To.Add(new MailboxAddress("Thilina Rajapakse", "chaturangarajapakshe@gmail.com"));
                    message.Subject = "Ceilao Gosto Order demo ";

                    message.Body = new TextPart("plain")
                    {
                        Text = "New order by " + user.FirstName + " " + user.LastName + Environment.NewLine
                        + "On:  " + DateTime.Now + Environment.NewLine
                        + "Customer email: " + user.Email + Environment.NewLine
                        + "Customer phone: " + user.PhoneNumber + Environment.NewLine
                        + "Customer address: " + user.Address + Environment.NewLine + Environment.NewLine
                        + "Ordered items: " + Environment.NewLine
                        + orderedItems + Environment.NewLine
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("gostocareers", "jobs@gosto");

                        client.Send(message);
                        client.Disconnect(true);
                    }
            }
            EmptyCart();
            return View();
        }     
    


        public IActionResult Error()
        {
            return View();
        }
    }
}
