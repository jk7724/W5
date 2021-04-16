using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace W5.Areas.User.Controllers
{
    [Authorize(Roles = SD.Role_User)]
    [Area("User")]
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
