using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DateServicesHost
{
    public partial class DateService : ServiceBase
    {
        ServiceHost host = null;

        public DateService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(_start);
            t.IsBackground = true;
            t.Start();
        }

        protected override void OnStop()
        {
        }

        private void _start()
        {
            try
            {
                host = new ServiceHost(typeof(WcfDateServiceLib.DateService));
                host.Open();
            }
            catch (Exception e)
            {
            }
        }
    }
}
