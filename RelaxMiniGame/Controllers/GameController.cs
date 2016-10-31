using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelaxMiniGame.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonkeyV1()
        {
            return View();
        }
        public ActionResult VueDice()
        {
            return View();
        }
    }
}