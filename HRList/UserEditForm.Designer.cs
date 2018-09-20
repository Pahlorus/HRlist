namespace HRList
{
    partial class UserEditForm
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
            this.checkSupervisorBox = new System.Windows.Forms.CheckBox();
            this.writeUserButton = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userPositionCBox = new System.Windows.Forms.ComboBox();
            this.unitCBox = new System.Windows.Forms.ComboBox();
            this.subUnitCBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkSupervisorBox
            // 
            this.checkSupervisorBox.AutoSize = true;
            this.checkSupervisorBox.Location = new System.Drawing.Point(250, 150);
            this.checkSupervisorBox.Name = "checkSupervisorBox";
            this.checkSupervisorBox.Size = new System.Drawing.Size(97, 17);
            this.checkSupervisorBox.TabIndex = 0;
            this.checkSupervisorBox.Text = "Руководитель";
            this.checkSupervisorBox.UseVisualStyleBackColor = true;
            this.checkSupervisorBox.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // writeUserButton
            // 
            this.writeUserButton.Location = new System.Drawing.Point(12, 195);
            this.writeUserButton.Name = "writeUserButton";
            this.writeUserButton.Size = new System.Drawing.Size(75, 23);
            this.writeUserButton.TabIndex = 1;
            this.writeUserButton.Text = "Записать";
            this.writeUserButton.UseVisualStyleBackColor = true;
            this.writeUserButton.Click += new System.EventHandler(this.WriteUserButton_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 50);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(442, 20);
            this.textBoxName.TabIndex = 5;
            this.textBoxName.Text = "ФИО";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Введите данные нового работника";
            // 
            // userPositionCBox
            // 
            this.userPositionCBox.FormattingEnabled = true;
            this.userPositionCBox.Location = new System.Drawing.Point(12, 98);
            this.userPositionCBox.Name = "userPositionCBox";
            this.userPositionCBox.Size = new System.Drawing.Size(179, 21);
            this.userPositionCBox.TabIndex = 10;
            // 
            // unitCBox
            // 
            this.unitCBox.FormattingEnabled = true;
            this.unitCBox.Location = new System.Drawing.Point(12, 146);
            this.unitCBox.Name = "unitCBox";
            this.unitCBox.Size = new System.Drawing.Size(179, 21);
            this.unitCBox.TabIndex = 11;
            // 
            // subUnitCBox
            // 
            this.subUnitCBox.FormattingEnabled = true;
            this.subUnitCBox.Location = new System.Drawing.Point(250, 98);
            this.subUnitCBox.Name = "subUnitCBox";
            this.subUnitCBox.Size = new System.Drawing.Size(179, 21);
            this.subUnitCBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Должность";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Отдел";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Группа";
            // 
            // UserEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 243);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.subUnitCBox);
            this.Controls.Add(this.unitCBox);
            this.Controls.Add(this.userPositionCBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.writeUserButton);
            this.Controls.Add(this.checkSupervisorBox);
            this.Name = "UserEditForm";
            this.Text = "UserCreate";
            this.Shown += new System.EventHandler(this.UserEditForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkSupervisorBox;
        private System.Windows.Forms.Button writeUserButton;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox userPositionCBox;
        private System.Windows.Forms.ComboBox unitCBox;
        private System.Windows.Forms.ComboBox subUnitCBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}