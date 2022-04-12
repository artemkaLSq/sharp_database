namespace bd
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Firstname = new System.Windows.Forms.Label();
            this.Lastname = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.UserComboBox = new System.Windows.Forms.ComboBox();
            this.UserNotFound = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(104, 139);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // Firstname
            // 
            this.Firstname.AutoSize = true;
            this.Firstname.Location = new System.Drawing.Point(101, 79);
            this.Firstname.Name = "Firstname";
            this.Firstname.Size = new System.Drawing.Size(29, 13);
            this.Firstname.TabIndex = 2;
            this.Firstname.Text = "Имя";
            // 
            // Lastname
            // 
            this.Lastname.AutoSize = true;
            this.Lastname.Location = new System.Drawing.Point(101, 123);
            this.Lastname.Name = "Lastname";
            this.Lastname.Size = new System.Drawing.Size(56, 13);
            this.Lastname.TabIndex = 3;
            this.Lastname.Text = "Фамилия";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(116, 192);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Войти";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserComboBox
            // 
            this.UserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserComboBox.FormattingEnabled = true;
            this.UserComboBox.Items.AddRange(new object[] {
            "Клиент",
            "Продавец",
            "Админ"});
            this.UserComboBox.Location = new System.Drawing.Point(94, 165);
            this.UserComboBox.Name = "UserComboBox";
            this.UserComboBox.Size = new System.Drawing.Size(121, 21);
            this.UserComboBox.TabIndex = 5;
            // 
            // UserNotFound
            // 
            this.UserNotFound.AutoSize = true;
            this.UserNotFound.Enabled = false;
            this.UserNotFound.ForeColor = System.Drawing.Color.Red;
            this.UserNotFound.Location = new System.Drawing.Point(91, 218);
            this.UserNotFound.Name = "UserNotFound";
            this.UserNotFound.Size = new System.Drawing.Size(134, 13);
            this.UserNotFound.TabIndex = 6;
            this.UserNotFound.Text = "Пользователь не найден";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 309);
            this.Controls.Add(this.UserNotFound);
            this.Controls.Add(this.UserComboBox);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Lastname);
            this.Controls.Add(this.Firstname);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Firstname;
        private System.Windows.Forms.Label Lastname;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.ComboBox UserComboBox;
        private System.Windows.Forms.Label UserNotFound;
    }
}