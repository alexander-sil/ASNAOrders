using System.Data;
using System.Linq.Expressions;
using System.Net.Security;

namespace ASNAOrders.Web.Administration.Client.Desktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LabelNamePerms_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EditCatalogSqlite_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoggingOptions_Enter(object sender, EventArgs e)
        {

        }

        private void LabelMQPortRabbitMQ_Click(object sender, EventArgs e)
        {

        }

        private void LoginOptions_Enter(object sender, EventArgs e)
        {

        }

        private void ProgressBarStatusBarAction_Click(object sender, EventArgs e)
        {

        }

        private void ButtonLoginToIFAction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditHostnameLogin.Text)
                || string.IsNullOrWhiteSpace(EditUsernameLogin.Text)
                || string.IsNullOrWhiteSpace(EditPasswordLogin.Text)
                || string.IsNullOrWhiteSpace(EditMQHostnameRabbitMQ.Text)
                || string.IsNullOrWhiteSpace(EditMQPortRabbitMQ.Text)
                || string.IsNullOrWhiteSpace(EditMQVhostRabbitMQ.Text))

            {
                MessageBox.Show($"Не задано значение одного или нескольких из обязательных параметров", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Configuration.RabbitMQHostname = EditMQHostnameRabbitMQ.Text;
                Configuration.RabbitMQPort = ushort.Parse(EditMQPortRabbitMQ.Text);
                Configuration.RabbitMQVhost = EditMQVhostRabbitMQ.Text;

                OpenApi.AuthenticationRequest request = new OpenApi.AuthenticationRequest()
                {
                    Username = EditUsernameLogin.Text,
                    Password = EditPasswordLogin.Text
                };

                OpenApi.AuthenticationResponse response = new OpenApi.InterfaceClientFE($"{EditHostnameLogin.Text}:{EditPortLogin.Text}", new HttpClient()).Client.AuthenticateAsync(request).Result;
                Configuration.AccessToken = response.AccessToken;

                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Configuration.AccessToken);

                Configuration.ClientFE = new OpenApi.InterfaceClientFE($"{EditHostnameLogin.Text}:{EditPortLogin.Text}", client);
                OpenApi.Config conf = Configuration.ClientFE.Client.GetConfigurationAsync(Configuration.RabbitMQHostname, Configuration.RabbitMQPort.ToString(), Configuration.RabbitMQVhost).Result;

                SecretGeneratorOptions.Enabled = true;

                CheckIssuerSigningKeySetToAutoSecretGenerator.Checked = conf.IssuerSigningKeySetToAuto;
                EditIssuerSigningKeySecretGenerator.Text = conf.IssuerSigningKey;
                CheckSigningKeyFileSetToAutoSecretGenerator.Checked = conf.SigningKeyFileSetToAuto;
                EditSigningKeyFileSecretGenerator.Text = conf.SigningKeyFile;


                CheckClientSecretSetToAutoSecretGenerator.Checked = conf.ClientSecretSetToAuto;
                EditClientSecretSecretGenerator.Text = conf.ClientSecret;
                CheckClientSecretFilenameSetToAutoSecretGenerator.Checked = conf.ClientSecretFilenameSetToAuto;
                EditClientSecretFilenameSecretGenerator.Text = conf.ClientSecretFilename;

                CheckClientIdSetToAutoSecretGenerator.Checked = conf.ClientIdSetToAuto;
                EditClientIdSecretGenerator.Text = conf.ClientId;

                LoggingOptions.Enabled = true;
                ContentOptions.Enabled = true;

                EditXMLStockPathContent.Text = conf.XmlStockPath;

                CreateUser.Enabled = true;
                DeleteUser.Enabled = true;

                BanUser.Enabled = true;
                UnbanUser.Enabled = true;

                ChangePassword.Enabled = true;
                ChangePermissions.Enabled = true;

                GetAllUsers.Enabled = true;

                if (conf.ClientSecretTransmissionMethod.Contains("INSECURE"))
                {
                    ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex = 1;
                }
                else if (conf.ClientSecretTransmissionMethod.Contains("TEMP"))
                {
                    ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex = 0;
                }
                else
                {
                    ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex = 2;
                }

                if (conf.DatabaseType.Contains("sqlite"))
                {
                    MssqlSettings.Enabled = false;
                    SqliteSettings.Enabled = true;

                    EditCatalogSqlite.Text = conf.SqliteDbCacheFilename;

                    ComboBoxDbTypeDatabase.SelectedIndex = 0;
                }
                else if (conf.DatabaseType.Contains("mssql"))
                {
                    SqliteSettings.Enabled = false;
                    MssqlSettings.Enabled = true;

                    EditCatalogMssql.Text = conf.InitialCatalog;
                    EditHostMssql.Text = conf.MssqlServerHost;
                    EditPortMssql.Text = conf.MssqlServerPort.ToString();
                    EditUsernameMssql.Text = conf.MssqlServerUsername;
                    EditPasswordMssql.Text = conf.MssqlServerPassword;

                    ComboBoxDbTypeDatabase.SelectedIndex = 1;
                }

                if (conf.Sink.Contains("mail"))
                {
                    PostOptions.Enabled = true;

                    EditMailHostPost.Text = conf.MailHost;
                    EditMailToPost.Text = conf.MailTo;
                    EditMailUsernamePost.Text = conf.Sink.Split("*")[1];
                    EditMailPasswordPost.Text = conf.MailPassword;
                    EditMailPortPost.Text = conf.MailPort.ToString();
                }

                if (conf.Sink.Contains("file"))
                {
                    if (ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex != 2 || ComboBoxSinkLogging.SelectedIndex != 1)
                    {
                        PostOptions.Enabled = false;
                    }

                    FileLoggingOptions.Enabled = true;

                    EditFilenameFileLogging.Text = conf.Sink.Split("*")[1];
                }
                else if (conf.Sink.Contains("mail"))
                {
                    FileLoggingOptions.Enabled = false;
                    PostOptions.Enabled = true;

                    EditMailHostPost.Text = conf.MailHost;
                    EditMailToPost.Text = conf.MailTo;
                    EditMailUsernamePost.Text = conf.Sink.Split("*")[1];
                    EditMailPasswordPost.Text = conf.MailPassword;
                    EditMailPortPost.Text = conf.MailPort.ToString();
                }

                EditMQHostnameRabbitMQ.Text = conf.MqHostname;
                EditMQPortRabbitMQ.Text = conf.MqPort.ToString();

                EditMQVhostRabbitMQ.Text = conf.MqvHost;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxSinkLogging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxSinkLogging.SelectedIndex == 1)
            {
                FileLoggingOptions.Enabled = false;
                PostOptions.Enabled = true;
            }
            else if (ComboBoxSinkLogging.SelectedIndex == 2)
            {
                FileLoggingOptions.Enabled = true;

                if (ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex != 2)
                {
                    PostOptions.Enabled = false;
                }
            }
            else if (ComboBoxSinkLogging.SelectedIndex == 0)
            {
                FileLoggingOptions.Enabled = false;

                if (ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex != 2)
                {
                    PostOptions.Enabled = false;
                }
            }
        }

        private void ComboBoxDbTypeDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxDbTypeDatabase.SelectedIndex == 0)
            {
                MssqlSettings.Enabled = false;
                SqliteSettings.Enabled = true;
            }
            else if (ComboBoxDbTypeDatabase.SelectedIndex == 1)
            {
                MssqlSettings.Enabled = true;
                SqliteSettings.Enabled = false;
            }
        }

        private void CheckIssuerSigningKeySetToAutoSecretGenerator_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckIssuerSigningKeySetToAutoSecretGenerator.Checked == true)
            {
                EditIssuerSigningKeySecretGenerator.Enabled = false;
            }
            else
            {
                EditIssuerSigningKeySecretGenerator.Enabled = true;
            }
        }

        private void CheckSigningKeyFileSetToAutoSecretGenerator_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSigningKeyFileSetToAutoSecretGenerator.Checked == true)
            {
                EditSigningKeyFileSecretGenerator.Enabled = false;
            }
            else
            {
                EditSigningKeyFileSecretGenerator.Enabled = true;
            }
        }

        private void CheckClientSecretSetToAutoSecretGenerator_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckClientSecretSetToAutoSecretGenerator.Checked == true)
            {
                EditClientSecretSecretGenerator.Enabled = false;
            }
            else
            {
                EditClientSecretSecretGenerator.Enabled = true;
            }
        }

        private void CheckClientSecretFilenameSetToAutoSecretGenerator_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckClientSecretFilenameSetToAutoSecretGenerator.Checked == true)
            {
                EditClientSecretFilenameSecretGenerator.Enabled = false;
            }
            else
            {
                EditClientSecretFilenameSecretGenerator.Enabled = true;
            }
        }

        private void CheckClientIdSetToAutoSecretGenerator_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckClientIdSetToAutoSecretGenerator.Checked == true)
            {
                EditClientIdSecretGenerator.Enabled = false;
            }
            else
            {
                EditClientIdSecretGenerator.Enabled = true;
            }
        }

        private void ComboBoxClientSecretTransmissionMethodSecretGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex == 2)
            {
                PostOptions.Enabled = true;
            }
            else
            {
                if (ComboBoxSinkLogging.SelectedIndex != 1)
                {
                    PostOptions.Enabled = false;
                }
            }
        }

        private void ButtonOKCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditUsernameCreate.Text) || string.IsNullOrWhiteSpace(EditPasswordCreate.Text) || (ComboBoxFlagCreate.SelectedIndex == -1))
            {
                MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Configuration.ClientFE.Client.CreateUserAsync(EditUsernameCreate.Text, EditPasswordCreate.Text,
                    ComboBoxFlagCreate.SelectedIndex == 0 ? "RWO" : ComboBoxFlagCreate.SelectedIndex == 1 ? "RW" : "R");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOKBan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтвердить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                if (string.IsNullOrWhiteSpace(EditNameBan.Text) || string.IsNullOrWhiteSpace(EditReasonBan.Text))
                {
                    MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    Configuration.ClientFE.Client.BanUserAsync(EditNameBan.Text, EditReasonBan.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonOKUnban_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditNameUnban.Text))
            {
                MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Configuration.ClientFE.Client.UnbanUserAsync(EditNameUnban.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOKDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтвердить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes))
            {
                if (string.IsNullOrWhiteSpace(EditNameDelete.Text))
                {
                    MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    Configuration.ClientFE.Client.DeleteUserAsync(EditNameDelete.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonOKPasswd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditNamePasswd.Text) || string.IsNullOrWhiteSpace(EditNewPassPasswd.Text))
            {
                MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Configuration.ClientFE.Client.ChangePasswordAsync(EditNamePasswd.Text, EditNewPassPasswd.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOKPerms_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditNamePerms.Text) || (ComboBoxFlagPerms.SelectedIndex == -1))
            {
                MessageBox.Show("Неправильно задан формат одного или нескольких обязательных полей.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Configuration.ClientFE.Client.ChangePermissionsAsync(EditNamePerms.Text, ComboBoxFlagPerms.SelectedIndex == 0 ? "RWO" : ComboBoxFlagPerms.SelectedIndex == 1 ? "RW" : "R");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonGetAllUsers_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewInformation.Columns.Clear();
                ListViewInformation.Clear();

                List<ListViewItem> users = new List<ListViewItem>();
                OpenApi.UserEntityDataModel[] userData = Configuration.ClientFE.Client.GetUsersAsync().Result.ToArray();

                ListViewInformation.Columns.AddRange(new ColumnHeader[]
                {
                    new ColumnHeader()
                    {
                        Name = "Users",
                        Text = "Пользователи",
                        TextAlign = HorizontalAlignment.Center,
                        Width = 313
                    }
                });

                int i = 0;

                foreach (var user in userData)
                {
                    string perms = user.Permissions.Operator ? "RWO" : user.Permissions.OptionsViewEdit ? "RW" : "R";
                    string banned = user.BanIssued ? "Да" : "Нет";
                    string reason = !string.IsNullOrEmpty(user.BanReason) ? user.BanReason : "Не забанен";

                    users.Add(new ListViewItem()
                    {
                        Name = $"Row{i}",
                        Text = $"ИД: {user.Id} Имя: {user.Username} Флаги: {perms} Забанен: {banned} Причина бана: {reason}"
                    });
                }

                ListViewInformation.Items.AddRange(users.ToArray());
                ListViewInformation.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonApplyConfigurationAction_Click(object sender, EventArgs e)
        {
            ProgressBarStatusBarAction.Style = ProgressBarStyle.Marquee;

            try
            {
                Configuration.ClientFE.Client.IssueConfigurationAsync(Configuration.RabbitMQHostname, Configuration.RabbitMQPort.ToString(), Configuration.RabbitMQVhost, new OpenApi.IssuibleConfig()
                {
                    MqHostname = EditMQHostnameRabbitMQ.Text,
                    MqPort = ushort.Parse(EditMQPortRabbitMQ.Text),
                    MqVhost = EditMQVhostRabbitMQ.Text,

                    IssuerSigningKeySetToAuto = CheckIssuerSigningKeySetToAutoSecretGenerator.Checked,
                    IssuerSigningKey = EditIssuerSigningKeySecretGenerator.Text,
                    SigningKeyFile = EditSigningKeyFileSecretGenerator.Text,
                    SigningKeyFileSetToAuto = CheckSigningKeyFileSetToAutoSecretGenerator.Checked,
                    ClientId = EditClientIdSecretGenerator.Text,
                    ClientIdSetToAuto = CheckClientIdSetToAutoSecretGenerator.Checked,

                    ClientSecretFilenameSetToAuto = CheckClientSecretFilenameSetToAutoSecretGenerator.Checked,
                    ClientSecretFilename = EditClientSecretFilenameSecretGenerator.Text,
                    ClientSecretSetToAuto = CheckClientSecretSetToAutoSecretGenerator.Checked,
                    ClientSecret = EditClientSecretSecretGenerator.Text,


                    XmlStockPath = EditXMLStockPathContent.Text,
                    DatabaseType = ComboBoxDbTypeDatabase.SelectedIndex == 1 ? "mssqlserver" : "sqlite",
                    InitialCatalog = EditCatalogMssql.Text,

                    SqliteDbCacheFilename = EditCatalogSqlite.Text,
                    MssqlServerHost = EditHostMssql.Text,
                    MssqlServerPort = ushort.Parse(EditPortMssql.Text),
                    MssqlServerUsername = EditUsernameMssql.Text,
                    MssqlServerPassword = EditPasswordMssql.Text,

                    Sink = ComboBoxSinkLogging.SelectedIndex == 1 ? $"eventlog+mail*{EditMailUsernamePost}" : ComboBoxSinkLogging.SelectedIndex == 2 ? $"file*{EditFilenameFileLogging}" : "eventlog",
                    ErrorLogPrefix = EditPrefixFileLogging.Text,

                    MailPassword = EditMailPasswordPost.Text,
                    MailTo = EditMailToPost.Text,
                    MailHost = EditMailHostPost.Text,
                    MailPort = ushort.Parse(EditMailPortPost.Text),
                    MailSslOptions = ComboBoxMailSSLOptionsPost.SelectedIndex == 0 ? "SSL" : ComboBoxMailSSLOptionsPost.SelectedIndex == 1 ? "STARTTLSavail" : ComboBoxMailSSLOptionsPost.SelectedIndex == 2 ? "auto" : "none",


                    ClientSecretTransmissionMethod = ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex == 2 ? "email" : ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndex == 1 ? "file-INSECURE" : "file-TEMP",
                });
                ProgressBarStatusBarAction.Style = ProgressBarStyle.Blocks;
            }
            catch (Exception ex)
            {
                ProgressBarStatusBarAction.Style = ProgressBarStyle.Blocks;
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
