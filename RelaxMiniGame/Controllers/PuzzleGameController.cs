using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresDAL;

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
            //var svPath = Server.MapPath("~/Images");
            //var imageUrl = "http://photo.depvd.com/13/110/14/ph_HGnupVdPFe_x36W37Fp_no.jpg";
            //SplitImage.SplitImageURL4X4(svPath,"1",imageUrl);
            return View();
        }
        public ActionResult SplitImageUrl(string id, string imgUrl)
        {
            var svPath = Server.MapPath("~/Images");
            var vExten = SplitImage.SplitImageURL4X4(svPath, id, imgUrl);
            return Json(new {success = true, exten = vExten }, JsonRequestBehavior.AllowGet);
        }
    }
}