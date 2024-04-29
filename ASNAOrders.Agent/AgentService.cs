using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Agent
{
    public partial class AgentService : ServiceBase
    {
        private Logic Logic { get; set; }
        public AgentService()
        {
            Logic = new Logic(this);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {


            if (Properties.Settings.Default.Disabled == true)
            {
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.DisabledIcon.GetHicon());

                AgentNotifyIcon.BalloonTipClicked += (s, e) =>
                {
                    Process.Start(Properties.Resources.BalloonTipClickedRun);
                };
            }

            if (Properties.Settings.Default.Disabled == false)
            {

                AgentEventLog.WriteEntry();
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());

                AgentNotifyIcon.BalloonTipClicked += (s, e) =>
                {
                    Process.Start(Properties.Resources.BalloonTipClickedRun);
                };
            }

        }

        protected override void OnStop()
        {

        }
    }
}
