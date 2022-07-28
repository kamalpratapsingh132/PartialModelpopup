using Microsoft.AspNetCore.Mvc;
using PartialModelpopup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialModelpopup.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var emp = _context.User.ToList();
            return View(emp);
        }

        public ActionResult CreateEdit(int? id)
        {
            UserDto model = new UserDto();
            model.IsActive = true;
            if (id.HasValue)
            {
                var emp = _context.User.Where(e => e.Id == id);
            }
            return PartialView("_CreateEdit", model);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateEdit(UserDto model)
        {
            //validate user  
            if (!ModelState.IsValid)
            {


                return PartialView("_CreateEdit", model);
            }

            else
            {
                _context.User.Add(model);
                _context.SaveChanges();
                return RedirectToAction("index");

            }
            //save user into database   
            
        }
    }
}
