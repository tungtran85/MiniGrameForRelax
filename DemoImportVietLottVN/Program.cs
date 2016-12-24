using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using PostgresDAL;

namespace DemoImportVietLottVN
{
    class Program
    {
        static void Main(string[] args)
        {
            //LookupVietlotVN();
            ImportOnePageVietlottVN(1);
            //Demo();
        }

        public static void Demo()
        {
            var bolService = new PostgresDAL.PostgresServices();
            //bolService.DemoInertVietLott();
            bolService.GetListByAmount();
        }

        public static void ImportOnePageVietlottVN(int pageId)
        {
            try
            {
                var bolService = new PostgresDAL.PostgresServices();
                List<PGVietLottVN> lst = new List<PGVietLottVN>();

                var web = new HtmlWeb();
                string strURL = string.Format("http://www.vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/mega-6-45/winning-numbers/?p={0}", pageId);
                var doc = web.Load(strURL);
                //TODO: Check valid 
                var divContainer = doc.DocumentNode.SelectSingleNode("//table[@class = 'table table-striped']");
                if (divContainer == null)
                {
                    Console.WriteLine("Nothing to import ^_^");
                    return;
                }

                foreach (HtmlNode childNode in divContainer.ChildNodes)
                {
                    if (childNode.Name.Equals("tbody"))
                    {
                        if (childNode.HasChildNodes)
                        {
                            foreach (HtmlNode mychildNode in childNode.ChildNodes)
                            {
                                if (mychildNode.Name.Equals("tr"))
                                {
                                    Console.WriteLine(mychildNode.InnerText);
                                    var obj = new PGVietLottVN();
                                    foreach (var tdChildNodes in mychildNode.ChildNodes)
                                    {
                                        if (tdChildNodes.Name.Equals("td"))
                                        {
                                            if (tdChildNodes.Attributes["style"] != null)
                                            {
                                                obj.StrNumber = tdChildNodes.InnerText;
                                                if (tdChildNodes.HasChildNodes)
                                                {
                                                    foreach (HtmlNode spanChildNode in tdChildNodes.ChildNodes)
                                                    {
                                                        if (spanChildNode.Name.Equals("span"))
                                                        {
                                                            string strNumber = spanChildNode.InnerText;
                                                            obj.ListNumbers.Add(strNumber);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                obj.DatePizeString = tdChildNodes.InnerText;
                                            }
                                        }
                                    }
                                    lst.Add(obj);
                                }
                            }
                        }
                    }
                }
                if (lst.Count > 0)
                {
                    bolService.ImportVietLottPage(lst);
                }
                Console.WriteLine("Finish page ^_^: " + strURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void LookupVietlotVN()
        {
            for (int i = 4; i > 0; i--)
            {
                ImportOnePageVietlottVN(i);
            }
        }

    }
}
