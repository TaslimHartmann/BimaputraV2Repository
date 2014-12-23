namespace ListViewToExcel
{
	partial class Form1
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
			this.myList = new System.Windows.Forms.ListView();
			this.btnExcel = new System.Windows.Forms.Button();
			this.btnCsv = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnExport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// myList
			// 
			this.myList.Location = new System.Drawing.Point(29, 37);
			this.myList.Name = "myList";
			this.myList.Size = new System.Drawing.Size(637, 232);
			this.myList.TabIndex = 0;
			this.myList.UseCompatibleStateImageBehavior = false;
			this.myList.View = System.Windows.Forms.View.Details;
			// 
			// btnExcel
			// 
			this.btnExcel.Location = new System.Drawing.Point(29, 8);
			this.btnExcel.Name = "btnExcel";
			this.btnExcel.Size = new System.Drawing.Size(75, 23);
			this.btnExcel.TabIndex = 1;
			this.btnExcel.Text = "Excel";
			this.btnExcel.UseVisualStyleBackColor = true;
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			// 
			// btnCsv
			// 
			this.btnCsv.Location = new System.Drawing.Point(122, 8);
			this.btnCsv.Name = "btnCsv";
			this.btnCsv.Size = new System.Drawing.Size(75, 23);
			this.btnCsv.TabIndex = 2;
			this.btnCsv.Text = "CSV";
			this.btnCsv.UseVisualStyleBackColor = true;
			this.btnCsv.Click += new System.EventHandler(this.btnCsv_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(216, 8);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 3;
			this.btnExport.Text = "Smart Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(691, 290);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnCsv);
			this.Controls.Add(this.btnExcel);
			this.Controls.Add(this.myList);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

	
		private System.Windows.Forms.ListView myList;
		private System.Windows.Forms.Button btnExcel;
		private System.Windows.Forms.Button btnCsv;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button btnExport;




	}
}

