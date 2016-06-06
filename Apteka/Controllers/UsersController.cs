using Apteka.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Apteka.Controllers {

    [Authorize(Roles = "Admin")]
    public class UsersController : Controller {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: Users
        public ActionResult Index() {
            ViewBag.Users = UserManager.Users;
            return View();
        }

        // GET: Users/Create
        [HttpGet]
        public ActionResult Create() {
            return View();
        }
        //public ActionResult Delete() {
        //    return View();
        //}

        //POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid && !model.UserName.Contains("admin"))
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, EmailConfirmed = true };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Employee");
                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }
            //    // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: Users/Delete/5
        //public async Task<ActionResult> Delete(string id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = UserManager.FindById(id);
        //    if (!user.UserName.Contains("admin")) {
        //        ViewBag.result = await UserManager.DeleteAsync(await UserManager.FindByIdAsync(id));
        //    }
        //    return RedirectToAction("Index");
        //}

        #region Helpers
        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}
