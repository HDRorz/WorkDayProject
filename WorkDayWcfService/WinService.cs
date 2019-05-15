using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkDayWcfService
{
    public class WinService
    {
        ServiceHost service = null;

        public void Start()
        {
            DateTimeHelper.GetDateTime();
            service = new ServiceHost(typeof(Service));
            service.Open();
        }

        public void Stop()
        {
            service.Close();
            service = null;
        }
    }
}
