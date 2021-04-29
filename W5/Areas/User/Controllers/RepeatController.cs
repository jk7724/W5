using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using W5.DataAccess.Data;

namespace W5.Areas.User.Controllers
{
    [Authorize(Roles = SD.Role_User)]
    [Area("User")]
    public class RepeatController : Controller
    {
        readonly private ApplicationDbContext _db;

        public RepeatController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var today = DateTime.Today;

            //Get RepetitionEvent scheduled for today and RepetitionEvent that user dont repeat on time
            var list = _db.RepetitionEvents.Where(x=> x.Date <= today).ToList();

            return View(list);
        }

        public IActionResult StartRepeat(int id)
        {
            var obj = _db.RepetitionEvents.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        #region ASP CALL
        [HttpGet]
        public IActionResult GetAll(int SetId)
        {
            var vocList = _db.Vocabulary.Where(x => x.SetId == SetId).ToList();

            return new JsonResult(vocList);
        }
        [HttpPost]
        public IActionResult DeleteAndBack(int Id)
        {
            //delete RepeatEvent from db

            var obj = _db.RepetitionEvents.FirstOrDefault(x => x.Id == Id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.RepetitionEvents.Remove(obj);
            _db.SaveChanges();

            return Json(Url.Action("Index", "Repeat"));
        }
        #endregion
    }
}
