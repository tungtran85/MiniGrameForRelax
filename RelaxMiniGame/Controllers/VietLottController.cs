using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresDAL;
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
            //var objBig = new VietLottVNService();
            var objBig = new PostgresServices();
            var objViewModel = new VietlottVNViewModel()
            {
                ListFrequencyNumbersMax = new List<int>(),
                ListFrequencyNumbersMin = new List<int>(),
                TotalRound =  objBig.TotalRows()
            };
            objViewModel.ListNumberCustom = objBig.GetListNumberExpose().OrderBy(o=>o.FrequenceExpose).ToList();
            for (int i = 0; i < 6; i++)
            {
                objViewModel.ListFrequencyNumbersMin.Add(objViewModel.ListNumberCustom[i].NumberVietLott);
            }
            int l = objViewModel.ListNumberCustom.Count;
            for (int j = 1; j <= 6; j++)
            {
                objViewModel.ListFrequencyNumbersMax.Add(objViewModel.ListNumberCustom[l-j].NumberVietLott);
            }
            objViewModel.ListFrequencyNumbersMin = objViewModel.ListFrequencyNumbersMin.OrderBy(o => o).ToList();
            objViewModel.ListFrequencyNumbersMax = objViewModel.ListFrequencyNumbersMax.OrderBy(o => o).ToList();
            
            return View(objViewModel);
        }

        public ActionResult GetFrequencyNumber()
        {
            //VietLottVNService objBig = new VietLottVNService();
            var objBig = new PostgresServices();
            var lst = objBig.GetFrequencyNumbers().ToList();
            return Json(new { success=true,mydata= lst }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTop10()
        {
            var objBig = new PostgresServices();
            var lst = objBig.GetListByAmount();
            var result = lst.Select(o => new MyItem()
            {
                Mykey = o.DayPrize.ToShortDateString(),
                Myvalue = o.FullBlockNumber
            }).ToList();
            return Json(new { success = true, mydata = result }, JsonRequestBehavior.AllowGet);
        }
    }
}