using ASNAOrders.Web.Administration.Client.OpenApi;
using System.Net;

namespace ASNAOrders.Web.Administration.Client.Mobile;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void ServerUsername_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ServerUsername = ServerUsername.Text.Trim();
    }

    private void ServerPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ServerPassword = ServerPassword.Text.Trim();
    }

    private void LoginConfirmButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            AuthenticationResponse response = new InterfaceClientFE(Configuration.ServerHostname, new HttpClient()).Client.AuthenticateAsync(new AuthenticationRequest()
            {
                Username = Configuration.ServerUsername,
                Password = Configuration.ServerPassword
            }).Result;

            if (response != null && response.Result)
            {
                Configuration.AccessToken = response.AccessToken;
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Configuration.AccessToken);

            InterfaceClientFE fe = new InterfaceClientFE(Configuration.ServerHostname, client);
            Configuration.ClientFE = fe; 

            Configuration.CurrentConfiguration = fe.Client.GetConfigurationAsync(Configuration.RabbitMQHostname, Configuration.RabbitMQPort.ToString(), Configuration.RabbitMQVhost).Result;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
    }

    private void RabbitMQHostname_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.RabbitMQHostname = RabbitMQHostname.Text.Trim();
    }

    private void RabbitMQPort_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ushort.TryParse(RabbitMQPort.Text.Trim(), out ushort port))
        {
            Configuration.RabbitMQPort = port;
        }
    }

    private void RabbitMQVhost_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.RabbitMQVhost = RabbitMQVhost.Text.Trim();
    }

    private void ServerHostname_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ServerHostname = ServerHostname.Text.Trim();
    }
}