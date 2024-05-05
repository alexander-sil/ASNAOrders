using System.Linq.Expressions;

namespace ASNAOrders.Web.Administration.Client.Mobile;

public partial class UserAdministrationPage : ContentPage
{
	public UserAdministrationPage()
	{
		InitializeComponent();
	}

    private void CreateUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.CreateUserAsync(DisplayPromptAsync("����� ������������", "������� ��� ������ ������������:", "OK", string.Empty).Result, DisplayPromptAsync("����� ������������", "������� ������ ������ ������������:", "OK", string.Empty).Result, "RWO");
            DisplayAlert("����������", "�������� ���������", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }
        
    }

    private void BanUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.BanUserAsync(DisplayPromptAsync("�������� ������������", "���� ��������?", "OK", string.Empty).Result, DisplayPromptAsync("�������� ������������", "������?", "OK", string.Empty).Result);
            DisplayAlert("����������", "�������� ���������", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }

    }

    private void UnbanUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.UnbanUserAsync(DisplayPromptAsync("��������� ������������", "���� ���������?", "OK", string.Empty).Result);
            DisplayAlert("����������", "�������� ���������", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }

    }

    private void GetUsersButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<string> userStrings = new List<string>();
            string message = string.Empty;

            OpenApi.UserEntityDataModel[] usersRaw = Configuration.ClientFE.Client.GetUsersAsync().Result.ToArray();

            foreach (var user in usersRaw)
            {
                string banIssued = user.BanIssued ? "��" : "���";
                string banReason = string.IsNullOrWhiteSpace(user.BanReason) ? "�� �������?" : user.BanReason;
                string permissions = user.Permissions.Operator ? "RWO" : user.Permissions.OptionsViewEdit ? "RW" : "R";

                userStrings.Add($"��: {user.Id} ���: {user.Username} �������: {banIssued} ������ {banReason} ����������: {permissions}");
            }

            foreach (var str in userStrings)
            {
                message += $"{str}{Environment.NewLine}";
            }

            DisplayAlert("������ �������������", message, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }
    }

    private void DeleteUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.DeleteUserAsync(DisplayPromptAsync("������� ������������", "���� �������?", "OK", string.Empty).Result);
            DisplayAlert("����������", "�������� ���������", "OK");
        } 
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }
    }

    private void ChangePasswdButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.ChangePasswordAsync(DisplayPromptAsync("������� ������", "���� ������� ������?", "OK", string.Empty).Result, DisplayPromptAsync("������� ������", "����� �����?", "OK").Result);
            DisplayAlert("����������", "�������� ���������", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }
    }

    private void ChangePermsButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            string username = DisplayPromptAsync("������� �����", "���� ������� �����?", "OK", string.Empty).Result;
            string perm = DisplayPromptAsync("������� �����", $"RWO - ��������{Environment.NewLine}RW - ������ ������������ � ������ �������{Environment.NewLine}R - ������ �������", "OK", string.Empty).Result;

            if ((perm == "RWO") || (perm == "RW") || (perm == "R"))
            {
                Configuration.ClientFE.Client.ChangePermissionsAsync(username, perm);
                DisplayAlert("����������", "�������� ���������", "OK");
            }
            else
            {
                DisplayAlert("������", "����������� ����� ������ ������.", "OK");
                return;
            }
        } catch (Exception ex)
        {
            DisplayAlert("������", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "�������");
        }
    }
}