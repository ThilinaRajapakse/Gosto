using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gosto.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gosto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            ViewData["Message"] = "The Menu.";

            return View();
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

        public IActionResult BusinessPartners()
        {
            ViewData["Message"] = "Business Partners";

            return View();
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
