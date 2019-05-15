using DateServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfDateServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class DateService : IDateService
    {
        DateUtil _dateutil = null;
        public DateService()
        {
            if (_dateutil == null)
            {
                string nonWorkdaysListFile = System.Configuration.ConfigurationManager.AppSettings["nonWorkdayListFile"];
                _dateutil = DateUtil.Instance(nonWorkdaysListFile);
                //File.WriteAllText(@"D:\东财工作\FundTrade\MiddleServices\DateServicesHost\bin\Release\a.log", nonWorkdaysListFile);
            }
        }
        public DateService(string nonWorkdaysListFile)
        {
            _dateutil = DateUtil.Instance(nonWorkdaysListFile);
            //File.WriteAllText(@"D:\东财工作\FundTrade\MiddleServices\DateServicesHost\bin\Release\a.log", nonWorkdaysListFile);
        }

        public DateTime FetchDateTime()
        {
            return _dateutil.FetchDateTime();
        }

        public DateTime FetchCurrWorkDay(DateTime date)
        {
            return _dateutil.FetchCurrWorkDay(date);
        }

        public DateTime FetchLastWorkday(DateTime date)
        {
            return _dateutil.GetLastWorkday(date);
        }

        public DateTime FetchLast2Workday(DateTime date)
        {
            return _dateutil.GetLast2Workday(date);
        }

        public DateTime FetchLast3Workday(DateTime date)
        {
            return _dateutil.GetLast3Workday(date);
        }

        public DateTime FetchNextWorkday(DateTime date)
        {
            return _dateutil.GetNextWorkday(date);
        }

        public DateTime FetchNext2Workday(DateTime date)
        {
            return _dateutil.GetNext2Workday(date);
        }

        public DateTime FetchNext3Workday(DateTime date)
        {
            return _dateutil.GetNext3Workday(date);
        }

        public DateTime FetchPointWorkday(DateTime date, int day)
        {
            return _dateutil.FetchPointWorkday(date, day);
        }

        public DateTime FetchPreCfmDate(DateTime appTime)
        {
            // 预计确认日期为下一工作日
            return FetchNextWorkday(appTime);
        }
    }
}
