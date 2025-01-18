namespace Launcher
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            listBox1 = new ListBox();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            progressBar1 = new ProgressBar();
            comboBox3 = new ComboBox();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.ScrollAlwaysVisible = true;
            listBox1.Size = new Size(208, 616);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(226, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(770, 616);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(879, 634);
            button1.Name = "button1";
            button1.Size = new Size(117, 25);
            button1.TabIndex = 2;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(879, 667);
            button2.Name = "button2";
            button2.Size = new Size(117, 50);
            button2.TabIndex = 3;
            button2.Text = "Launch";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 698);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(502, 19);
            progressBar1.TabIndex = 4;
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(656, 667);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(217, 25);
            comboBox3.TabIndex = 6;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "English", "中文" });
            comboBox1.Location = new Point(656, 634);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(80, 25);
            comboBox1.TabIndex = 7;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "1024x768", "1280x720", "1366x768", "1440x900", "1600x900", "1920x1080" });
            comboBox2.Location = new Point(742, 634);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(131, 25);
            comboBox2.TabIndex = 8;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Location = new Point(12, 636);
            label1.Name = "label1";
            label1.Size = new Size(502, 23);
            label1.TabIndex = 9;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(520, 667);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.PlaceholderText = "PassWord";
            textBox2.Size = new Size(130, 23);
            textBox2.TabIndex = 10;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(520, 635);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "UserName";
            textBox1.Size = new Size(130, 23);
            textBox1.TabIndex = 11;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.Location = new Point(520, 693);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(115, 24);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "FullScreen";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.Location = new Point(639, 695);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(115, 21);
            checkBox2.TabIndex = 13;
            checkBox2.Text = "FPS Cap";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.Location = new Point(758, 695);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(115, 21);
            checkBox3.TabIndex = 14;
            checkBox3.Text = "WindowOnTop";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // label2
            // 
            label2.Location = new Point(12, 667);
            label2.Name = "label2";
            label2.Size = new Size(502, 23);
            label2.TabIndex = 15;
            label2.Text = "label2";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1008, 729);
            Controls.Add(label2);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(comboBox3);
            Controls.Add(progressBar1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(listBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Launcher";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
        private ProgressBar progressBar1;
        private ComboBox comboBox3;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Label label2;
    }
}
