﻿namespace Sudoku.Forms
{
	partial class MainForm
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
			this._pictureBoxGrid = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this._pictureBoxGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// _pictureBoxGrid
			// 
			this._pictureBoxGrid.Location = new System.Drawing.Point(398, 110);
			this._pictureBoxGrid.Margin = new System.Windows.Forms.Padding(0);
			this._pictureBoxGrid.Name = "_pictureBoxGrid";
			this._pictureBoxGrid.Size = new System.Drawing.Size(540, 540);
			this._pictureBoxGrid.TabIndex = 1;
			this._pictureBoxGrid.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1228, 775);
			this.Controls.Add(this._pictureBoxGrid);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainForm";
			this.Text = "[MainForm]";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this._pictureBoxGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox _pictureBoxGrid;
	}
}

