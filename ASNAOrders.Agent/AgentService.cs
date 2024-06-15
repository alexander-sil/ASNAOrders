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

        private IConnection Connection { get; set; }

        private IModel Channel { get; set; }

        private Logic Logic { get; set; }
        public AgentService()
        {
            Logic = new Logic(this);
            InitializeComponent();
        }

        internal void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
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
                            .WithInterval(new TimeSpan(days: (int)Properties.Settings.Default.RepeatIntervalInDays, 0, 0, 0))
                            .RepeatForever())
                        .Build();

                    scheduler.AddJob(job, true);
                    scheduler.ScheduleJob(trigger);

                    if (Properties.Settings.Default.ListenForOrders)
                    {
                        var factory = new ConnectionFactory
                        {
                            HostName = !string.IsNullOrWhiteSpace(Properties.Settings.Default.MQHostname) ? Properties.Settings.Default.MQHostname : Properties.Resources.RabbitmqLocal,
                            Port = Properties.Settings.Default.MQPort != 0 ? Properties.Settings.Default.MQPort : 5672,
                            UserName = !string.IsNullOrWhiteSpace(Properties.Settings.Default.MQUsername) ? Properties.Settings.Default.MQUsername : Properties.Resources.ConfigMQUsername,
                            Password = !string.IsNullOrWhiteSpace(Properties.Settings.Default.MQPassword) ? Properties.Settings.Default.MQPassword : Properties.Resources.ConfigMQPassword

                        };


                        Connection = factory.CreateConnection();

                        Channel = Connection.CreateModel();

                        var consumer = new EventingBasicConsumer(Channel);

                        consumer.Received += Logic.OnReceiveNotification;

                        Channel.QueueDeclare(queue: Properties.Resources.NotifyQueueProperty,
                                                     durable: true,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);

                        Channel.BasicConsume(queue: Properties.Resources.NotifyQueueProperty,
                                    autoAck: true,
                                    consumer: consumer);


                    }
                }
            }
            catch (RabbitMQ.Client.Exceptions.RabbitMQClientException ex)
            {
                AgentNotifyIcon.BalloonTipTitle = Properties.Resources.ErrorMessageRabbitMQTitleTray;
                AgentNotifyIcon.Text = Properties.Resources.ErrorMessageRabbitMQDescTray;
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());

                AgentEventLog.WriteEntry($"{Properties.Resources.ErrorMessageRabbitMQLog}{Properties.Resources.ExceptionMessageTrayLog}{ex.Message}{Properties.Resources.StackTraceTrayLog}{ex.StackTrace}");
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                AgentNotifyIcon.BalloonTipTitle = Properties.Resources.ErrorMessageRabbitMQTitleTray;
                AgentNotifyIcon.Text = Properties.Resources.ErrorMessageRabbitMQDescTray;
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());

                AgentEventLog.WriteEntry($"{Properties.Resources.ErrorMessageRabbitMQLog}{Properties.Resources.ExceptionMessageTrayLog}{ex.Message}{Properties.Resources.StackTraceTrayLog}{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                AgentNotifyIcon.BalloonTipTitle = Properties.Resources.ErrorMessageInfoConnectionTitleTray;
                AgentNotifyIcon.Text = Properties.Resources.ErrorMessageInfoGenericDescTray;
                AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());

                AgentEventLog.WriteEntry($"{Properties.Resources.ErrorMessageInfoGenericLog}{Properties.Resources.ExceptionMessageTrayLog}{ex.Message}{Properties.Resources.StackTraceTrayLog}{ex.StackTrace}");
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
            if ((Connection != null) || (Channel != null))
            {
                Channel.Dispose();
                Connection.Dispose();
            }

            AgentEventLog.Dispose();
        }
    }
}
