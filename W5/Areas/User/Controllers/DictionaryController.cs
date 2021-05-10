using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Models;
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

            if (HttpContext.Session.GetInt32(SD.ssSetId) != null)
            {
                obj.SetId = (int)HttpContext.Session.GetInt32(SD.ssSetId);
                HttpContext.Session.Remove(SD.ssSetId);
            }

            var list = _db.Sets.Where(x => x.ApplicationUserId == userId).ToList();

            obj.SetsList = list.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            var vocObj = _db.Vocabulary.FirstOrDefault(x => x.Id == id);

            if (vocObj == null)
            {
                return NotFound();
            }
            return View(vocObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vocabulary obj)
        {
            if (ModelState.IsValid)
            {
                
                _db.Vocabulary.Update(obj);
                _db.SaveChanges();
                //return RedirectToAction("AddWord", new { setId = obj.SetId });
            }
            
            return RedirectToAction(nameof(Index));
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

            HttpContext.Session.SetInt32(SD.ssSetId, obj.SetId); //When we back from Edit page to Index, we need SetId
            

            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int SetID)
        {
            var list = new List<Set>();
            var obj = _db.Sets.FirstOrDefault(x => x.Id == SetID);
            if (obj != null)
                list.Add(obj);
            
            return Json(new { data = list });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var vocObj = _db.Sets.FirstOrDefault(x => x.Id == id);

            if (vocObj == null)
            {
                return Json(new { success = false, message = "Something went wrong!" });
            }

            _db.Sets.Remove(vocObj);

            _db.SaveChanges();

            return Json(new { success = true, message = "Dane zostały usunięte z bazy danych" });
        }
        #endregion

    }
}
