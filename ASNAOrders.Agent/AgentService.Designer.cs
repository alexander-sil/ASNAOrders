namespace ASNAOrders.Agent
{
    partial class AgentService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentService));
            this.AgentEventLog = new System.Diagnostics.EventLog();
            this.AgentNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AgentEventLog)).BeginInit();
            // 
            // AgentEventLog
            // 
            this.AgentEventLog.Log = "Application";
            this.AgentEventLog.Source = "ASNAOrdersAgent";
            // 
            // AgentNotifyIcon
            // 
            this.AgentNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("AgentNotifyIcon.Icon")));
            this.AgentNotifyIcon.Text = "ASNAOrders Agent";
            this.AgentNotifyIcon.Visible = true;
            // 
            // AgentService
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.AgentEventLog)).EndInit();

        }

        #endregion

        public System.Diagnostics.EventLog AgentEventLog;
        public System.Windows.Forms.NotifyIcon AgentNotifyIcon;
    }
}
