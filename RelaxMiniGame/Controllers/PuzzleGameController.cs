using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelaxMiniGame.Controllers
{
    public class PuzzleGameController : Controller
    {
        // GET: PuzzleGame
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Version1()
        {
            return View();
        }
        public ActionResult Version2()
        {
            //SplitImageClass.SplitImageFile(@"D:\Publish\Practice_\ProjectTools\DemoSplitImage\Images_\DemoSplit_.jpg", 4, 4);
            return View();
        }
    }
}