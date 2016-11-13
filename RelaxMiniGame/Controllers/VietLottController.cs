using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelaxMiniGame.Controllers
{
    public class VietLottController : Controller
    {
        // GET: VietLott
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult version1()
        {
            return View();
        }
    }
}