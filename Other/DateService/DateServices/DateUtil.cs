using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DateServices
{
    public class DateUtil
    {
        static DateUtil _dateUtil = null;
        static object _locker = new object();
        static string _nonworkdaylistfilepath = string.Empty;
        private DateUtil()
        {
        }

        public static DateUtil Instance(string nonWorkDayListFilePath)
        {
            _nonworkdaylistfilepath = nonWorkDayListFilePath;
            lock (_locker)
            {
                if (_dateUtil == null)
                {
                    _dateUtil = new DateUtil();
                }
            }
            return _dateUtil;
        }
        //static List<DateTime> _nonworkdays = null;
        static Dictionary<DateTime,DateTime> _nonworkdays = null;
        static object _nonworkdaysLocker = new object();

        
        /// <summary>
        /// 非工作日列表
        /// </summary>
        private Dictionary<DateTime,DateTime> NonWorkdays
        {
            get
            {
                if (null == _nonworkdays)
                {
                    _nonworkdays = new Dictionary<DateTime,DateTime>();
                    string[] nonworkdaylist = File.ReadAllLines(_nonworkdaylistfilepath);
                    lock (_nonworkdaysLocker)
                    {
                        foreach (string day in nonworkdaylist)
                        {
                            _nonworkdays.Add(Convert.ToDateTime(day), Convert.ToDateTime(day));
                        }
                    }
                }
                return _nonworkdays;
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public DateTime FetchDateTime()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// 获取当前工作日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime FetchCurrWorkDay(DateTime date)
        {
            return GetWorkday(date, 0);
        }

        /// <summary>
        /// 获取当前工作日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime FetchCurrWorkDay()
        {
            return GetWorkday(DateTime.Now, 0);
        }
        /// <summary>
        /// 下一工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime FetchNextWorkday(DateTime date)
        {
            return GetWorkday(date, 1);
        }

        /// <summary>
        /// 上一工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetLastWorkday(DateTime date)
        {
            return GetWorkday(date, -1);
        }

        /// <summary>
        /// 上两个工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetLast2Workday(DateTime date)
        {
            return GetWorkday(date, -2);
        }
        /// <summary>
        /// 上三个工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetLast3Workday(DateTime date)
        {
            return GetWorkday(date, -3);
        }
        /// <summary>
        /// 下一工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetNextWorkday(DateTime date)
        {
            return GetWorkday(date, 1);
        }
        /// <summary>
        /// 下二个工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetNext2Workday(DateTime date)
        {
            return GetWorkday(date, 2);
        }
        /// <summary>
        /// 下三个工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public DateTime GetNext3Workday(DateTime date)
        {
            return GetWorkday(date, 3);
        }

        /// <summary>
        /// 获取预计确认日期
        /// </summary>
        /// <param name="appTime">交易申请日期</param>
        /// <returns>返回预计确认日期</returns>
        public DateTime FetchPreCfmDate(DateTime appTime)
        {
            // 预计确认日期为下一工作日
            return FetchNextWorkday(appTime);
        }

        /// <summary>
        /// 指定工作日
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DateTime FetchPointWorkday(DateTime datetime, int day)
        {
            return GetWorkday(datetime, day);
        }
        /// <summary>
        /// 获取指定工作日的前N工作日，或后N工作日
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="days">向前或向后推days个工作日，向前为负数，向后为正数</param>
        /// <returns></returns>
        private DateTime GetWorkday(DateTime date, int days)
        {
            int pos = 1;    // 1代表days为非负数，-1代表days为负数
            if (days < 0)
            {
                pos = -1;
                days *= -1;
            }

            DateTime curWorkday = date;
            //2015-07-17修改 15:01的也算当前工作日
            // 如果时间已经超过当天的15:01，则作为下一工作日算
            //if (date.Hour >= 15)
            if ((date.Hour >= 15 && date.Minute == 0 && date.Second > 0) || (date.Hour >= 15 && date.Minute > 0) || (date.Hour > 15))
            {
                curWorkday = curWorkday.AddDays(1);
            }
            curWorkday = curWorkday.Date;

            // 获取当前工作日
            // 判断当天是不是工作日
            var notWorkday = NonWorkdays.ContainsKey(curWorkday);
            while (notWorkday)
            {
                curWorkday = curWorkday.AddDays(1); // 向后推一天

                notWorkday = NonWorkdays.ContainsKey(curWorkday);
            }

            // 计算工作日
            for (int i = 1; i <= days; i++)
            {
                do
                {
                    curWorkday = curWorkday.AddDays(pos); // 向前或向后推一天

                    notWorkday = NonWorkdays.ContainsKey(curWorkday);
                } while (notWorkday);
                // curWorkday = date.AddDays(pos); // 向前或向后推一天
            }

            return curWorkday.Date;
        }
    }
}
