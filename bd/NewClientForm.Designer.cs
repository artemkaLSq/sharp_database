namespace bd
{
    partial class NewClientForm
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
            this.TariffGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gigabytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minutes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TariffComboBox = new System.Windows.Forms.ComboBox();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TariffGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TariffGridView
            // 
            this.TariffGridView.AllowUserToAddRows = false;
            this.TariffGridView.AllowUserToDeleteRows = false;
            this.TariffGridView.AllowUserToResizeColumns = false;
            this.TariffGridView.AllowUserToResizeRows = false;
            this.TariffGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.TariffGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TariffGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.price,
            this.gigabytes,
            this.minutes,
            this.sms});
            this.TariffGridView.GridColor = System.Drawing.Color.White;
            this.TariffGridView.Location = new System.Drawing.Point(12, 109);
            this.TariffGridView.Name = "TariffGridView";
            this.TariffGridView.ReadOnly = true;
            this.TariffGridView.RowHeadersWidth = 20;
            this.TariffGridView.Size = new System.Drawing.Size(520, 175);
            this.TariffGridView.TabIndex = 13;
            // 
            // name
            // 
            this.name.Frozen = true;
            this.name.HeaderText = "Название";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // price
            // 
            this.price.Frozen = true;
            this.price.HeaderText = "Цена, руб.";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // gigabytes
            // 
            this.gigabytes.Frozen = true;
            this.gigabytes.HeaderText = "Гигабайты";
            this.gigabytes.Name = "gigabytes";
            this.gigabytes.ReadOnly = true;
            // 
            // minutes
            // 
            this.minutes.Frozen = true;
            this.minutes.HeaderText = "Минуты";
            this.minutes.Name = "minutes";
            this.minutes.ReadOnly = true;
            // 
            // sms
            // 
            this.sms.Frozen = true;
            this.sms.HeaderText = "SMS";
            this.sms.Name = "sms";
            this.sms.ReadOnly = true;
            // 
            // TariffComboBox
            // 
            this.TariffComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TariffComboBox.FormattingEnabled = true;
            this.TariffComboBox.Location = new System.Drawing.Point(309, 300);
            this.TariffComboBox.Name = "TariffComboBox";
            this.TariffComboBox.Size = new System.Drawing.Size(121, 21);
            this.TariffComboBox.TabIndex = 15;
            this.TariffComboBox.TabStop = false;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(330, 336);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(100, 23);
            this.ChangeButton.TabIndex = 14;
            this.ChangeButton.Text = "Сделать запрос";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "label7";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 300);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.TabStop = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(163, 300);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 339);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Отчество";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(163, 365);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Паспорт";
            // 
            // NewClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TariffComboBox);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.TariffGridView);
            this.Name = "NewClientForm";
            this.Text = "NewClientForm";
            ((System.ComponentModel.ISupportInitialize)(this.TariffGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TariffGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn gigabytes;
        private System.Windows.Forms.DataGridViewTextBoxColumn minutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn sms;
        private System.Windows.Forms.ComboBox TariffComboBox;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}