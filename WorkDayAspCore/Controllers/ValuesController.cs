using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkDayAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Test2();
            //return "value";
            DateTimeHelper.GetCurrWorkDay();

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            int len = 10000000;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetCurrWorkDay();
            }
            sw.Stop();

            var timeuse = sw.ElapsedMilliseconds;

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            sw.Restart();
            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetCurrWorkDay();
            }
            sw.Stop();

            var timeuse1 = sw.ElapsedMilliseconds;

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            sw.Restart();
            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetCurrWorkDay_Old();
            }
            sw.Stop();
            var timeuse2 = sw.ElapsedMilliseconds;

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetNextWorkday(DateTime.Now);
            }
            sw.Stop();

            var timeuse3 = sw.ElapsedMilliseconds;

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            sw.Restart();
            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetNextWorkday_Old(DateTime.Now);
            }
            sw.Stop();
            var timeuse4 = sw.ElapsedMilliseconds;


            return $"GetCurrWorkDay 优化后{timeuse1}ms，优化前{timeuse2}ms\r\n"
                + $"GetNextWorkday 优化后{timeuse3}ms，优化前{timeuse4}ms\r\n";
        }

        
        public string Test2()
        {
            //return "value";
            DateTimeHelper.GetCurrWorkDay();

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            int len = 10000000;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < len; i++)
            {
                DateTimeHelper.GetCurrWorkDay();
            }
            sw.Stop();

            var timeuse = sw.ElapsedMilliseconds;

            GC.Collect();
            System.Threading.Thread.Sleep(1000);

            string result = "";
            
            for (int day = 0; day < 10; day++)
            {
                sw.Restart();
                for (int i = 0; i < len; i++)
                {
                    DateTimeHelper.GetCurrWorkDay();
                }
                sw.Stop();

                var timeuse1 = sw.ElapsedMilliseconds;

                GC.Collect();
                System.Threading.Thread.Sleep(1000);

                result += $"n={day}，{timeuse1}ms\r\n";
            }


            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
