namespace ASNAOrders.Web.Administration.Client.Desktop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CreateUser = new GroupBox();
            ButtonOKCreate = new Button();
            LabelFlagCreate = new Label();
            ComboBoxFlagCreate = new ComboBox();
            EditPasswordCreate = new TextBox();
            LabelPasswordCreate = new Label();
            EditUsernameCreate = new TextBox();
            LabelUsernameCreate = new Label();
            BanUser = new GroupBox();
            ButtonOKBan = new Button();
            EditReasonBan = new TextBox();
            LabelReasonBan = new Label();
            EditNameBan = new TextBox();
            LabelNameBan = new Label();
            UnbanUser = new GroupBox();
            ButtonOKUnban = new Button();
            EditNameUnban = new TextBox();
            LabelNameUnban = new Label();
            DeleteUser = new GroupBox();
            ButtonOKDelete = new Button();
            EditNameDelete = new TextBox();
            LabelNameDelete = new Label();
            ChangePassword = new GroupBox();
            ButtonOKPasswd = new Button();
            EditNewPassPasswd = new TextBox();
            LabelNewPassPasswd = new Label();
            EditNamePasswd = new TextBox();
            LabelNamePasswd = new Label();
            ChangePermissions = new GroupBox();
            ButtonOKPerms = new Button();
            ComboBoxFlagPerms = new ComboBox();
            LabelFlagPerms = new Label();
            EditNamePerms = new TextBox();
            LabelNamePerms = new Label();
            DBOptions = new GroupBox();
            ComboBoxDbTypeDatabase = new ComboBox();
            DBTypeDBOptions = new Label();
            SqliteSettings = new GroupBox();
            EditCatalogSqlite = new TextBox();
            LabelCatalogSqlite = new Label();
            MssqlSettings = new GroupBox();
            EditCatalogMssql = new TextBox();
            LabelCatalogMssql = new Label();
            EditPasswordMssql = new TextBox();
            LabelPasswordMssql = new Label();
            LabelUsernameMssql = new Label();
            EditUsernameMssql = new TextBox();
            EditPortMssql = new TextBox();
            LabelPortMssql = new Label();
            LabelHostMssql = new Label();
            EditHostMssql = new TextBox();
            LoggingOptions = new GroupBox();
            ComboBoxSinkLogging = new ComboBox();
            LabelSinkLogging = new Label();
            FileLoggingOptions = new GroupBox();
            EditFilenameFileLogging = new TextBox();
            EditPrefixFileLogging = new TextBox();
            LabelFilenameFIleLogging = new Label();
            LabelPrefixFileLogging = new Label();
            SecretGeneratorOptions = new GroupBox();
            LabelClientSecretTransmissionMethodSecretGenerator = new Label();
            ComboBoxClientSecretTransmissionMethodSecretGenerator = new ComboBox();
            CheckClientIdSetToAutoSecretGenerator = new CheckBox();
            EditClientIdSecretGenerator = new TextBox();
            LabelClientIdSecretGenerator = new Label();
            CheckClientSecretFilenameSetToAutoSecretGenerator = new CheckBox();
            EditClientSecretFilenameSecretGenerator = new TextBox();
            LabelClientSecretFilenameSecretGenerator = new Label();
            CheckClientSecretSetToAutoSecretGenerator = new CheckBox();
            EditClientSecretSecretGenerator = new TextBox();
            LabelClientSecretSecretGenerator = new Label();
            CheckSigningKeyFileSetToAutoSecretGenerator = new CheckBox();
            EditSigningKeyFileSecretGenerator = new TextBox();
            LabelSigningKeyFileSecretGenerator = new Label();
            CheckIssuerSigningKeySetToAutoSecretGenerator = new CheckBox();
            EditIssuerSigningKeySecretGenerator = new TextBox();
            LabelIssuerSigningKeySecretGenerator = new Label();
            PostOptions = new GroupBox();
            EditMailToPost = new TextBox();
            LabelMailToPost = new Label();
            ComboBoxMailSSLOptionsPost = new ComboBox();
            LabelMailSSLOptionsPost = new Label();
            EditMailPasswordPost = new TextBox();
            LabelMailPasswordPost = new Label();
            EditMailUsernamePost = new TextBox();
            LabelMailUsernamePost = new Label();
            EditMailPortPost = new TextBox();
            LabelMailPortPost = new Label();
            EditMailHostPost = new TextBox();
            LabelMailHostPost = new Label();
            ContentOptions = new GroupBox();
            EditXMLStockPathContent = new TextBox();
            LabelXMLStockPathContent = new Label();
            RabbitMQOptions = new GroupBox();
            EditMQVhostRabbitMQ = new TextBox();
            LabelMQVhostRabbitMQ = new Label();
            EditMQPortRabbitMQ = new TextBox();
            LabelMQPortRabbitMQ = new Label();
            EditMQHostnameRabbitMQ = new TextBox();
            LabelMQHostnameRabbitMQ = new Label();
            LoginOptions = new GroupBox();
            EditPortLogin = new TextBox();
            LabelPortLogin = new Label();
            EditHostnameLogin = new TextBox();
            LabelHostnameLogin = new Label();
            EditPasswordLogin = new TextBox();
            EditUsernameLogin = new TextBox();
            LabelPasswordLogin = new Label();
            LabelUsernameLogin = new Label();
            Information = new GroupBox();
            ListViewInformation = new ListView();
            Actions = new GroupBox();
            ButtonLoginToIFAction = new Button();
            ProgressBarStatusBarAction = new ProgressBar();
            ButtonExitAction = new Button();
            ButtonApplyConfigurationAction = new Button();
            GetAllUsers = new GroupBox();
            ButtonGetAllUsers = new Button();
            CreateUser.SuspendLayout();
            BanUser.SuspendLayout();
            UnbanUser.SuspendLayout();
            DeleteUser.SuspendLayout();
            ChangePassword.SuspendLayout();
            ChangePermissions.SuspendLayout();
            DBOptions.SuspendLayout();
            SqliteSettings.SuspendLayout();
            MssqlSettings.SuspendLayout();
            LoggingOptions.SuspendLayout();
            FileLoggingOptions.SuspendLayout();
            SecretGeneratorOptions.SuspendLayout();
            PostOptions.SuspendLayout();
            ContentOptions.SuspendLayout();
            RabbitMQOptions.SuspendLayout();
            LoginOptions.SuspendLayout();
            Information.SuspendLayout();
            Actions.SuspendLayout();
            GetAllUsers.SuspendLayout();
            SuspendLayout();
            // 
            // CreateUser
            // 
            CreateUser.Controls.Add(ButtonOKCreate);
            CreateUser.Controls.Add(LabelFlagCreate);
            CreateUser.Controls.Add(ComboBoxFlagCreate);
            CreateUser.Controls.Add(EditPasswordCreate);
            CreateUser.Controls.Add(LabelPasswordCreate);
            CreateUser.Controls.Add(EditUsernameCreate);
            CreateUser.Controls.Add(LabelUsernameCreate);
            CreateUser.Enabled = false;
            CreateUser.Location = new Point(12, 12);
            CreateUser.Name = "CreateUser";
            CreateUser.Size = new Size(250, 125);
            CreateUser.TabIndex = 0;
            CreateUser.TabStop = false;
            CreateUser.Text = "Создать пользователя";
            // 
            // ButtonOKCreate
            // 
            ButtonOKCreate.Location = new Point(146, 85);
            ButtonOKCreate.Name = "ButtonOKCreate";
            ButtonOKCreate.Size = new Size(98, 29);
            ButtonOKCreate.TabIndex = 7;
            ButtonOKCreate.Text = "Создать";
            ButtonOKCreate.UseVisualStyleBackColor = true;
            ButtonOKCreate.Click += ButtonOKCreate_Click;
            // 
            // LabelFlagCreate
            // 
            LabelFlagCreate.AutoSize = true;
            LabelFlagCreate.Location = new Point(6, 88);
            LabelFlagCreate.Name = "LabelFlagCreate";
            LabelFlagCreate.Size = new Size(42, 20);
            LabelFlagCreate.TabIndex = 6;
            LabelFlagCreate.Text = "Флаг";
            // 
            // ComboBoxFlagCreate
            // 
            ComboBoxFlagCreate.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxFlagCreate.FormattingEnabled = true;
            ComboBoxFlagCreate.Items.AddRange(new object[] { "RWO", "RW", "R" });
            ComboBoxFlagCreate.Location = new Point(74, 85);
            ComboBoxFlagCreate.Name = "ComboBoxFlagCreate";
            ComboBoxFlagCreate.Size = new Size(66, 28);
            ComboBoxFlagCreate.TabIndex = 4;
            // 
            // EditPasswordCreate
            // 
            EditPasswordCreate.Location = new Point(74, 52);
            EditPasswordCreate.Name = "EditPasswordCreate";
            EditPasswordCreate.PasswordChar = '*';
            EditPasswordCreate.Size = new Size(170, 27);
            EditPasswordCreate.TabIndex = 3;
            // 
            // LabelPasswordCreate
            // 
            LabelPasswordCreate.AutoSize = true;
            LabelPasswordCreate.Location = new Point(6, 55);
            LabelPasswordCreate.Name = "LabelPasswordCreate";
            LabelPasswordCreate.Size = new Size(62, 20);
            LabelPasswordCreate.TabIndex = 2;
            LabelPasswordCreate.Text = "Пароль";
            // 
            // EditUsernameCreate
            // 
            EditUsernameCreate.Location = new Point(74, 20);
            EditUsernameCreate.Name = "EditUsernameCreate";
            EditUsernameCreate.Size = new Size(170, 27);
            EditUsernameCreate.TabIndex = 1;
            // 
            // LabelUsernameCreate
            // 
            LabelUsernameCreate.AutoSize = true;
            LabelUsernameCreate.Location = new Point(6, 23);
            LabelUsernameCreate.Name = "LabelUsernameCreate";
            LabelUsernameCreate.Size = new Size(39, 20);
            LabelUsernameCreate.TabIndex = 0;
            LabelUsernameCreate.Text = "Имя";
            // 
            // BanUser
            // 
            BanUser.Controls.Add(ButtonOKBan);
            BanUser.Controls.Add(EditReasonBan);
            BanUser.Controls.Add(LabelReasonBan);
            BanUser.Controls.Add(EditNameBan);
            BanUser.Controls.Add(LabelNameBan);
            BanUser.Enabled = false;
            BanUser.Location = new Point(12, 143);
            BanUser.Name = "BanUser";
            BanUser.Size = new Size(250, 125);
            BanUser.TabIndex = 1;
            BanUser.TabStop = false;
            BanUser.Text = "Забанить пользователя";
            // 
            // ButtonOKBan
            // 
            ButtonOKBan.Location = new Point(150, 91);
            ButtonOKBan.Name = "ButtonOKBan";
            ButtonOKBan.Size = new Size(94, 29);
            ButtonOKBan.TabIndex = 8;
            ButtonOKBan.Text = "Забанить";
            ButtonOKBan.UseVisualStyleBackColor = true;
            ButtonOKBan.Click += ButtonOKBan_Click;
            // 
            // EditReasonBan
            // 
            EditReasonBan.Location = new Point(84, 58);
            EditReasonBan.Name = "EditReasonBan";
            EditReasonBan.Size = new Size(160, 27);
            EditReasonBan.TabIndex = 7;
            // 
            // LabelReasonBan
            // 
            LabelReasonBan.AutoSize = true;
            LabelReasonBan.Location = new Point(6, 61);
            LabelReasonBan.Name = "LabelReasonBan";
            LabelReasonBan.Size = new Size(72, 20);
            LabelReasonBan.TabIndex = 6;
            LabelReasonBan.Text = "Причина";
            // 
            // EditNameBan
            // 
            EditNameBan.Location = new Point(84, 26);
            EditNameBan.Name = "EditNameBan";
            EditNameBan.Size = new Size(160, 27);
            EditNameBan.TabIndex = 5;
            // 
            // LabelNameBan
            // 
            LabelNameBan.AutoSize = true;
            LabelNameBan.Location = new Point(6, 29);
            LabelNameBan.Name = "LabelNameBan";
            LabelNameBan.Size = new Size(39, 20);
            LabelNameBan.TabIndex = 4;
            LabelNameBan.Text = "Имя";
            // 
            // UnbanUser
            // 
            UnbanUser.Controls.Add(ButtonOKUnban);
            UnbanUser.Controls.Add(EditNameUnban);
            UnbanUser.Controls.Add(LabelNameUnban);
            UnbanUser.Enabled = false;
            UnbanUser.Location = new Point(12, 274);
            UnbanUser.Name = "UnbanUser";
            UnbanUser.Size = new Size(250, 125);
            UnbanUser.TabIndex = 2;
            UnbanUser.TabStop = false;
            UnbanUser.Text = "Разбанить пользователя";
            // 
            // ButtonOKUnban
            // 
            ButtonOKUnban.Location = new Point(150, 59);
            ButtonOKUnban.Name = "ButtonOKUnban";
            ButtonOKUnban.Size = new Size(94, 29);
            ButtonOKUnban.TabIndex = 2;
            ButtonOKUnban.Text = "Разбанить";
            ButtonOKUnban.UseVisualStyleBackColor = true;
            ButtonOKUnban.Click += ButtonOKUnban_Click;
            // 
            // EditNameUnban
            // 
            EditNameUnban.Location = new Point(74, 26);
            EditNameUnban.Name = "EditNameUnban";
            EditNameUnban.Size = new Size(170, 27);
            EditNameUnban.TabIndex = 1;
            // 
            // LabelNameUnban
            // 
            LabelNameUnban.AutoSize = true;
            LabelNameUnban.Location = new Point(10, 30);
            LabelNameUnban.Name = "LabelNameUnban";
            LabelNameUnban.Size = new Size(39, 20);
            LabelNameUnban.TabIndex = 0;
            LabelNameUnban.Text = "Имя";
            // 
            // DeleteUser
            // 
            DeleteUser.Controls.Add(ButtonOKDelete);
            DeleteUser.Controls.Add(EditNameDelete);
            DeleteUser.Controls.Add(LabelNameDelete);
            DeleteUser.Enabled = false;
            DeleteUser.Location = new Point(268, 12);
            DeleteUser.Name = "DeleteUser";
            DeleteUser.Size = new Size(250, 125);
            DeleteUser.TabIndex = 3;
            DeleteUser.TabStop = false;
            DeleteUser.Text = "Удалить пользователя";
            // 
            // ButtonOKDelete
            // 
            ButtonOKDelete.Location = new Point(146, 55);
            ButtonOKDelete.Name = "ButtonOKDelete";
            ButtonOKDelete.Size = new Size(94, 29);
            ButtonOKDelete.TabIndex = 5;
            ButtonOKDelete.Text = "Удалить";
            ButtonOKDelete.UseVisualStyleBackColor = true;
            ButtonOKDelete.Click += ButtonOKDelete_Click;
            // 
            // EditNameDelete
            // 
            EditNameDelete.Location = new Point(51, 22);
            EditNameDelete.Name = "EditNameDelete";
            EditNameDelete.Size = new Size(189, 27);
            EditNameDelete.TabIndex = 4;
            // 
            // LabelNameDelete
            // 
            LabelNameDelete.AutoSize = true;
            LabelNameDelete.Location = new Point(6, 23);
            LabelNameDelete.Name = "LabelNameDelete";
            LabelNameDelete.Size = new Size(39, 20);
            LabelNameDelete.TabIndex = 3;
            LabelNameDelete.Text = "Имя";
            // 
            // ChangePassword
            // 
            ChangePassword.Controls.Add(ButtonOKPasswd);
            ChangePassword.Controls.Add(EditNewPassPasswd);
            ChangePassword.Controls.Add(LabelNewPassPasswd);
            ChangePassword.Controls.Add(EditNamePasswd);
            ChangePassword.Controls.Add(LabelNamePasswd);
            ChangePassword.Enabled = false;
            ChangePassword.Location = new Point(268, 143);
            ChangePassword.Name = "ChangePassword";
            ChangePassword.Size = new Size(250, 125);
            ChangePassword.TabIndex = 4;
            ChangePassword.TabStop = false;
            ChangePassword.Text = "Сменить пароль";
            // 
            // ButtonOKPasswd
            // 
            ButtonOKPasswd.Location = new Point(150, 88);
            ButtonOKPasswd.Name = "ButtonOKPasswd";
            ButtonOKPasswd.Size = new Size(94, 29);
            ButtonOKPasswd.TabIndex = 13;
            ButtonOKPasswd.Text = "Сменить";
            ButtonOKPasswd.UseVisualStyleBackColor = true;
            ButtonOKPasswd.Click += ButtonOKPasswd_Click;
            // 
            // EditNewPassPasswd
            // 
            EditNewPassPasswd.Location = new Point(124, 58);
            EditNewPassPasswd.Name = "EditNewPassPasswd";
            EditNewPassPasswd.PasswordChar = '*';
            EditNewPassPasswd.Size = new Size(120, 27);
            EditNewPassPasswd.TabIndex = 12;
            // 
            // LabelNewPassPasswd
            // 
            LabelNewPassPasswd.AutoSize = true;
            LabelNewPassPasswd.Location = new Point(6, 58);
            LabelNewPassPasswd.Name = "LabelNewPassPasswd";
            LabelNewPassPasswd.Size = new Size(112, 20);
            LabelNewPassPasswd.TabIndex = 11;
            LabelNewPassPasswd.Text = "Новый пароль";
            // 
            // EditNamePasswd
            // 
            EditNamePasswd.Location = new Point(84, 26);
            EditNamePasswd.Name = "EditNamePasswd";
            EditNamePasswd.Size = new Size(160, 27);
            EditNamePasswd.TabIndex = 10;
            // 
            // LabelNamePasswd
            // 
            LabelNamePasswd.AutoSize = true;
            LabelNamePasswd.Location = new Point(6, 26);
            LabelNamePasswd.Name = "LabelNamePasswd";
            LabelNamePasswd.Size = new Size(39, 20);
            LabelNamePasswd.TabIndex = 9;
            LabelNamePasswd.Text = "Имя";
            // 
            // ChangePermissions
            // 
            ChangePermissions.Controls.Add(ButtonOKPerms);
            ChangePermissions.Controls.Add(ComboBoxFlagPerms);
            ChangePermissions.Controls.Add(LabelFlagPerms);
            ChangePermissions.Controls.Add(EditNamePerms);
            ChangePermissions.Controls.Add(LabelNamePerms);
            ChangePermissions.Enabled = false;
            ChangePermissions.Location = new Point(268, 274);
            ChangePermissions.Name = "ChangePermissions";
            ChangePermissions.Size = new Size(250, 125);
            ChangePermissions.TabIndex = 5;
            ChangePermissions.TabStop = false;
            ChangePermissions.Text = "Выдать флаги";
            // 
            // ButtonOKPerms
            // 
            ButtonOKPerms.Location = new Point(146, 90);
            ButtonOKPerms.Name = "ButtonOKPerms";
            ButtonOKPerms.Size = new Size(94, 29);
            ButtonOKPerms.TabIndex = 4;
            ButtonOKPerms.Text = "Выдать";
            ButtonOKPerms.UseVisualStyleBackColor = true;
            ButtonOKPerms.Click += ButtonOKPerms_Click;
            // 
            // ComboBoxFlagPerms
            // 
            ComboBoxFlagPerms.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxFlagPerms.FormattingEnabled = true;
            ComboBoxFlagPerms.Items.AddRange(new object[] { "RWO", "RW", "R" });
            ComboBoxFlagPerms.Location = new Point(70, 56);
            ComboBoxFlagPerms.Name = "ComboBoxFlagPerms";
            ComboBoxFlagPerms.Size = new Size(170, 28);
            ComboBoxFlagPerms.TabIndex = 3;
            // 
            // LabelFlagPerms
            // 
            LabelFlagPerms.AutoSize = true;
            LabelFlagPerms.Location = new Point(6, 59);
            LabelFlagPerms.Name = "LabelFlagPerms";
            LabelFlagPerms.Size = new Size(42, 20);
            LabelFlagPerms.TabIndex = 2;
            LabelFlagPerms.Text = "Флаг";
            // 
            // EditNamePerms
            // 
            EditNamePerms.Location = new Point(70, 23);
            EditNamePerms.Name = "EditNamePerms";
            EditNamePerms.Size = new Size(170, 27);
            EditNamePerms.TabIndex = 1;
            // 
            // LabelNamePerms
            // 
            LabelNamePerms.AutoSize = true;
            LabelNamePerms.Location = new Point(6, 26);
            LabelNamePerms.Name = "LabelNamePerms";
            LabelNamePerms.Size = new Size(39, 20);
            LabelNamePerms.TabIndex = 0;
            LabelNamePerms.Text = "Имя";
            LabelNamePerms.Click += LabelNamePerms_Click;
            // 
            // DBOptions
            // 
            DBOptions.Controls.Add(ComboBoxDbTypeDatabase);
            DBOptions.Controls.Add(DBTypeDBOptions);
            DBOptions.Enabled = false;
            DBOptions.Location = new Point(12, 405);
            DBOptions.Name = "DBOptions";
            DBOptions.Size = new Size(250, 64);
            DBOptions.TabIndex = 6;
            DBOptions.TabStop = false;
            DBOptions.Text = "Настройки БД";
            // 
            // ComboBoxDbTypeDatabase
            // 
            ComboBoxDbTypeDatabase.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxDbTypeDatabase.FormattingEnabled = true;
            ComboBoxDbTypeDatabase.Items.AddRange(new object[] { "SQLite Database", "Microsoft SQL Server" });
            ComboBoxDbTypeDatabase.Location = new Point(70, 20);
            ComboBoxDbTypeDatabase.Name = "ComboBoxDbTypeDatabase";
            ComboBoxDbTypeDatabase.Size = new Size(174, 28);
            ComboBoxDbTypeDatabase.TabIndex = 1;
            ComboBoxDbTypeDatabase.SelectedIndexChanged += ComboBoxDbTypeDatabase_SelectedIndexChanged;
            // 
            // DBTypeDBOptions
            // 
            DBTypeDBOptions.AutoSize = true;
            DBTypeDBOptions.Location = new Point(6, 23);
            DBTypeDBOptions.Name = "DBTypeDBOptions";
            DBTypeDBOptions.Size = new Size(58, 20);
            DBTypeDBOptions.TabIndex = 0;
            DBTypeDBOptions.Text = "Тип БД";
            // 
            // SqliteSettings
            // 
            SqliteSettings.Controls.Add(EditCatalogSqlite);
            SqliteSettings.Controls.Add(LabelCatalogSqlite);
            SqliteSettings.Enabled = false;
            SqliteSettings.Location = new Point(268, 405);
            SqliteSettings.Name = "SqliteSettings";
            SqliteSettings.Size = new Size(250, 64);
            SqliteSettings.TabIndex = 7;
            SqliteSettings.TabStop = false;
            SqliteSettings.Text = "Настройки SQLite";
            // 
            // EditCatalogSqlite
            // 
            EditCatalogSqlite.Location = new Point(75, 21);
            EditCatalogSqlite.Name = "EditCatalogSqlite";
            EditCatalogSqlite.Size = new Size(159, 27);
            EditCatalogSqlite.TabIndex = 1;
            EditCatalogSqlite.TextChanged += EditCatalogSqlite_TextChanged;
            // 
            // LabelCatalogSqlite
            // 
            LabelCatalogSqlite.AutoSize = true;
            LabelCatalogSqlite.Location = new Point(6, 23);
            LabelCatalogSqlite.Name = "LabelCatalogSqlite";
            LabelCatalogSqlite.Size = new Size(63, 20);
            LabelCatalogSqlite.TabIndex = 0;
            LabelCatalogSqlite.Text = "Каталог";
            LabelCatalogSqlite.Click += label1_Click;
            // 
            // MssqlSettings
            // 
            MssqlSettings.Controls.Add(EditCatalogMssql);
            MssqlSettings.Controls.Add(LabelCatalogMssql);
            MssqlSettings.Controls.Add(EditPasswordMssql);
            MssqlSettings.Controls.Add(LabelPasswordMssql);
            MssqlSettings.Controls.Add(LabelUsernameMssql);
            MssqlSettings.Controls.Add(EditUsernameMssql);
            MssqlSettings.Controls.Add(EditPortMssql);
            MssqlSettings.Controls.Add(LabelPortMssql);
            MssqlSettings.Controls.Add(LabelHostMssql);
            MssqlSettings.Controls.Add(EditHostMssql);
            MssqlSettings.Enabled = false;
            MssqlSettings.Location = new Point(12, 475);
            MssqlSettings.Name = "MssqlSettings";
            MssqlSettings.Size = new Size(506, 155);
            MssqlSettings.TabIndex = 8;
            MssqlSettings.TabStop = false;
            MssqlSettings.Text = "Настройки Microsoft SQL Server";
            // 
            // EditCatalogMssql
            // 
            EditCatalogMssql.Location = new Point(70, 93);
            EditCatalogMssql.Name = "EditCatalogMssql";
            EditCatalogMssql.Size = new Size(192, 27);
            EditCatalogMssql.TabIndex = 9;
            // 
            // LabelCatalogMssql
            // 
            LabelCatalogMssql.AutoSize = true;
            LabelCatalogMssql.Location = new Point(6, 93);
            LabelCatalogMssql.Name = "LabelCatalogMssql";
            LabelCatalogMssql.Size = new Size(63, 20);
            LabelCatalogMssql.TabIndex = 8;
            LabelCatalogMssql.Text = "Каталог";
            // 
            // EditPasswordMssql
            // 
            EditPasswordMssql.Location = new Point(314, 60);
            EditPasswordMssql.Name = "EditPasswordMssql";
            EditPasswordMssql.PasswordChar = '*';
            EditPasswordMssql.Size = new Size(125, 27);
            EditPasswordMssql.TabIndex = 7;
            // 
            // LabelPasswordMssql
            // 
            LabelPasswordMssql.AutoSize = true;
            LabelPasswordMssql.Location = new Point(200, 60);
            LabelPasswordMssql.Name = "LabelPasswordMssql";
            LabelPasswordMssql.Size = new Size(62, 20);
            LabelPasswordMssql.TabIndex = 6;
            LabelPasswordMssql.Text = "Пароль";
            // 
            // LabelUsernameMssql
            // 
            LabelUsernameMssql.AutoSize = true;
            LabelUsernameMssql.Location = new Point(201, 29);
            LabelUsernameMssql.Name = "LabelUsernameMssql";
            LabelUsernameMssql.Size = new Size(107, 20);
            LabelUsernameMssql.TabIndex = 5;
            LabelUsernameMssql.Text = "Пользователь";
            // 
            // EditUsernameMssql
            // 
            EditUsernameMssql.Location = new Point(314, 27);
            EditUsernameMssql.Name = "EditUsernameMssql";
            EditUsernameMssql.Size = new Size(125, 27);
            EditUsernameMssql.TabIndex = 4;
            // 
            // EditPortMssql
            // 
            EditPortMssql.Location = new Point(70, 60);
            EditPortMssql.Name = "EditPortMssql";
            EditPortMssql.Size = new Size(125, 27);
            EditPortMssql.TabIndex = 3;
            EditPortMssql.TextChanged += textBox1_TextChanged;
            // 
            // LabelPortMssql
            // 
            LabelPortMssql.AutoSize = true;
            LabelPortMssql.Location = new Point(6, 63);
            LabelPortMssql.Name = "LabelPortMssql";
            LabelPortMssql.Size = new Size(44, 20);
            LabelPortMssql.TabIndex = 2;
            LabelPortMssql.Text = "Порт";
            // 
            // LabelHostMssql
            // 
            LabelHostMssql.AutoSize = true;
            LabelHostMssql.Location = new Point(7, 29);
            LabelHostMssql.Name = "LabelHostMssql";
            LabelHostMssql.Size = new Size(40, 20);
            LabelHostMssql.TabIndex = 1;
            LabelHostMssql.Text = "Хост";
            // 
            // EditHostMssql
            // 
            EditHostMssql.Location = new Point(70, 26);
            EditHostMssql.Name = "EditHostMssql";
            EditHostMssql.Size = new Size(125, 27);
            EditHostMssql.TabIndex = 0;
            // 
            // LoggingOptions
            // 
            LoggingOptions.Controls.Add(ComboBoxSinkLogging);
            LoggingOptions.Controls.Add(LabelSinkLogging);
            LoggingOptions.Enabled = false;
            LoggingOptions.Location = new Point(15, 636);
            LoggingOptions.Name = "LoggingOptions";
            LoggingOptions.Size = new Size(275, 67);
            LoggingOptions.TabIndex = 9;
            LoggingOptions.TabStop = false;
            LoggingOptions.Text = "Настройки логирования";
            LoggingOptions.Enter += LoggingOptions_Enter;
            // 
            // ComboBoxSinkLogging
            // 
            ComboBoxSinkLogging.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSinkLogging.FormattingEnabled = true;
            ComboBoxSinkLogging.Items.AddRange(new object[] { "Журнал", "Журнал+почта", "Файл" });
            ComboBoxSinkLogging.Location = new Point(109, 23);
            ComboBoxSinkLogging.Name = "ComboBoxSinkLogging";
            ComboBoxSinkLogging.Size = new Size(151, 28);
            ComboBoxSinkLogging.TabIndex = 1;
            ComboBoxSinkLogging.SelectedIndexChanged += ComboBoxSinkLogging_SelectedIndexChanged;
            // 
            // LabelSinkLogging
            // 
            LabelSinkLogging.AutoSize = true;
            LabelSinkLogging.Location = new Point(6, 26);
            LabelSinkLogging.Name = "LabelSinkLogging";
            LabelSinkLogging.Size = new Size(97, 20);
            LabelSinkLogging.TabIndex = 0;
            LabelSinkLogging.Text = "Умывальник";
            // 
            // FileLoggingOptions
            // 
            FileLoggingOptions.Controls.Add(EditFilenameFileLogging);
            FileLoggingOptions.Controls.Add(EditPrefixFileLogging);
            FileLoggingOptions.Controls.Add(LabelFilenameFIleLogging);
            FileLoggingOptions.Controls.Add(LabelPrefixFileLogging);
            FileLoggingOptions.Enabled = false;
            FileLoggingOptions.Location = new Point(296, 636);
            FileLoggingOptions.Name = "FileLoggingOptions";
            FileLoggingOptions.Size = new Size(222, 104);
            FileLoggingOptions.TabIndex = 10;
            FileLoggingOptions.TabStop = false;
            FileLoggingOptions.Text = "Настр. логирования в файл";
            // 
            // EditFilenameFileLogging
            // 
            EditFilenameFileLogging.Location = new Point(103, 65);
            EditFilenameFileLogging.Name = "EditFilenameFileLogging";
            EditFilenameFileLogging.Size = new Size(109, 27);
            EditFilenameFileLogging.TabIndex = 3;
            // 
            // EditPrefixFileLogging
            // 
            EditPrefixFileLogging.Location = new Point(87, 32);
            EditPrefixFileLogging.Name = "EditPrefixFileLogging";
            EditPrefixFileLogging.Size = new Size(125, 27);
            EditPrefixFileLogging.TabIndex = 2;
            // 
            // LabelFilenameFIleLogging
            // 
            LabelFilenameFIleLogging.AutoSize = true;
            LabelFilenameFIleLogging.Location = new Point(11, 67);
            LabelFilenameFIleLogging.Name = "LabelFilenameFIleLogging";
            LabelFilenameFIleLogging.Size = new Size(86, 20);
            LabelFilenameFIleLogging.TabIndex = 1;
            LabelFilenameFIleLogging.Text = "Имя файла";
            // 
            // LabelPrefixFileLogging
            // 
            LabelPrefixFileLogging.AutoSize = true;
            LabelPrefixFileLogging.Location = new Point(11, 32);
            LabelPrefixFileLogging.Name = "LabelPrefixFileLogging";
            LabelPrefixFileLogging.Size = new Size(70, 20);
            LabelPrefixFileLogging.TabIndex = 0;
            LabelPrefixFileLogging.Text = "Префикс";
            // 
            // SecretGeneratorOptions
            // 
            SecretGeneratorOptions.Controls.Add(LabelClientSecretTransmissionMethodSecretGenerator);
            SecretGeneratorOptions.Controls.Add(ComboBoxClientSecretTransmissionMethodSecretGenerator);
            SecretGeneratorOptions.Controls.Add(CheckClientIdSetToAutoSecretGenerator);
            SecretGeneratorOptions.Controls.Add(EditClientIdSecretGenerator);
            SecretGeneratorOptions.Controls.Add(LabelClientIdSecretGenerator);
            SecretGeneratorOptions.Controls.Add(CheckClientSecretFilenameSetToAutoSecretGenerator);
            SecretGeneratorOptions.Controls.Add(EditClientSecretFilenameSecretGenerator);
            SecretGeneratorOptions.Controls.Add(LabelClientSecretFilenameSecretGenerator);
            SecretGeneratorOptions.Controls.Add(CheckClientSecretSetToAutoSecretGenerator);
            SecretGeneratorOptions.Controls.Add(EditClientSecretSecretGenerator);
            SecretGeneratorOptions.Controls.Add(LabelClientSecretSecretGenerator);
            SecretGeneratorOptions.Controls.Add(CheckSigningKeyFileSetToAutoSecretGenerator);
            SecretGeneratorOptions.Controls.Add(EditSigningKeyFileSecretGenerator);
            SecretGeneratorOptions.Controls.Add(LabelSigningKeyFileSecretGenerator);
            SecretGeneratorOptions.Controls.Add(CheckIssuerSigningKeySetToAutoSecretGenerator);
            SecretGeneratorOptions.Controls.Add(EditIssuerSigningKeySecretGenerator);
            SecretGeneratorOptions.Controls.Add(LabelIssuerSigningKeySecretGenerator);
            SecretGeneratorOptions.Enabled = false;
            SecretGeneratorOptions.Location = new Point(15, 709);
            SecretGeneratorOptions.Name = "SecretGeneratorOptions";
            SecretGeneratorOptions.Size = new Size(275, 263);
            SecretGeneratorOptions.TabIndex = 11;
            SecretGeneratorOptions.TabStop = false;
            SecretGeneratorOptions.Text = "Настройки генератора секретов";
            // 
            // LabelClientSecretTransmissionMethodSecretGenerator
            // 
            LabelClientSecretTransmissionMethodSecretGenerator.AutoSize = true;
            LabelClientSecretTransmissionMethodSecretGenerator.Location = new Point(13, 217);
            LabelClientSecretTransmissionMethodSecretGenerator.Name = "LabelClientSecretTransmissionMethodSecretGenerator";
            LabelClientSecretTransmissionMethodSecretGenerator.Size = new Size(134, 20);
            LabelClientSecretTransmissionMethodSecretGenerator.TabIndex = 16;
            LabelClientSecretTransmissionMethodSecretGenerator.Text = "Передача секрета";
            // 
            // ComboBoxClientSecretTransmissionMethodSecretGenerator
            // 
            ComboBoxClientSecretTransmissionMethodSecretGenerator.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxClientSecretTransmissionMethodSecretGenerator.FormattingEnabled = true;
            ComboBoxClientSecretTransmissionMethodSecretGenerator.Items.AddRange(new object[] { "Врем. файл", "Стд. файл (НЕБЕЗОПАСНО)", "Почта" });
            ComboBoxClientSecretTransmissionMethodSecretGenerator.Location = new Point(153, 214);
            ComboBoxClientSecretTransmissionMethodSecretGenerator.Name = "ComboBoxClientSecretTransmissionMethodSecretGenerator";
            ComboBoxClientSecretTransmissionMethodSecretGenerator.Size = new Size(106, 28);
            ComboBoxClientSecretTransmissionMethodSecretGenerator.TabIndex = 15;
            ComboBoxClientSecretTransmissionMethodSecretGenerator.SelectedIndexChanged += ComboBoxClientSecretTransmissionMethodSecretGenerator_SelectedIndexChanged;
            // 
            // CheckClientIdSetToAutoSecretGenerator
            // 
            CheckClientIdSetToAutoSecretGenerator.AutoSize = true;
            CheckClientIdSetToAutoSecretGenerator.Location = new Point(205, 175);
            CheckClientIdSetToAutoSecretGenerator.Name = "CheckClientIdSetToAutoSecretGenerator";
            CheckClientIdSetToAutoSecretGenerator.Size = new Size(64, 24);
            CheckClientIdSetToAutoSecretGenerator.TabIndex = 14;
            CheckClientIdSetToAutoSecretGenerator.Text = "Авто";
            CheckClientIdSetToAutoSecretGenerator.UseVisualStyleBackColor = true;
            CheckClientIdSetToAutoSecretGenerator.CheckedChanged += CheckClientIdSetToAutoSecretGenerator_CheckedChanged;
            // 
            // EditClientIdSecretGenerator
            // 
            EditClientIdSecretGenerator.Location = new Point(134, 172);
            EditClientIdSecretGenerator.Name = "EditClientIdSecretGenerator";
            EditClientIdSecretGenerator.Size = new Size(65, 27);
            EditClientIdSecretGenerator.TabIndex = 13;
            // 
            // LabelClientIdSecretGenerator
            // 
            LabelClientIdSecretGenerator.AutoSize = true;
            LabelClientIdSecretGenerator.Location = new Point(13, 175);
            LabelClientIdSecretGenerator.Name = "LabelClientIdSecretGenerator";
            LabelClientIdSecretGenerator.Size = new Size(89, 20);
            LabelClientIdSecretGenerator.TabIndex = 12;
            LabelClientIdSecretGenerator.Text = "ИД клиента";
            // 
            // CheckClientSecretFilenameSetToAutoSecretGenerator
            // 
            CheckClientSecretFilenameSetToAutoSecretGenerator.AutoSize = true;
            CheckClientSecretFilenameSetToAutoSecretGenerator.Location = new Point(205, 139);
            CheckClientSecretFilenameSetToAutoSecretGenerator.Name = "CheckClientSecretFilenameSetToAutoSecretGenerator";
            CheckClientSecretFilenameSetToAutoSecretGenerator.Size = new Size(64, 24);
            CheckClientSecretFilenameSetToAutoSecretGenerator.TabIndex = 11;
            CheckClientSecretFilenameSetToAutoSecretGenerator.Text = "Авто";
            CheckClientSecretFilenameSetToAutoSecretGenerator.UseVisualStyleBackColor = true;
            CheckClientSecretFilenameSetToAutoSecretGenerator.CheckedChanged += CheckClientSecretFilenameSetToAutoSecretGenerator_CheckedChanged;
            // 
            // EditClientSecretFilenameSecretGenerator
            // 
            EditClientSecretFilenameSecretGenerator.Location = new Point(134, 136);
            EditClientSecretFilenameSecretGenerator.Name = "EditClientSecretFilenameSecretGenerator";
            EditClientSecretFilenameSecretGenerator.Size = new Size(65, 27);
            EditClientSecretFilenameSecretGenerator.TabIndex = 10;
            // 
            // LabelClientSecretFilenameSecretGenerator
            // 
            LabelClientSecretFilenameSecretGenerator.AutoSize = true;
            LabelClientSecretFilenameSecretGenerator.Location = new Point(13, 139);
            LabelClientSecretFilenameSecretGenerator.Name = "LabelClientSecretFilenameSecretGenerator";
            LabelClientSecretFilenameSecretGenerator.Size = new Size(102, 20);
            LabelClientSecretFilenameSecretGenerator.TabIndex = 9;
            LabelClientSecretFilenameSecretGenerator.Text = "Файл секрета";
            // 
            // CheckClientSecretSetToAutoSecretGenerator
            // 
            CheckClientSecretSetToAutoSecretGenerator.AutoSize = true;
            CheckClientSecretSetToAutoSecretGenerator.Location = new Point(205, 103);
            CheckClientSecretSetToAutoSecretGenerator.Name = "CheckClientSecretSetToAutoSecretGenerator";
            CheckClientSecretSetToAutoSecretGenerator.Size = new Size(64, 24);
            CheckClientSecretSetToAutoSecretGenerator.TabIndex = 8;
            CheckClientSecretSetToAutoSecretGenerator.Text = "Авто";
            CheckClientSecretSetToAutoSecretGenerator.UseVisualStyleBackColor = true;
            CheckClientSecretSetToAutoSecretGenerator.CheckedChanged += CheckClientSecretSetToAutoSecretGenerator_CheckedChanged;
            // 
            // EditClientSecretSecretGenerator
            // 
            EditClientSecretSecretGenerator.Location = new Point(134, 100);
            EditClientSecretSecretGenerator.Name = "EditClientSecretSecretGenerator";
            EditClientSecretSecretGenerator.Size = new Size(65, 27);
            EditClientSecretSecretGenerator.TabIndex = 7;
            // 
            // LabelClientSecretSecretGenerator
            // 
            LabelClientSecretSecretGenerator.AutoSize = true;
            LabelClientSecretSecretGenerator.Location = new Point(13, 103);
            LabelClientSecretSecretGenerator.Name = "LabelClientSecretSecretGenerator";
            LabelClientSecretSecretGenerator.Size = new Size(115, 20);
            LabelClientSecretSecretGenerator.TabIndex = 6;
            LabelClientSecretSecretGenerator.Text = "Секрет клиента";
            // 
            // CheckSigningKeyFileSetToAutoSecretGenerator
            // 
            CheckSigningKeyFileSetToAutoSecretGenerator.AutoSize = true;
            CheckSigningKeyFileSetToAutoSecretGenerator.Location = new Point(205, 65);
            CheckSigningKeyFileSetToAutoSecretGenerator.Name = "CheckSigningKeyFileSetToAutoSecretGenerator";
            CheckSigningKeyFileSetToAutoSecretGenerator.Size = new Size(64, 24);
            CheckSigningKeyFileSetToAutoSecretGenerator.TabIndex = 5;
            CheckSigningKeyFileSetToAutoSecretGenerator.Text = "Авто";
            CheckSigningKeyFileSetToAutoSecretGenerator.UseVisualStyleBackColor = true;
            CheckSigningKeyFileSetToAutoSecretGenerator.CheckedChanged += CheckSigningKeyFileSetToAutoSecretGenerator_CheckedChanged;
            // 
            // EditSigningKeyFileSecretGenerator
            // 
            EditSigningKeyFileSecretGenerator.Location = new Point(109, 62);
            EditSigningKeyFileSecretGenerator.Name = "EditSigningKeyFileSecretGenerator";
            EditSigningKeyFileSecretGenerator.Size = new Size(90, 27);
            EditSigningKeyFileSecretGenerator.TabIndex = 4;
            // 
            // LabelSigningKeyFileSecretGenerator
            // 
            LabelSigningKeyFileSecretGenerator.AutoSize = true;
            LabelSigningKeyFileSecretGenerator.Location = new Point(13, 65);
            LabelSigningKeyFileSecretGenerator.Name = "LabelSigningKeyFileSecretGenerator";
            LabelSigningKeyFileSecretGenerator.Size = new Size(92, 20);
            LabelSigningKeyFileSecretGenerator.TabIndex = 3;
            LabelSigningKeyFileSecretGenerator.Text = "Файл ключа";
            // 
            // CheckIssuerSigningKeySetToAutoSecretGenerator
            // 
            CheckIssuerSigningKeySetToAutoSecretGenerator.AutoSize = true;
            CheckIssuerSigningKeySetToAutoSecretGenerator.Location = new Point(205, 29);
            CheckIssuerSigningKeySetToAutoSecretGenerator.Name = "CheckIssuerSigningKeySetToAutoSecretGenerator";
            CheckIssuerSigningKeySetToAutoSecretGenerator.Size = new Size(64, 24);
            CheckIssuerSigningKeySetToAutoSecretGenerator.TabIndex = 2;
            CheckIssuerSigningKeySetToAutoSecretGenerator.Text = "Авто";
            CheckIssuerSigningKeySetToAutoSecretGenerator.UseVisualStyleBackColor = true;
            CheckIssuerSigningKeySetToAutoSecretGenerator.CheckedChanged += CheckIssuerSigningKeySetToAutoSecretGenerator_CheckedChanged;
            // 
            // EditIssuerSigningKeySecretGenerator
            // 
            EditIssuerSigningKeySecretGenerator.Location = new Point(109, 26);
            EditIssuerSigningKeySecretGenerator.Name = "EditIssuerSigningKeySecretGenerator";
            EditIssuerSigningKeySecretGenerator.Size = new Size(90, 27);
            EditIssuerSigningKeySecretGenerator.TabIndex = 1;
            // 
            // LabelIssuerSigningKeySecretGenerator
            // 
            LabelIssuerSigningKeySecretGenerator.AutoSize = true;
            LabelIssuerSigningKeySecretGenerator.Location = new Point(13, 29);
            LabelIssuerSigningKeySecretGenerator.Name = "LabelIssuerSigningKeySecretGenerator";
            LabelIssuerSigningKeySecretGenerator.Size = new Size(97, 20);
            LabelIssuerSigningKeySecretGenerator.TabIndex = 0;
            LabelIssuerSigningKeySecretGenerator.Text = "Ключ токена";
            // 
            // PostOptions
            // 
            PostOptions.Controls.Add(EditMailToPost);
            PostOptions.Controls.Add(LabelMailToPost);
            PostOptions.Controls.Add(ComboBoxMailSSLOptionsPost);
            PostOptions.Controls.Add(LabelMailSSLOptionsPost);
            PostOptions.Controls.Add(EditMailPasswordPost);
            PostOptions.Controls.Add(LabelMailPasswordPost);
            PostOptions.Controls.Add(EditMailUsernamePost);
            PostOptions.Controls.Add(LabelMailUsernamePost);
            PostOptions.Controls.Add(EditMailPortPost);
            PostOptions.Controls.Add(LabelMailPortPost);
            PostOptions.Controls.Add(EditMailHostPost);
            PostOptions.Controls.Add(LabelMailHostPost);
            PostOptions.Enabled = false;
            PostOptions.Location = new Point(296, 743);
            PostOptions.Name = "PostOptions";
            PostOptions.Size = new Size(356, 161);
            PostOptions.TabIndex = 12;
            PostOptions.TabStop = false;
            PostOptions.Text = "Настройки почты (SMTP)";
            // 
            // EditMailToPost
            // 
            EditMailToPost.Location = new Point(159, 121);
            EditMailToPost.Name = "EditMailToPost";
            EditMailToPost.Size = new Size(179, 27);
            EditMailToPost.TabIndex = 11;
            // 
            // LabelMailToPost
            // 
            LabelMailToPost.AutoSize = true;
            LabelMailToPost.Location = new Point(19, 123);
            LabelMailToPost.Name = "LabelMailToPost";
            LabelMailToPost.Size = new Size(134, 20);
            LabelMailToPost.TabIndex = 10;
            LabelMailToPost.Text = "Адрес получателя";
            // 
            // ComboBoxMailSSLOptionsPost
            // 
            ComboBoxMailSSLOptionsPost.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxMailSSLOptionsPost.FormattingEnabled = true;
            ComboBoxMailSSLOptionsPost.Items.AddRange(new object[] { "SSL", "STARTTLS (по возможности)", "Авто", "Нет" });
            ComboBoxMailSSLOptionsPost.Location = new Point(216, 87);
            ComboBoxMailSSLOptionsPost.Name = "ComboBoxMailSSLOptionsPost";
            ComboBoxMailSSLOptionsPost.Size = new Size(122, 28);
            ComboBoxMailSSLOptionsPost.TabIndex = 9;
            // 
            // LabelMailSSLOptionsPost
            // 
            LabelMailSSLOptionsPost.AutoSize = true;
            LabelMailSSLOptionsPost.Location = new Point(19, 90);
            LabelMailSSLOptionsPost.Name = "LabelMailSSLOptionsPost";
            LabelMailSSLOptionsPost.Size = new Size(191, 20);
            LabelMailSSLOptionsPost.TabIndex = 8;
            LabelMailSSLOptionsPost.Text = "Параметры безопасности";
            // 
            // EditMailPasswordPost
            // 
            EditMailPasswordPost.Location = new Point(246, 54);
            EditMailPasswordPost.Name = "EditMailPasswordPost";
            EditMailPasswordPost.PasswordChar = '*';
            EditMailPasswordPost.Size = new Size(92, 27);
            EditMailPasswordPost.TabIndex = 7;
            // 
            // LabelMailPasswordPost
            // 
            LabelMailPasswordPost.AutoSize = true;
            LabelMailPasswordPost.Location = new Point(178, 57);
            LabelMailPasswordPost.Name = "LabelMailPasswordPost";
            LabelMailPasswordPost.Size = new Size(62, 20);
            LabelMailPasswordPost.TabIndex = 6;
            LabelMailPasswordPost.Text = "Пароль";
            // 
            // EditMailUsernamePost
            // 
            EditMailUsernamePost.Location = new Point(85, 54);
            EditMailUsernamePost.Name = "EditMailUsernamePost";
            EditMailUsernamePost.Size = new Size(87, 27);
            EditMailUsernamePost.TabIndex = 5;
            // 
            // LabelMailUsernamePost
            // 
            LabelMailUsernamePost.AutoSize = true;
            LabelMailUsernamePost.Location = new Point(19, 57);
            LabelMailUsernamePost.Name = "LabelMailUsernamePost";
            LabelMailUsernamePost.Size = new Size(52, 20);
            LabelMailUsernamePost.TabIndex = 4;
            LabelMailUsernamePost.Text = "Логин";
            // 
            // EditMailPortPost
            // 
            EditMailPortPost.Location = new Point(246, 21);
            EditMailPortPost.Name = "EditMailPortPost";
            EditMailPortPost.Size = new Size(92, 27);
            EditMailPortPost.TabIndex = 3;
            // 
            // LabelMailPortPost
            // 
            LabelMailPortPost.AutoSize = true;
            LabelMailPortPost.Location = new Point(196, 24);
            LabelMailPortPost.Name = "LabelMailPortPost";
            LabelMailPortPost.Size = new Size(44, 20);
            LabelMailPortPost.TabIndex = 2;
            LabelMailPortPost.Text = "Порт";
            // 
            // EditMailHostPost
            // 
            EditMailHostPost.Location = new Point(85, 21);
            EditMailHostPost.Name = "EditMailHostPost";
            EditMailHostPost.Size = new Size(87, 27);
            EditMailHostPost.TabIndex = 1;
            // 
            // LabelMailHostPost
            // 
            LabelMailHostPost.AutoSize = true;
            LabelMailHostPost.Location = new Point(19, 24);
            LabelMailHostPost.Name = "LabelMailHostPost";
            LabelMailHostPost.Size = new Size(60, 20);
            LabelMailHostPost.TabIndex = 0;
            LabelMailHostPost.Text = "Сервер";
            // 
            // ContentOptions
            // 
            ContentOptions.Controls.Add(EditXMLStockPathContent);
            ContentOptions.Controls.Add(LabelXMLStockPathContent);
            ContentOptions.Enabled = false;
            ContentOptions.Location = new Point(296, 910);
            ContentOptions.Name = "ContentOptions";
            ContentOptions.Size = new Size(356, 62);
            ContentOptions.TabIndex = 13;
            ContentOptions.TabStop = false;
            ContentOptions.Text = "Настройки контента";
            // 
            // EditXMLStockPathContent
            // 
            EditXMLStockPathContent.Location = new Point(216, 23);
            EditXMLStockPathContent.Name = "EditXMLStockPathContent";
            EditXMLStockPathContent.Size = new Size(132, 27);
            EditXMLStockPathContent.TabIndex = 1;
            // 
            // LabelXMLStockPathContent
            // 
            LabelXMLStockPathContent.AutoSize = true;
            LabelXMLStockPathContent.Location = new Point(11, 26);
            LabelXMLStockPathContent.Name = "LabelXMLStockPathContent";
            LabelXMLStockPathContent.Size = new Size(197, 20);
            LabelXMLStockPathContent.TabIndex = 0;
            LabelXMLStockPathContent.Text = "Корневая папка (отн. путь)";
            // 
            // RabbitMQOptions
            // 
            RabbitMQOptions.Controls.Add(EditMQVhostRabbitMQ);
            RabbitMQOptions.Controls.Add(LabelMQVhostRabbitMQ);
            RabbitMQOptions.Controls.Add(EditMQPortRabbitMQ);
            RabbitMQOptions.Controls.Add(LabelMQPortRabbitMQ);
            RabbitMQOptions.Controls.Add(EditMQHostnameRabbitMQ);
            RabbitMQOptions.Controls.Add(LabelMQHostnameRabbitMQ);
            RabbitMQOptions.Location = new Point(658, 743);
            RabbitMQOptions.Name = "RabbitMQOptions";
            RabbitMQOptions.Size = new Size(206, 229);
            RabbitMQOptions.TabIndex = 14;
            RabbitMQOptions.TabStop = false;
            RabbitMQOptions.Text = "Настройки RabbitMQ";
            // 
            // EditMQVhostRabbitMQ
            // 
            EditMQVhostRabbitMQ.Location = new Point(10, 183);
            EditMQVhostRabbitMQ.Name = "EditMQVhostRabbitMQ";
            EditMQVhostRabbitMQ.Size = new Size(184, 27);
            EditMQVhostRabbitMQ.TabIndex = 5;
            // 
            // LabelMQVhostRabbitMQ
            // 
            LabelMQVhostRabbitMQ.AutoSize = true;
            LabelMQVhostRabbitMQ.Location = new Point(7, 160);
            LabelMQVhostRabbitMQ.Name = "LabelMQVhostRabbitMQ";
            LabelMQVhostRabbitMQ.Size = new Size(138, 20);
            LabelMQVhostRabbitMQ.TabIndex = 4;
            LabelMQVhostRabbitMQ.Text = "Виртуальный хост:";
            // 
            // EditMQPortRabbitMQ
            // 
            EditMQPortRabbitMQ.Location = new Point(10, 120);
            EditMQPortRabbitMQ.Name = "EditMQPortRabbitMQ";
            EditMQPortRabbitMQ.Size = new Size(184, 27);
            EditMQPortRabbitMQ.TabIndex = 3;
            // 
            // LabelMQPortRabbitMQ
            // 
            LabelMQPortRabbitMQ.AutoSize = true;
            LabelMQPortRabbitMQ.Location = new Point(10, 97);
            LabelMQPortRabbitMQ.Name = "LabelMQPortRabbitMQ";
            LabelMQPortRabbitMQ.Size = new Size(180, 20);
            LabelMQPortRabbitMQ.TabIndex = 2;
            LabelMQPortRabbitMQ.Text = "Порт сервера RabbitMQ:";
            LabelMQPortRabbitMQ.Click += LabelMQPortRabbitMQ_Click;
            // 
            // EditMQHostnameRabbitMQ
            // 
            EditMQHostnameRabbitMQ.Location = new Point(10, 54);
            EditMQHostnameRabbitMQ.Name = "EditMQHostnameRabbitMQ";
            EditMQHostnameRabbitMQ.Size = new Size(184, 27);
            EditMQHostnameRabbitMQ.TabIndex = 1;
            // 
            // LabelMQHostnameRabbitMQ
            // 
            LabelMQHostnameRabbitMQ.AutoSize = true;
            LabelMQHostnameRabbitMQ.Location = new Point(10, 31);
            LabelMQHostnameRabbitMQ.Name = "LabelMQHostnameRabbitMQ";
            LabelMQHostnameRabbitMQ.Size = new Size(187, 20);
            LabelMQHostnameRabbitMQ.TabIndex = 0;
            LabelMQHostnameRabbitMQ.Text = "Адрес сервера RabbitMQ:";
            // 
            // LoginOptions
            // 
            LoginOptions.Controls.Add(EditPortLogin);
            LoginOptions.Controls.Add(LabelPortLogin);
            LoginOptions.Controls.Add(EditHostnameLogin);
            LoginOptions.Controls.Add(LabelHostnameLogin);
            LoginOptions.Controls.Add(EditPasswordLogin);
            LoginOptions.Controls.Add(EditUsernameLogin);
            LoginOptions.Controls.Add(LabelPasswordLogin);
            LoginOptions.Controls.Add(LabelUsernameLogin);
            LoginOptions.Location = new Point(524, 589);
            LoginOptions.Name = "LoginOptions";
            LoginOptions.Size = new Size(340, 151);
            LoginOptions.TabIndex = 15;
            LoginOptions.TabStop = false;
            LoginOptions.Text = "Вход в интерфейс администрации";
            LoginOptions.Enter += LoginOptions_Enter;
            // 
            // EditPortLogin
            // 
            EditPortLogin.Location = new Point(266, 31);
            EditPortLogin.Name = "EditPortLogin";
            EditPortLogin.Size = new Size(68, 27);
            EditPortLogin.TabIndex = 7;
            // 
            // LabelPortLogin
            // 
            LabelPortLogin.AutoSize = true;
            LabelPortLogin.Location = new Point(216, 34);
            LabelPortLogin.Name = "LabelPortLogin";
            LabelPortLogin.Size = new Size(44, 20);
            LabelPortLogin.TabIndex = 6;
            LabelPortLogin.Text = "Порт";
            // 
            // EditHostnameLogin
            // 
            EditHostnameLogin.Location = new Point(89, 31);
            EditHostnameLogin.Name = "EditHostnameLogin";
            EditHostnameLogin.Size = new Size(121, 27);
            EditHostnameLogin.TabIndex = 5;
            // 
            // LabelHostnameLogin
            // 
            LabelHostnameLogin.AutoSize = true;
            LabelHostnameLogin.Location = new Point(9, 34);
            LabelHostnameLogin.Name = "LabelHostnameLogin";
            LabelHostnameLogin.Size = new Size(80, 20);
            LabelHostnameLogin.TabIndex = 4;
            LabelHostnameLogin.Text = "Имя хоста";
            // 
            // EditPasswordLogin
            // 
            EditPasswordLogin.Location = new Point(89, 109);
            EditPasswordLogin.Name = "EditPasswordLogin";
            EditPasswordLogin.PasswordChar = '*';
            EditPasswordLogin.Size = new Size(245, 27);
            EditPasswordLogin.TabIndex = 3;
            // 
            // EditUsernameLogin
            // 
            EditUsernameLogin.Location = new Point(89, 70);
            EditUsernameLogin.Name = "EditUsernameLogin";
            EditUsernameLogin.Size = new Size(245, 27);
            EditUsernameLogin.TabIndex = 2;
            // 
            // LabelPasswordLogin
            // 
            LabelPasswordLogin.AutoSize = true;
            LabelPasswordLogin.Location = new Point(9, 112);
            LabelPasswordLogin.Name = "LabelPasswordLogin";
            LabelPasswordLogin.Size = new Size(62, 20);
            LabelPasswordLogin.TabIndex = 1;
            LabelPasswordLogin.Text = "Пароль";
            // 
            // LabelUsernameLogin
            // 
            LabelUsernameLogin.AutoSize = true;
            LabelUsernameLogin.Location = new Point(9, 73);
            LabelUsernameLogin.Name = "LabelUsernameLogin";
            LabelUsernameLogin.Size = new Size(52, 20);
            LabelUsernameLogin.TabIndex = 0;
            LabelUsernameLogin.Text = "Логин";
            // 
            // Information
            // 
            Information.Controls.Add(ListViewInformation);
            Information.Location = new Point(533, 143);
            Information.Name = "Information";
            Information.Size = new Size(331, 336);
            Information.TabIndex = 16;
            Information.TabStop = false;
            Information.Text = "Информация";
            // 
            // ListViewInformation
            // 
            ListViewInformation.Location = new Point(9, 26);
            ListViewInformation.Name = "ListViewInformation";
            ListViewInformation.Size = new Size(313, 300);
            ListViewInformation.TabIndex = 17;
            ListViewInformation.UseCompatibleStateImageBehavior = false;
            ListViewInformation.View = View.Details;
            // 
            // Actions
            // 
            Actions.Controls.Add(ButtonLoginToIFAction);
            Actions.Controls.Add(ProgressBarStatusBarAction);
            Actions.Controls.Add(ButtonExitAction);
            Actions.Controls.Add(ButtonApplyConfigurationAction);
            Actions.Location = new Point(534, 485);
            Actions.Name = "Actions";
            Actions.Size = new Size(329, 98);
            Actions.TabIndex = 17;
            Actions.TabStop = false;
            Actions.Text = "Действия";
            // 
            // ButtonLoginToIFAction
            // 
            ButtonLoginToIFAction.Location = new Point(8, 61);
            ButtonLoginToIFAction.Name = "ButtonLoginToIFAction";
            ButtonLoginToIFAction.Size = new Size(163, 29);
            ButtonLoginToIFAction.TabIndex = 3;
            ButtonLoginToIFAction.Text = "Вход в интерфейс";
            ButtonLoginToIFAction.UseVisualStyleBackColor = true;
            ButtonLoginToIFAction.Click += ButtonLoginToIFAction_Click;
            // 
            // ProgressBarStatusBarAction
            // 
            ProgressBarStatusBarAction.Location = new Point(177, 61);
            ProgressBarStatusBarAction.Name = "ProgressBarStatusBarAction";
            ProgressBarStatusBarAction.Size = new Size(146, 29);
            ProgressBarStatusBarAction.TabIndex = 2;
            ProgressBarStatusBarAction.Click += ProgressBarStatusBarAction_Click;
            // 
            // ButtonExitAction
            // 
            ButtonExitAction.Location = new Point(256, 26);
            ButtonExitAction.Name = "ButtonExitAction";
            ButtonExitAction.Size = new Size(67, 29);
            ButtonExitAction.TabIndex = 1;
            ButtonExitAction.Text = "Выход";
            ButtonExitAction.UseVisualStyleBackColor = true;
            ButtonExitAction.Click += ButtonExitAction_Click;
            // 
            // ButtonApplyConfigurationAction
            // 
            ButtonApplyConfigurationAction.Enabled = false;
            ButtonApplyConfigurationAction.Location = new Point(8, 26);
            ButtonApplyConfigurationAction.Name = "ButtonApplyConfigurationAction";
            ButtonApplyConfigurationAction.Size = new Size(242, 29);
            ButtonApplyConfigurationAction.TabIndex = 0;
            ButtonApplyConfigurationAction.Text = "Применить конфигурацию";
            ButtonApplyConfigurationAction.UseVisualStyleBackColor = true;
            ButtonApplyConfigurationAction.Click += ButtonApplyConfigurationAction_Click;
            // 
            // GetAllUsers
            // 
            GetAllUsers.Controls.Add(ButtonGetAllUsers);
            GetAllUsers.Enabled = false;
            GetAllUsers.Location = new Point(533, 12);
            GetAllUsers.Name = "GetAllUsers";
            GetAllUsers.Size = new Size(330, 125);
            GetAllUsers.TabIndex = 18;
            GetAllUsers.TabStop = false;
            GetAllUsers.Text = "Получить информацию о пользователях";
            // 
            // ButtonGetAllUsers
            // 
            ButtonGetAllUsers.Font = new Font("Segoe UI", 24F);
            ButtonGetAllUsers.Location = new Point(11, 24);
            ButtonGetAllUsers.Name = "ButtonGetAllUsers";
            ButtonGetAllUsers.Size = new Size(311, 90);
            ButtonGetAllUsers.TabIndex = 0;
            ButtonGetAllUsers.Text = "Получить";
            ButtonGetAllUsers.UseVisualStyleBackColor = true;
            ButtonGetAllUsers.Click += ButtonGetAllUsers_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 984);
            Controls.Add(GetAllUsers);
            Controls.Add(Actions);
            Controls.Add(Information);
            Controls.Add(LoginOptions);
            Controls.Add(RabbitMQOptions);
            Controls.Add(ContentOptions);
            Controls.Add(PostOptions);
            Controls.Add(SecretGeneratorOptions);
            Controls.Add(FileLoggingOptions);
            Controls.Add(LoggingOptions);
            Controls.Add(MssqlSettings);
            Controls.Add(SqliteSettings);
            Controls.Add(DBOptions);
            Controls.Add(ChangePermissions);
            Controls.Add(ChangePassword);
            Controls.Add(DeleteUser);
            Controls.Add(UnbanUser);
            Controls.Add(BanUser);
            Controls.Add(CreateUser);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            ShowIcon = false;
            Text = "Интерфейс администрации";
            FormClosing += MainForm_FormClosing;
            CreateUser.ResumeLayout(false);
            CreateUser.PerformLayout();
            BanUser.ResumeLayout(false);
            BanUser.PerformLayout();
            UnbanUser.ResumeLayout(false);
            UnbanUser.PerformLayout();
            DeleteUser.ResumeLayout(false);
            DeleteUser.PerformLayout();
            ChangePassword.ResumeLayout(false);
            ChangePassword.PerformLayout();
            ChangePermissions.ResumeLayout(false);
            ChangePermissions.PerformLayout();
            DBOptions.ResumeLayout(false);
            DBOptions.PerformLayout();
            SqliteSettings.ResumeLayout(false);
            SqliteSettings.PerformLayout();
            MssqlSettings.ResumeLayout(false);
            MssqlSettings.PerformLayout();
            LoggingOptions.ResumeLayout(false);
            LoggingOptions.PerformLayout();
            FileLoggingOptions.ResumeLayout(false);
            FileLoggingOptions.PerformLayout();
            SecretGeneratorOptions.ResumeLayout(false);
            SecretGeneratorOptions.PerformLayout();
            PostOptions.ResumeLayout(false);
            PostOptions.PerformLayout();
            ContentOptions.ResumeLayout(false);
            ContentOptions.PerformLayout();
            RabbitMQOptions.ResumeLayout(false);
            RabbitMQOptions.PerformLayout();
            LoginOptions.ResumeLayout(false);
            LoginOptions.PerformLayout();
            Information.ResumeLayout(false);
            Actions.ResumeLayout(false);
            GetAllUsers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox CreateUser;
        private GroupBox BanUser;
        private GroupBox UnbanUser;
        private GroupBox DeleteUser;
        private GroupBox ChangePassword;
        private GroupBox ChangePermissions;
        private TextBox EditPasswordCreate;
        private Label LabelPasswordCreate;
        private TextBox EditUsernameCreate;
        private Label LabelUsernameCreate;
        private Button ButtonOKCreate;
        private Label LabelFlagCreate;
        private ComboBox ComboBoxFlagCreate;
        private TextBox EditReasonBan;
        private Label LabelReasonBan;
        private TextBox EditNameBan;
        private Label LabelNameBan;
        private Button ButtonOKBan;
        private TextBox EditNameUnban;
        private Label LabelNameUnban;
        private Button ButtonOKUnban;
        private Button ButtonOKDelete;
        private TextBox EditNameDelete;
        private Label LabelNameDelete;
        private Button ButtonOKPasswd;
        private TextBox EditNewPassPasswd;
        private Label LabelNewPassPasswd;
        private TextBox EditNamePasswd;
        private Label LabelNamePasswd;
        private Label LabelNamePerms;
        private Label LabelFlagPerms;
        private TextBox EditNamePerms;
        private Button ButtonOKPerms;
        private ComboBox ComboBoxFlagPerms;
        private GroupBox DBOptions;
        private Label DBTypeDBOptions;
        private ComboBox ComboBoxDbTypeDatabase;
        private GroupBox SqliteSettings;
        private Label LabelCatalogSqlite;
        private TextBox EditCatalogSqlite;
        private GroupBox MssqlSettings;
        private Label LabelHostMssql;
        private TextBox EditHostMssql;
        private TextBox EditPortMssql;
        private Label LabelPortMssql;
        private TextBox EditCatalogMssql;
        private Label LabelCatalogMssql;
        private TextBox EditPasswordMssql;
        private Label LabelPasswordMssql;
        private Label LabelUsernameMssql;
        private TextBox EditUsernameMssql;
        private GroupBox LoggingOptions;
        private Label LabelSinkLogging;
        private ComboBox ComboBoxSinkLogging;
        private GroupBox FileLoggingOptions;
        private TextBox EditFilenameFileLogging;
        private TextBox EditPrefixFileLogging;
        private Label LabelFilenameFIleLogging;
        private Label LabelPrefixFileLogging;
        private GroupBox SecretGeneratorOptions;
        private CheckBox CheckSigningKeyFileSetToAutoSecretGenerator;
        private TextBox EditSigningKeyFileSecretGenerator;
        private Label LabelSigningKeyFileSecretGenerator;
        private CheckBox CheckIssuerSigningKeySetToAutoSecretGenerator;
        private TextBox EditIssuerSigningKeySecretGenerator;
        private Label LabelIssuerSigningKeySecretGenerator;
        private CheckBox CheckClientIdSetToAutoSecretGenerator;
        private TextBox EditClientIdSecretGenerator;
        private Label LabelClientIdSecretGenerator;
        private CheckBox CheckClientSecretFilenameSetToAutoSecretGenerator;
        private TextBox EditClientSecretFilenameSecretGenerator;
        private Label LabelClientSecretFilenameSecretGenerator;
        private CheckBox CheckClientSecretSetToAutoSecretGenerator;
        private TextBox EditClientSecretSecretGenerator;
        private Label LabelClientSecretSecretGenerator;
        private Label LabelClientSecretTransmissionMethodSecretGenerator;
        private ComboBox ComboBoxClientSecretTransmissionMethodSecretGenerator;
        private GroupBox PostOptions;
        private Label LabelMailHostPost;
        private TextBox EditMailHostPost;
        private TextBox EditMailPasswordPost;
        private Label LabelMailPasswordPost;
        private TextBox EditMailUsernamePost;
        private Label LabelMailUsernamePost;
        private TextBox EditMailPortPost;
        private Label LabelMailPortPost;
        private ComboBox ComboBoxMailSSLOptionsPost;
        private Label LabelMailSSLOptionsPost;
        private GroupBox ContentOptions;
        private Label LabelXMLStockPathContent;
        private TextBox EditXMLStockPathContent;
        private GroupBox RabbitMQOptions;
        private TextBox EditMQPortRabbitMQ;
        private Label LabelMQPortRabbitMQ;
        private TextBox EditMQHostnameRabbitMQ;
        private Label LabelMQHostnameRabbitMQ;
        private Label LabelMQVhostRabbitMQ;
        private TextBox EditMQVhostRabbitMQ;
        private GroupBox LoginOptions;
        private Label LabelPasswordLogin;
        private Label LabelUsernameLogin;
        private TextBox EditPasswordLogin;
        private TextBox EditUsernameLogin;
        private Label LabelPortLogin;
        private TextBox EditHostnameLogin;
        private Label LabelHostnameLogin;
        private Label LabelMailToPost;
        private TextBox EditPortLogin;
        private TextBox EditMailToPost;
        private GroupBox Information;
        private ListView ListViewInformation;
        private GroupBox Actions;
        private ProgressBar ProgressBarStatusBarAction;
        private Button ButtonExitAction;
        private Button ButtonApplyConfigurationAction;
        private GroupBox GetAllUsers;
        private Button ButtonGetAllUsers;
        private Button ButtonLoginToIFAction;
    }
}
