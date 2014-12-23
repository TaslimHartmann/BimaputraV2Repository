using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ListViewToExcel
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
			for (int i = 0; i < 10; i++)
			{
				myList.Columns.Add(i.ToString());	
				ListViewItem lv = new ListViewItem(i.ToString());
				for (int ai = 0; ai < 10; ai++)
				{
					lv.SubItems.Add(ai.ToString());
				}
				myList.Items.Add(lv);
			}

		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			Excel.Application app = new Excel.Application();
			app.Visible = true;
			Excel.Workbook wb = app.Workbooks.Add(1);
			Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
			int i = 1;
			int i2 = 1;
			foreach (ListViewItem lvi in myList.Items)
			{
				i = 1;
				foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
				{
					ws.Cells[i2, i] = lvs.Text;
					i++;
				}
				i2++;
			}
		}

		private void btnCsv_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
			saveFileDialog1.FileName = "logs";
			saveFileDialog1.Title = "Export to Excel";
			StringBuilder sb = new StringBuilder();
			foreach (ColumnHeader ch in myList.Columns)
			{
				sb.Append(ch.Text + ",");
			}
			sb.AppendLine();
			foreach (ListViewItem lvi in myList.Items)
			{
				foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
				{
					if (lvs.Text.Trim() == string.Empty)
						sb.Append(" ,");
					else
						sb.Append(lvs.Text + ",");
				}
				sb.AppendLine();
			}
			DialogResult dr = saveFileDialog1.ShowDialog();
			if (dr == DialogResult.OK)
			{
				StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
				sw.Write(sb.ToString());
				sw.Close();
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			
			saveFileDialog1.Filter = "excel files (*.xls)|*.xls | csv files (*.csv)|*.csv";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if (saveFileDialog1.FilterIndex == 1)
				{
					Excel.Application app = new Excel.Application();
					Excel.Workbook wb = app.Workbooks.Add(1);
					Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
					int i = 1;
					int i2 = 1;
					foreach (ListViewItem lvi in myList.Items)
					{
						i = 1;
						foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
						{
							ws.Cells[i2, i] = lvs.Text;
							i++;
						}
						i2++;
					}
					wb.SaveAs(saveFileDialog1.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
				Type.Missing, Type.Missing);
					wb.Close(false, Type.Missing, Type.Missing);
					app.Quit();
				}
				else if (saveFileDialog1.FilterIndex == 2)
				{
					StringBuilder sb = new StringBuilder();
					foreach (ColumnHeader ch in myList.Columns)
					{
						sb.Append(ch.Text + ",");
					}
					sb.AppendLine();
					foreach (ListViewItem lvi in myList.Items)
					{
						foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
						{
							if (lvs.Text.Trim() == string.Empty)
								sb.Append(" ,");
							else
								sb.Append(lvs.Text + ",");
						}
						sb.AppendLine();
					}
					StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
					sw.Write(sb.ToString());
					sw.Close();
				}
				
			}
		}
	}
}