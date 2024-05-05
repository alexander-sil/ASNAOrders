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
            comboBox1 = new ComboBox();
            DBTypeDBOptions = new Label();
            SqliteSettings = new GroupBox();
            Label = new Label();
            CreateUser.SuspendLayout();
            BanUser.SuspendLayout();
            UnbanUser.SuspendLayout();
            DeleteUser.SuspendLayout();
            ChangePassword.SuspendLayout();
            ChangePermissions.SuspendLayout();
            DBOptions.SuspendLayout();
            SqliteSettings.SuspendLayout();
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
            DBOptions.Controls.Add(comboBox1);
            DBOptions.Controls.Add(DBTypeDBOptions);
            DBOptions.Location = new Point(12, 405);
            DBOptions.Name = "DBOptions";
            DBOptions.Size = new Size(250, 64);
            DBOptions.TabIndex = 6;
            DBOptions.TabStop = false;
            DBOptions.Text = "Настройки БД";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "SQLite Database", "Microsoft SQL Server" });
            comboBox1.Location = new Point(70, 20);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 1;
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
            SqliteSettings.Controls.Add(Label);
            SqliteSettings.Location = new Point(274, 405);
            SqliteSettings.Name = "SqliteSettings";
            SqliteSettings.Size = new Size(244, 125);
            SqliteSettings.TabIndex = 7;
            SqliteSettings.TabStop = false;
            SqliteSettings.Text = "Настройки SQLite";
            // 
            // Label
            // 
            Label.AutoSize = true;
            Label.Location = new Point(6, 23);
            Label.Name = "Label";
            Label.Size = new Size(50, 20);
            Label.TabIndex = 0;
            Label.Text = "label1";
            Label.Click += label1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 984);
            Controls.Add(SqliteSettings);
            Controls.Add(DBOptions);
            Controls.Add(ChangePermissions);
            Controls.Add(ChangePassword);
            Controls.Add(DeleteUser);
            Controls.Add(UnbanUser);
            Controls.Add(BanUser);
            Controls.Add(CreateUser);
            Name = "MainForm";
            Text = "Интерфейс администрации";
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
        private ComboBox comboBox1;
        private GroupBox SqliteSettings;
        private Label Label;
    }
}
