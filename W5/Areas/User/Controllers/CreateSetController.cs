using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilities;
using W5.DataAccess.Data;

namespace W5.Areas.User.Controllers
{
    [Authorize(Roles = SD.Role_User)]
    [Area("User")]
    public class CreateSetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CreateSetController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Set setObj)
        {
       
            if (ModelState.IsValid)
            {
                setObj.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in

                var NewDay = setObj.CreateDate.AddDays(5);
                
                _db.Sets.Add(setObj);
                _db.SaveChanges();
            }

            return View();
        }
    }
}
