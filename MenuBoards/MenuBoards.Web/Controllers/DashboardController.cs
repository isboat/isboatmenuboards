using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Web.Mvc;

namespace MenuBoards.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [RequiresAuthentication]
        public ActionResult Index()
        {
            return View();
        }
    }
}
