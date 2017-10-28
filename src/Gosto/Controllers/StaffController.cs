using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Gosto.Controllers
{
    [Authorize(Roles = "Staff")]

    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}