using Quartz;
using Quartz.Impl;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

                AgentNotifyIcon.BalloonTipClicked += AgentNotifyIconClicked;
            }

            if (Properties.Settings.Default.Disabled == false)
            {
                if (Properties.Settings.Default.PlaceId == 0)
                {
                    Properties.Settings.Default.PlaceId = new Random().Next(1, int.MaxValue);
                    Properties.Settings.Default.Save();
                }

                AgentEventLog.WriteEntry($"{DateTime.Now} {Assembly.GetExecutingAssembly().GetName().Name} " + Properties.Resources.VersionWord + $" {Assembly.GetExecutingAssembly().GetName().Version} " + Properties.Resources.AtYourService);
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());

                AgentNotifyIcon.BalloonTipClicked += AgentNotifyIconClicked;

                StdSchedulerFactory schFactory = new StdSchedulerFactory();
                var scheduler = schFactory.GetScheduler().Result;

                var job = new JobDetailImpl(Properties.Resources.UploadJobName, Properties.Resources.UploadJobGroup, typeof(Logic));

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(Properties.Resources.UploadJobName, Properties.Resources.UploadJobGroup)
                    .WithSimpleSchedule(x => x
                        .WithInterval(new TimeSpan(days: (int)Properties.Settings.Default.RepeatIntervalInDays, 0, 0 ,0))
                        .RepeatForever())
                    .Build();

                scheduler.AddJob(job, true);
                scheduler.ScheduleJob(trigger);

                if (Properties.Settings.Default.ListenForOrders)
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = !string.IsNullOrEmpty(Properties.Settings.Default.MQHostname) ? Properties.Settings.Default.MQHostname : Properties.Resources.RabbitmqLocal,
                        Port = Properties.Settings.Default.MQPort != 0 ? Properties.Settings.Default.MQPort : 5672,

                    };

                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            var consumer = new EventingBasicConsumer(channel);

                            consumer.Received += Logic.OnReceiveNotification;

                            channel.QueueDeclare(queue: Properties.Resources.NotifyQueueProperty,
                                                 durable: true,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                            channel.BasicConsume(queue: Properties.Resources.NotifyQueueProperty,
                                autoAck: true,
                                consumer: consumer);
                        }
                    }
                }  
            }
        }

        public void AgentNotifyIconClicked(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Disabled == false)
            {
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());
            }

            Process.Start(Properties.Resources.BalloonTipClickedRun);
        }

        protected override void OnStop()
        {
            AgentEventLog.Dispose();
        }
    }
}
