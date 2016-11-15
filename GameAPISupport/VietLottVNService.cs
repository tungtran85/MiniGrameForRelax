using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAPISupport.Data;

namespace GameAPISupport
{
    public class VietLottVNService
    {
        public VLVNDataContext _dataContext;
        public VietLottVNService()
        {
            _dataContext = new VLVNDataContext();
        }

        /// <summary>
        /// Get Lastest Record Import
        /// </summary>
        /// <returns></returns>
        public VietlottVN GetlasttestVietLottVN()
        {
            int maxDrawId = _dataContext.VietlottVNs.Max(o => o.DrawId);
            return _dataContext.VietlottVNs.FirstOrDefault(o => o.DrawId == maxDrawId);
        }

        /// <summary>
        /// Import VietLott
        /// </summary>
        /// <param name="obj"></param>
        public void ImportVietlottVN(VietlottVNDto obj)
        {
            var vObj = _dataContext.VietlottVNs.FirstOrDefault(o => o.DrawId == obj.DrawId && o.DayPrize == obj.DayPrize);
            if (vObj == null)
            {
                var dbEntity = new VietlottVN()
                {
                    DrawId = obj.DrawId,
                    DayPrize = obj.DayPrize,
                    FullBlockNumber = obj.FullBlockNumber,
                    ImportDate = DateTime.Now,
                    NumberOne = obj.NumberOne,
                    NumberTwo = obj.NumberTwo,
                    NumberThree = obj.NumberThree,
                    NumberFour = obj.NumberFour,
                    NumberFive = obj.NumberFive,
                    NumberSix = obj.NumberSix
                };
                _dataContext.VietlottVNs.InsertOnSubmit(dbEntity);
                _dataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Get VietlottVN
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<VietlottVNDto> GetListByAmount(int pageSize, int pageIndex)
        {
            var lst = _dataContext.VietlottVNs
                .OrderByDescending(o => o.DayPrize)
                .Skip(pageSize*pageIndex).Take(pageSize).ToList();
                
            if (lst.Count > 0)
            {
                return lst.Select(obj => new VietlottVNDto()
                {
                    DrawId = obj.DrawId,
                    DayPrize = obj.DayPrize,
                    FullBlockNumber = obj.FullBlockNumber,
                    ImportDate = obj.ImportDate,
                    NumberOne = obj.NumberOne,
                    NumberTwo = obj.NumberTwo,
                    NumberThree = obj.NumberThree,
                    NumberFour = obj.NumberFour,
                    NumberFive = obj.NumberFive,
                    NumberSix = obj.NumberSix
                }).ToList();
            }
            return new List<VietlottVNDto>();
        }

        public int[] GetFrequencyNumbers()
        {
            int[] arr = new int[45];
            for (int i = 0; i < 45; i++)
            {
                var objNumber = _dataContext.CountFrequencyNumber(i + 1).FirstOrDefault();
                if (objNumber?.total != null)
                {
                    arr[i] = objNumber.total.Value;
                }
            }
            return arr;
        }
    }
}
