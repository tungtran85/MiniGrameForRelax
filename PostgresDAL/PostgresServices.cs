using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresDAL
{
    public class PostgresServices
    {
        public PostgresServices()
        {
            ;
        }
        public void DemoInertVietLott()
        {
            PostgresBOL.Open();
            var obj = new PGVietLottVN()
            {
                DayPrize = DateTime.UtcNow,
                Drawid = 0,
                FullBlockNumber = "28 31 34 37 38 41",
                NumberOne = 11,
                NumberTwo = 12,
                NumberThree = 13,
                NumberFour = 14,
                NumberFive = 15,
                NumberSix = 16
            };
            PostgresBOL.InsertVietLottVN(obj);
            PostgresBOL.Close();
        }

        public void ImportVietLottPage(List<PGVietLottVN> lst)
        {
            List<PGVietLottVN> lstPgVietLottVns = new List<PGVietLottVN>();
            if (lst != null && lst.Count > 0)
            {
                foreach (var dto in lst)
                {
                    try
                    {
                        var datePize = dto.DatePizeString.Trim();
                        DateTime dt = DateTime.ParseExact(datePize, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        List<int> listNumberInt = new List<int>();
                        if (dto.ListNumbers != null && dto.ListNumbers.Count > 0)
                        {
                            foreach (var number in dto.ListNumbers)
                            {
                                int iNumber = 0;
                                int.TryParse(number, out iNumber);
                                if (iNumber > 0)
                                {
                                    listNumberInt.Add(iNumber);
                                }
                            }
                        }
                        listNumberInt = listNumberInt.OrderBy(o => o).ToList();
                        if (dto.ListNumbers != null && listNumberInt.Count == 6)
                        {
                            var obj = new PGVietLottVN()
                            {
                                DayPrize = dt,
                                Drawid = 0,
                                FullBlockNumber = String.Join(" ", dto.ListNumbers),
                                NumberOne = listNumberInt[0],
                                NumberTwo = listNumberInt[1],
                                NumberThree = listNumberInt[2],
                                NumberFour = listNumberInt[3],
                                NumberFive = listNumberInt[4],
                                NumberSix = listNumberInt[5]
                            };
                            lstPgVietLottVns.Add(obj);
                        }
                    }
                    catch (Exception exception)
                    {
                        ;
                    }
                }
            }
            PostgresBOL.Open();
            foreach (var pgVietLottVn in lstPgVietLottVns)
            {
                PostgresBOL.InsertVietLottVN(pgVietLottVn);
            }
            PostgresBOL.Close();
        }
    }
}
