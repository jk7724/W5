using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CalendarController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in

            var list = _db.RepetitionEvents.Where(x => x.ApplicationUserId == userId).ToList();

            //return Json(new { data = list });
            return new JsonResult(list);

        }
       
        #endregion

    }
}
