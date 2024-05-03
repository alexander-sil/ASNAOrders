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

    private void ServerUsername_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }

    private void ServerPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ServerPassword = ServerPassword.Text.Trim();
    }

    private void LoginConfirmButton_Clicked(object sender, EventArgs e)
    {

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

    private void LoginConfirmButton_Clicked_1(object sender, EventArgs e)
    {

    }

    private void ServerHostname_TextChanged(object sender, TextChangedEventArgs e)
    {
        Configuration.ServerHostname = ServerHostname.Text.Trim();
    }
}