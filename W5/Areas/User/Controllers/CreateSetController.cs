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
                
                _db.Sets.Add(setObj);
                _db.SaveChanges();

                //Create RepetitionEvent. Set date after 1, 3, 8, 17, 32 days after CraeDate
                for (int i = 0; i < 5; i++)
                {
                    var obj = new RepetitionEvent();
                    obj.ApplicationUserId = userId;
                    obj.SetId = setObj.Id;
                    obj.SetName = setObj.Name;
                    obj.Repetition = " Repetition: " + (i + 1);
                    if (i == 0) obj.Date = setObj.CreateDate.AddDays(1);
                    else if (i == 1) obj.Date = setObj.CreateDate.AddDays(3);
                    else if (i == 2) obj.Date = setObj.CreateDate.AddDays(8);
                    else if (i == 3) obj.Date = setObj.CreateDate.AddDays(17);
                    else if (i == 4) obj.Date = setObj.CreateDate.AddDays(32);

                    _db.RepetitionEvents.Add(obj);

                }
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

            if (vocObj == null)
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
