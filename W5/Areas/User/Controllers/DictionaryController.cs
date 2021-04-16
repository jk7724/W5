using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
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
    public class DictionaryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DictionaryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in
            var obj = new SetsVM();

            var list = _db.Sets.Where(x => x.ApplicationUserId == userId).ToList();

            obj.SetsList = list.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SetsVM obj)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in
            
            var list = _db.Sets.Where(x => x.ApplicationUserId == userId).ToList();

            obj.SetsList = list.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(obj);
        }

        #region API CALLS
        //[HttpGet]
        //public IActionResult GetAll(int SetID)
        //{
        //    //var setId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in
        //    var data = _db.Vocabulary.Where(x => x.SetId == SetID).ToList();

        //    return Json(new { data = data });
        //}
        #endregion

    }
}
