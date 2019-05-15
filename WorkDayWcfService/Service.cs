using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkDayWcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Service : IService
    {
        DateTime IService.GetCurrWorkDay()
        {
            return DateTimeHelper.GetCurrWorkDay();
        }

        DateTime IService.GetDateTime()
        {
            return DateTimeHelper.GetDateTime();
        }

        DateTime IService.GetWorkDay(DateTime date)
        {
            return DateTimeHelper.GetWorkDay(date);
        }

        DateTime IService.GetLast2Workday(DateTime date)
        {
            return DateTimeHelper.GetLast2Workday(date);
        }

        DateTime IService.GetLast3Workday(DateTime date)
        {
            return DateTimeHelper.GetLast3Workday(date);
        }

        DateTime IService.GetLastWorkday(DateTime date)
        {
            return DateTimeHelper.GetLastWorkday(date);
        }

        DateTime IService.GetNext2Workday(DateTime date)
        {
            return DateTimeHelper.GetNext2Workday(date);
        }

        DateTime IService.GetNext3Workday(DateTime date)
        {
            return DateTimeHelper.GetNext3Workday(date);
        }

        DateTime IService.GetNextWorkday(DateTime date)
        {
            return DateTimeHelper.GetNextWorkday(date);
        }

        DateTime IService.GetPointWorkday(DateTime date, int day)
        {
            return DateTimeHelper.GetDateTime();
        }
    }
}
