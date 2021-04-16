using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult AddWord(int setId)
        {
            //create Vocabulary object, set Foreigh key
            var vocObj = new Vocabulary();
            vocObj.SetId = setId;

            return View(vocObj);
        }
        public IActionResult Edit(int id)
        {
            var vocObj = _db.Vocabulary.FirstOrDefault(x => x.Id == id);

            if(vocObj == null)
            {
                return NotFound();
            }
            return View(vocObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vocabulary obj)
        {
            if(ModelState.IsValid)
            {
                _db.Vocabulary.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("AddWord", new { setId = obj.SetId });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWord(Vocabulary obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vocabulary.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("AddWord", new { setId = obj.SetId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Set setObj)
        {
       
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in

                //chcek if User have set with given Name in db. Different user can share the same name.
                if (_db.Sets.Any(x=> x.Name == setObj.Name && x.ApplicationUserId == userId ))
                {
                    ModelState.AddModelError("Name", "Set o podanej nazwie już istnieje. Podaj inną nazwę");
                    return View();
                }

                setObj.ApplicationUserId = userId;
                
                //generate repetition date after 1, 2, 5, 9, 15 days
                setObj.Repeat1 = setObj.CreateDate.AddDays(1);
                setObj.Repeat2 = setObj.Repeat1.AddDays(2);
                setObj.Repeat3 = setObj.Repeat2.AddDays(5);
                setObj.Repeat4 = setObj.Repeat3.AddDays(9);
                setObj.Repeat5 = setObj.Repeat4.AddDays(15);
                
                _db.Sets.Add(setObj);
                _db.SaveChanges();

                
            }
            
            //AddWord controler create Vocabulary. We need send to controler Sets Id as a foreign key
            var setFromDb = _db.Sets.FirstOrDefault(x => x.Name == setObj.Name);
            if (setFromDb == null)
            {
                return NotFound();
            }

            return RedirectToAction("AddWord", new { setId = setFromDb.Id });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int SetID)
        {
            //var setId = User.FindFirst(ClaimTypes.NameIdentifier).Value; //get userId of user who log in
            var data = _db.Vocabulary.Where(x=>x.SetId == SetID).ToList();

            return Json(new { data = data });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var vocObj = _db.Vocabulary.FirstOrDefault(x => x.Id == id);

            if(vocObj == null)
            {
                return Json(new { success = false, message = "Something went wrong!" });
            }

            _db.Vocabulary.Remove(vocObj);

            _db.SaveChanges();

            return Json(new { success = true, message = "The object was successfully deleted from Db " });
        }

        #endregion
    }
}
