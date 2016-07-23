namespace ImageProcessing
{
    partial class Mask
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
            this.tb0 = new System.Windows.Forms.TextBox();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.tb4 = new System.Windows.Forms.TextBox();
            this.tb5 = new System.Windows.Forms.TextBox();
            this.tb6 = new System.Windows.Forms.TextBox();
            this.tb7 = new System.Windows.Forms.TextBox();
            this.tb8 = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb0
            // 
            this.tb0.Location = new System.Drawing.Point(12, 26);
            this.tb0.Name = "tb0";
            this.tb0.Size = new System.Drawing.Size(35, 20);
            this.tb0.TabIndex = 0;
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(121, 26);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(35, 20);
            this.tb1.TabIndex = 0;
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(237, 26);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(35, 20);
            this.tb2.TabIndex = 0;
            // 
            // tb3
            // 
            this.tb3.Location = new System.Drawing.Point(12, 83);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(35, 20);
            this.tb3.TabIndex = 0;
            // 
            // tb4
            // 
            this.tb4.Location = new System.Drawing.Point(121, 83);
            this.tb4.Name = "tb4";
            this.tb4.Size = new System.Drawing.Size(35, 20);
            this.tb4.TabIndex = 0;
            // 
            // tb5
            // 
            this.tb5.Location = new System.Drawing.Point(237, 83);
            this.tb5.Name = "tb5";
            this.tb5.Size = new System.Drawing.Size(35, 20);
            this.tb5.TabIndex = 0;
            // 
            // tb6
            // 
            this.tb6.Location = new System.Drawing.Point(12, 142);
            this.tb6.Name = "tb6";
            this.tb6.Size = new System.Drawing.Size(35, 20);
            this.tb6.TabIndex = 0;
            // 
            // tb7
            // 
            this.tb7.Location = new System.Drawing.Point(121, 142);
            this.tb7.Name = "tb7";
            this.tb7.Size = new System.Drawing.Size(35, 20);
            this.tb7.TabIndex = 0;
            // 
            // tb8
            // 
            this.tb8.Location = new System.Drawing.Point(237, 142);
            this.tb8.Name = "tb8";
            this.tb8.Size = new System.Drawing.Size(35, 20);
            this.tb8.TabIndex = 0;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(101, 196);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 1;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // Mask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.tb8);
            this.Controls.Add(this.tb5);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.tb7);
            this.Controls.Add(this.tb6);
            this.Controls.Add(this.tb4);
            this.Controls.Add(this.tb3);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.tb0);
            this.Name = "Mask";
            this.Text = "Mask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb0;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.TextBox tb4;
        private System.Windows.Forms.TextBox tb5;
        private System.Windows.Forms.TextBox tb6;
        private System.Windows.Forms.TextBox tb7;
        private System.Windows.Forms.TextBox tb8;
        private System.Windows.Forms.Button submit;
    }
}