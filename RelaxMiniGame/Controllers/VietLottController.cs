using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameAPISupport;
using RelaxMiniGame.Models;

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
            VietLottVNService objBig = new VietLottVNService();
            VietlottVNViewModel objViewModel = new VietlottVNViewModel();
            //objViewModel.ListFrequencyNumbers = objBig.GetFrequencyNumbers().ToList();
            return View(objViewModel);
        }

        public ActionResult GetFrequencyNumber()
        {
            VietLottVNService objBig = new VietLottVNService();
            var lst = objBig.GetFrequencyNumbers().ToList();
            return Json(new { success=true,mydata= lst }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTop10()
        {
            VietLottVNService objBig = new VietLottVNService();
            var lst = objBig.GetListByAmount(10, 0);
            var result = lst.Select(o => new MyItem()
            {
                Mykey = o.DayPrize.ToShortDateString(),
                Myvalue = o.FullBlockNumber
            }).ToList();
            return Json(new { success = true, mydata = result }, JsonRequestBehavior.AllowGet);
        }
    }
}