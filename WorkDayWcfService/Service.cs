using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkDayWcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IService
    {
        /// <summary>
        /// wcf 过于智能，会识别方法名中的Async，然后就与异步方法冲突了
        /// </summary>
        /// <returns></returns>
        //string IService.GetCurrWorkDay()
        //{
        //    return DateTimeHelper.GetCurrWorkDay().ToString();
        //}

        Task<string> IService.GetCurrWorkDayAsync()
        {
            return Task.Run(() => DateTimeHelper.GetCurrWorkDay().ToString());
        }

        /// <summary>
        /// 和不用async没什么差别
        /// </summary>
        /// <returns></returns>
        //async Task<string> IService.GetCurrWorkDayAsync()
        //{
        //    return await Task.Run(() => DateTimeHelper.GetCurrWorkDay().ToString());
        //}

        string IService.GetDateTime()
        {
            return DateTimeHelper.GetDateTime().ToString();
        }

        string IService.GetWorkDay(DateTime date)
        {
            return DateTimeHelper.GetWorkDay(date).ToString();
        }

        string IService.GetLastWorkday(DateTime date)
        {
            return DateTimeHelper.GetLastWorkday(date).ToString();
        }

        string IService.GetNextWorkday(DateTime date)
        {
            return DateTimeHelper.GetNextWorkday(date).ToString();
        }

        string IService.GetPointWorkday(DateTime date, int day)
        {
            return DateTimeHelper.GetPointWorkday(date, day).ToString();
        }

        Task<string> IService.TestForThrottle()
        {
            return Task.Run(() => { Thread.Sleep(50); return "true"; });
        }
    }
}
