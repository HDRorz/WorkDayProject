using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkDayAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateServiceController : ControllerBase
    {

        [HttpGet(template: "CurrWorkDay")]
        public string GetCurrWorkDay()
        {
            return DateTimeHelper.GetCurrWorkDay().ToString("yyyy-MM-dd");
        }

        [HttpGet(template: "CurrWorkDayAsync")]
        public Task<string> GetCurrWorkDayAsync()
        {
            return Task.Run(() => DateTimeHelper.GetCurrWorkDay().ToString("yyyy-MM-dd"));
        }

        [HttpGet(template: "DateTime")]
        public string GetDateTime()
        {
            return DateTimeHelper.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        [HttpGet(template: "WorkDay")]
        public string GetWorkDay([FromForm]DateTime date)
        {
            return DateTimeHelper.GetWorkDay(date).ToString("yyyy-MM-dd");
        }

        [HttpGet(template: "LastWorkday")]
        public string GetLastWorkday([FromForm]DateTime date)
        {
            return DateTimeHelper.GetLastWorkday(date).ToString("yyyy-MM-dd");
        }

        [HttpGet(template: "NextWorkday")]
        public string GetNextWorkday([FromForm]DateTime date)
        {
            return DateTimeHelper.GetNextWorkday(date).ToString("yyyy-MM-dd");
        }

        [HttpGet(template: "PointWorkday")]
        public string GetPointWorkday([FromForm]DateTime date, [FromForm]int day)
        {
            return DateTimeHelper.GetPointWorkday(date, day).ToString("yyyy-MM-dd");
        }

    }
}