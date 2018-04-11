//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using MenuBoards.Interfaces.Web;
//using MenuBoards.Web.ModelServices;
//using MenuBoards.Web.ViewModels;

//namespace MenuBoards.Web.Controllers
//{
//    public class AccountController : Controller
//    {
//        #region Instances Variables

//        private readonly IAccountService accountService = new AccountService();

//        #endregion

//        // GET: Account
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: Account/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        //
//        // POST: /Account/Register
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public ActionResult Register(RegisterViewModel model)
//        {
//            try
//            {
//                if (this.Request.IsAuthenticated && this.User.UserLoginRole < 2)
//                {
//                    return this.RedirectToAction("Index", "Home");
//                }

//                var model = new RegisterRequest
//                {
//                    MembershipTypeOptions = UIHelper.GetMembershipTypeOptions(),
//                    GenderOptions = UIHelper.GetGenderOptions()
//                };

//                return View(model);
//            }
//            catch (Exception ex)
//            {
//                this.logProvider.Error(this.Request.RawUrl, ex);
//                throw;
//            }
//        }

//        // POST: Account/Create
//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        #region Helpers

        

//        #endregion
//    }
//}
