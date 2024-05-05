namespace ASNAOrders.Web.Administration.Client.Mobile;

public partial class ConfigPage : ContentPage
{
    
    private string? MailAddressStr { get; set; }

    private bool UseMail { get; set; }

	public ConfigPage()
	{
        InitializeComponent();

        try
        {
            RabbitMQHostname.Text = Configuration.CurrentConfiguration.MqHostname;
            RabbitMQPort.Text = Configuration.CurrentConfiguration.MqPort.ToString();
            RabbitMQVhost.Text = Configuration.CurrentConfiguration.MqvHost;

            IssuerSigningKeySetToAuto.IsChecked = Configuration.CurrentConfiguration.IssuerSigningKeySetToAuto;
            SigningKeyFileSetToAuto.IsChecked = Configuration.CurrentConfiguration.SigningKeyFileSetToAuto;
            ClientSecretSetToAuto.IsChecked = Configuration.CurrentConfiguration.ClientSecretSetToAuto;
            ClientIdSetToAuto.IsChecked = Configuration.CurrentConfiguration.ClientIdSetToAuto;

            IssuerSigningKey.Text = Configuration.CurrentConfiguration.IssuerSigningKey;
            SigningKeyFile.Text = Configuration.CurrentConfiguration.SigningKeyFile;
            ClientSecret.Text = Configuration.CurrentConfiguration.ClientSecret;
            ClientId.Text = Configuration.CurrentConfiguration.ClientId;

            if (Configuration.CurrentConfiguration.DatabaseType == "mssqlserver")
            {
                RadioButtonGroup.SetSelectedValue(DbType, DbType[2]);
            }
            else
            {
                RadioButtonGroup.SetSelectedValue(DbType, DbType[1]);
            }

            SqliteDbCacheFilename.Text = Configuration.CurrentConfiguration.SqliteDbCacheFilename;
            InitialCatalog.Text = Configuration.CurrentConfiguration.InitialCatalog;

            MssqlServerHost.Text = Configuration.CurrentConfiguration.MssqlServerHost;
            MssqlServerPort.Text = Configuration.CurrentConfiguration.MssqlServerPort.ToString();
            MssqlServerUsername.Text = Configuration.CurrentConfiguration.MssqlServerUsername;
            MssqlServerPassword.Text = Configuration.CurrentConfiguration.MssqlServerPassword;

            if (Configuration.CurrentConfiguration.Sink.Contains("mail"))
            {
                RadioButtonGroup.SetSelectedValue(Sink, Sink[2]);
            }
            else if (Configuration.CurrentConfiguration.Sink.Contains("file"))
            {
                RadioButtonGroup.SetSelectedValue(Sink, Sink[3]);
            }
            else
            {
                RadioButtonGroup.SetSelectedValue(Sink, Sink[1]);
            }

            ErrorLogPrefix.Text = Configuration.CurrentConfiguration.ErrorLogPrefix;

            if (Configuration.CurrentConfiguration.MailSSLOptions.Contains("SSL"))
            {
                RadioButtonGroup.SetSelectedValue(MailSSLOptions, MailSSLOptions[2]);
            }
            else if (Configuration.CurrentConfiguration.MailSSLOptions.Contains("STARTTLS"))
            {
                RadioButtonGroup.SetSelectedValue(MailSSLOptions, MailSSLOptions[3]);
            }
            else if (Configuration.CurrentConfiguration.MailSSLOptions.Contains("auto"))
            {
                RadioButtonGroup.SetSelectedValue(MailSSLOptions, MailSSLOptions[1]);
            }
            else
            {
                RadioButtonGroup.SetSelectedValue(MailSSLOptions, MailSSLOptions[4]);
            }

            MailHost.Text = Configuration.CurrentConfiguration.MailHost;
            MailPort.Text = Configuration.CurrentConfiguration.MailPort.ToString();
            MailAddress.Text = Configuration.CurrentConfiguration.Sink.Contains("mail") ? Configuration.CurrentConfiguration.Sink.Split("*")[1] : string.Empty;
            MailPassword.Text = Configuration.CurrentConfiguration.MailPassword;
            MailTo.Text = Configuration.CurrentConfiguration.MailTo;

            if (Configuration.CurrentConfiguration.ClientSecretTransmissionMethod.Contains("TEMP"))
            {
                RadioButtonGroup.SetSelectedValue(ClientSecretTransmissionMethod, ClientSecretTransmissionMethod[1]);
            }
            else if (Configuration.CurrentConfiguration.Sink.Contains("INSECURE"))
            {
                RadioButtonGroup.SetSelectedValue(ClientSecretTransmissionMethod, ClientSecretTransmissionMethod[2]);
            }
            else
            {
                RadioButtonGroup.SetSelectedValue(ClientSecretTransmissionMethod, ClientSecretTransmissionMethod[3]);
            }

            XMLStockPath.Text = Configuration.CurrentConfiguration.XmlStockPath;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
            Navigation.PopAsync(true);
        }
        
    }

    private void GoToUserManagementModeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UserAdministrationPage());
    }

    private void RabbitMQHostname_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MqHostname = e.NewTextValue;
    }

    private void RabbitMQPort_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ushort.TryParse(e.NewTextValue, out ushort result))
        {
            Configuration.ConfigToBeIssued.MqPort = result;
        }
    }

    private void RabbitMQVhost_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MqVhost = e.NewTextValue;
    }

    private void IssuerSigningKeySetToAuto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            IssuerSigningKey.IsEnabled = false;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = true;
        }
        else
        {
            IssuerSigningKey.IsEnabled = true;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = false;
        }
    }

    private void IssuerSigningKey_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.IssuerSigningKey = e.NewTextValue;
    }

    private void SigningKeyFileSetToAuto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            SigningKeyFile.IsEnabled = false;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = true;
        }
        else
        {
            SigningKeyFile.IsEnabled = true;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = false;
        }
    }

    private void SigningKeyFile_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.SigningKeyFile = e.NewTextValue;
    }

    private void ClientIdSetToAuto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ClientId.IsEnabled = false;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = true;
        }
        else
        {
            ClientId.IsEnabled = true;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = false;
        }
    }

    private void ClientId_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.ClientId = e.NewTextValue;
    }

    private void ClientSecret_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.ClientSecret = e.NewTextValue;
    }

    private void SqliteDbCacheFilename_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.SqliteDbCacheFilename = e.NewTextValue;
    }

    private void InitialCatalog_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.CurrentConfiguration.InitialCatalog = e.NewTextValue;
    }

    private void MssqlServerHost_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MssqlServerHost = e.NewTextValue;
    }

    private void MssqlServerPort_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ushort.TryParse(e.NewTextValue, out ushort result))
        {
            Configuration.ConfigToBeIssued.MssqlServerPort = result;
        }
    }

    private void XMLStockPath_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.XmlStockPath = e.NewTextValue;
    }

    private void ErrorLogPrefix_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.ErrorLogPrefix = e.NewTextValue;
    }

    private void MailAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (UseMail)
        {
            MailAddressStr = e.NewTextValue;
        }
    }

    private void MailHost_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MailHost = e.NewTextValue;
    }

    private void MailPort_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ushort.TryParse(e.NewTextValue, out ushort result))
        {
            Configuration.ConfigToBeIssued.MailPort = result;
        }
    }

    private void MailTo_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MailTo = e.NewTextValue;
    }

    private void MailPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MailTo = e.NewTextValue;
    }

    private void IssueAllConfigurationsButton_Clicked(object sender, EventArgs e)
    {
        string sink = "eventlog+mail*";

        try
        {
            if (UseMail)
            {
                sink += MailAddressStr;
                Configuration.ConfigToBeIssued.Sink = sink;
            }
            Configuration.ClientFE.Client.IssueConfigurationAsync(Configuration.RabbitMQHostname, Configuration.RabbitMQPort.ToString(), Configuration.RabbitMQVhost, Configuration.ConfigToBeIssued);
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
        
    }

    private void RadioSqlite_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            SqliteDbCacheFilename.IsEnabled = true;

            InitialCatalog.IsEnabled = false;
            MssqlServerHost.IsEnabled = false;
            MssqlServerPort.IsEnabled = false;
            MssqlServerUsername.IsEnabled = false;
            MssqlServerPassword.IsEnabled = false;
        }

        Configuration.ConfigToBeIssued.DatabaseType = "sqlite";
    }

    private void RadioMssql_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            SqliteDbCacheFilename.IsEnabled = false;

            InitialCatalog.IsEnabled = true;
            MssqlServerHost.IsEnabled = true;
            MssqlServerPort.IsEnabled = true;
            MssqlServerUsername.IsEnabled = true;
            MssqlServerPassword.IsEnabled = true;
        }

        Configuration.ConfigToBeIssued.DatabaseType = "mssqlserver";
    }

    private void RadioAuto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.MailSslOptions = "auto";
        }
    }

    private void RadioSSL_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.MailSslOptions = "none";
        }
    }

    private void EvtLog_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ErrorLogPrefix.IsEnabled = false;
            MailAddress.IsEnabled = false;
            MailHost.IsEnabled = false;
            MailPort.IsEnabled = false;
            MailTo.IsEnabled = false;
            MailPassword.IsEnabled = false;
            MailSSLOptions.IsEnabled = false;
        }

        UseMail = false;
        Configuration.ConfigToBeIssued.Sink = "eventlog";
    }

    private void EvtLogMail_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ErrorLogPrefix.IsEnabled = false;
            MailAddress.IsEnabled = true;
            MailHost.IsEnabled = true;
            MailPort.IsEnabled = true;
            MailTo.IsEnabled = true;
            MailPassword.IsEnabled = true;
            MailSSLOptions.IsEnabled = true;
        }

        DisplayAlert("Предупреждение", "Пожалуйста, введите данные электронной почты.", "Понятно");

        UseMail = true;
    }

    private void File_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ErrorLogPrefix.IsEnabled = true;
            MailAddress.IsEnabled = false;
            MailHost.IsEnabled = false;
            MailPort.IsEnabled = false;
            MailTo.IsEnabled = false;
            MailPassword.IsEnabled = false;
            MailSSLOptions.IsEnabled = false;
        }

        UseMail = false;
        Configuration.ConfigToBeIssued.Sink = $"file*run.log";
    }

    private void Email_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.ClientSecretTransmissionMethod = "email";
        }
    }

    private void FileInsecure_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.ClientSecretTransmissionMethod = "file-INSECURE";
        }
    }

    private void FileTemp_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.ClientSecretTransmissionMethod = "file-TEMP";
        }
    }

    private void MssqlServerPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MssqlServerPassword = e.NewTextValue;
    }

    private void MssqlServerUsername_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ConfigToBeIssued.MssqlServerUsername = e.NewTextValue;
    }

    private void ClientSecretSetToAuto_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ClientSecret.IsEnabled = false;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = true;
        }
        else
        {
            ClientSecret.IsEnabled = true;
            Configuration.ConfigToBeIssued.IssuerSigningKeySetToAuto = false;
        }
    }

    private void RadioSTARTTLSavail_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.MailSslOptions = "STARTTLSavail";
        }
    }

    private void RadioNone_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            Configuration.ConfigToBeIssued.MailSslOptions = "none";
        }
    }
}