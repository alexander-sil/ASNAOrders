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
            Configuration.ClientFE.Client.CreateUserAsync(DisplayPromptAsync("Новый пользователь", "Введите имя нового пользователя:", "OK", string.Empty).Result, DisplayPromptAsync("Новый пользователь", "Введите пароль нового пользователя:", "OK", string.Empty).Result, "RWO");
            DisplayAlert("Информация", "Операция выполнена", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
        
    }

    private void BanUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.BanUserAsync(DisplayPromptAsync("Забанить пользователя", "Кого забанить?", "OK", string.Empty).Result, DisplayPromptAsync("Забанить пользователя", "Почему?", "OK", string.Empty).Result);
            DisplayAlert("Информация", "Операция выполнена", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }

    }

    private void UnbanUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.UnbanUserAsync(DisplayPromptAsync("Разбанить пользователя", "Кого разбанить?", "OK", string.Empty).Result);
            DisplayAlert("Информация", "Операция выполнена", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
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
                string banIssued = user.BanIssued ? "Да" : "Нет";
                string banReason = string.IsNullOrWhiteSpace(user.BanReason) ? "не забанен?" : user.BanReason;
                string permissions = user.Permissions.Operator ? "RWO" : user.Permissions.OptionsViewEdit ? "RW" : "R";

                userStrings.Add($"ИД: {user.Id} Имя: {user.Username} Забанен: {banIssued} Почему {banReason} Разрешения: {permissions}");
            }

            foreach (var str in userStrings)
            {
                message += $"{str}{Environment.NewLine}";
            }

            DisplayAlert("Список пользователей", message, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
    }

    private void DeleteUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.DeleteUserAsync(DisplayPromptAsync("Удалить пользователя", "Кого удалить?", "OK", string.Empty).Result);
            DisplayAlert("Информация", "Операция выполнена", "OK");
        } 
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
    }

    private void ChangePasswdButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Configuration.ClientFE.Client.ChangePasswordAsync(DisplayPromptAsync("Сменить пароль", "Кому сменить пароль?", "OK", string.Empty).Result, DisplayPromptAsync("Сменить пароль", "Какой новый?", "OK").Result);
            DisplayAlert("Информация", "Операция выполнена", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
    }

    private void ChangePermsButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            string username = DisplayPromptAsync("Сменить флаги", "Кому сменить флаги?", "OK", string.Empty).Result;
            string perm = DisplayPromptAsync("Сменить флаги", $"RWO - оператор{Environment.NewLine}RW - выдача конфигураций и чтение списков{Environment.NewLine}R - чтение списков", "OK", string.Empty).Result;

            if ((perm == "RWO") || (perm == "RW") || (perm == "R"))
            {
                Configuration.ClientFE.Client.ChangePermissionsAsync(username, perm);
                DisplayAlert("Информация", "Операция выполнена", "OK");
            }
            else
            {
                DisplayAlert("Ошибка", "Неправильно задан формат флагов.", "OK");
                return;
            }
        } catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Понятно");
        }
    }
}