namespace BlackWin
{
	partial class Form1
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			checkedListBox1 = new CheckedListBox();
			panel1 = new Panel();
			button1 = new Button();
			timer1 = new System.Windows.Forms.Timer(components);
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// checkedListBox1
			// 
			checkedListBox1.CheckOnClick = true;
			checkedListBox1.Dock = DockStyle.Top;
			checkedListBox1.Font = new Font("Segoe UI", 16F);
			checkedListBox1.FormattingEnabled = true;
			checkedListBox1.Location = new Point(0, 0);
			checkedListBox1.Name = "checkedListBox1";
			checkedListBox1.Size = new Size(668, 128);
			checkedListBox1.TabIndex = 0;
			checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
			// 
			// panel1
			// 
			panel1.AutoSize = true;
			panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panel1.Controls.Add(checkedListBox1);
			panel1.Controls.Add(button1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(800, 128);
			panel1.TabIndex = 1;
			panel1.Visible = false;
			// 
			// button1
			// 
			button1.BackColor = SystemColors.ActiveCaption;
			button1.Dock = DockStyle.Right;
			button1.Font = new Font("Segoe UI", 16F);
			button1.Location = new Point(668, 0);
			button1.Name = "button1";
			button1.Size = new Size(132, 128);
			button1.TabIndex = 2;
			button1.Text = "ЗАКРЫТЬ";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// timer1
			// 
			timer1.Tick += timer1_Tick;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			ClientSize = new Size(800, 450);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "Form1";
			StartPosition = FormStartPosition.Manual;
			Text = "Form1";
			TopMost = true;
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckedListBox checkedListBox1;
		private Panel panel1;
		private Button button1;
		private System.Windows.Forms.Timer timer1;
	}
}
