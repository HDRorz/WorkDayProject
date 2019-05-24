using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkDayAspCore
{
    public static class DateTimeHelper
    {
    
        static SortedSet<DateTime> NonWorkdays = new SortedSet<DateTime>();

        /// <summary>
        /// 15:00:01
        /// </summary>
        static TimeSpan LatestTime = TimeSpan.FromSeconds(15 * 60 * 60 + 1);


        static DateTimeHelper()
        {
            Init();
        }

        /// <summary>
        /// 非工作日列表
        /// </summary>
        private static void Init()
        {
            string[] nonworkdaylist = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nonworkdaylist.txt"));

            NonWorkdays =
                new SortedSet<DateTime>(nonworkdaylist.AsEnumerable().Select(dateStr => Convert.ToDateTime(dateStr)));
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取当前工作日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetWorkDay(DateTime date)
        {
            return GetWorkday(date, 0);
        }

        /// <summary>
        /// 获取当前工作日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetCurrWorkDay()
        {
            return GetWorkday(DateTime.Now, 0);
        }

        /// <summary>
        /// 上一工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public static DateTime GetLastWorkday(DateTime date)
        {
            return GetWorkday(date, -1);
        }

        /// <summary>
        /// 下一工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>result为DateTime</returns>
        public static DateTime GetNextWorkday(DateTime date)
        {
            return GetWorkday(date, 1);
        }

        /// <summary>
        /// 指定工作日
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime GetPointWorkday(DateTime datetime, int day)
        {
            return GetWorkday(datetime, day);
        }

        /// <summary>
        /// 获取指定工作日的前N工作日，或后N工作日
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="days">向前或向后推days个工作日，向前为负数，向后为正数</param>
        /// <returns></returns>
        private static DateTime GetWorkday(DateTime date, int days)
        {
            int pos = 1;    // 1代表days为非负数，-1代表days为负数
            if (days < 0)
            {
                pos = -1;
                days *= -1;
            }

            DateTime curWorkday = date.Date;
            // 如果时间已经超过当天的15:00:01，则作为下一工作日算
            if (date.TimeOfDay >= LatestTime)
            {
                curWorkday = curWorkday.AddDays(1);
            }

            // 获取当前工作日
            // 判断当天是不是工作日
            var notWorkday = NonWorkdays.Contains(curWorkday);
            while (notWorkday)
            {
                curWorkday = curWorkday.AddDays(1); // 向后推一天

                notWorkday = NonWorkdays.Contains(curWorkday);
            }

            // 计算工作日
            for (int i = 1; i <= days; i++)
            {
                do
                {
                    curWorkday = curWorkday.AddDays(pos); // 向前或向后推一天

                    notWorkday = NonWorkdays.Contains(curWorkday);
                } while (notWorkday);
            }

            return curWorkday.Date;
        }
    }
}
