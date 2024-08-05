
namespace WinFormUI
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
            this.Subtotal = new System.Windows.Forms.Label();
            this.SubTotalTextBox = new System.Windows.Forms.TextBox();
            this.Total = new System.Windows.Forms.Label();
            this.TotalTextBox = new System.Windows.Forms.TextBox();
            this.MessageBoxDemo = new System.Windows.Forms.Button();
            this.TextBoxDemo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Subtotal
            // 
            this.Subtotal.AutoSize = true;
            this.Subtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Subtotal.Location = new System.Drawing.Point(178, 24);
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.Size = new System.Drawing.Size(77, 24);
            this.Subtotal.TabIndex = 0;
            this.Subtotal.Text = "Subtotal";
            // 
            // SubTotalTextBox
            // 
            this.SubTotalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubTotalTextBox.Location = new System.Drawing.Point(182, 51);
            this.SubTotalTextBox.Name = "SubTotalTextBox";
            this.SubTotalTextBox.Size = new System.Drawing.Size(160, 29);
            this.SubTotalTextBox.TabIndex = 1;
            // 
            // Total
            // 
            this.Total.AutoSize = true;
            this.Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total.Location = new System.Drawing.Point(178, 83);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(51, 24);
            this.Total.TabIndex = 2;
            this.Total.Text = "Total";
            // 
            // TotalTextBox
            // 
            this.TotalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalTextBox.Location = new System.Drawing.Point(182, 110);
            this.TotalTextBox.Name = "TotalTextBox";
            this.TotalTextBox.Size = new System.Drawing.Size(160, 29);
            this.TotalTextBox.TabIndex = 3;
            // 
            // MessageBoxDemo
            // 
            this.MessageBoxDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageBoxDemo.Location = new System.Drawing.Point(16, 148);
            this.MessageBoxDemo.Name = "MessageBoxDemo";
            this.MessageBoxDemo.Size = new System.Drawing.Size(160, 74);
            this.MessageBoxDemo.TabIndex = 4;
            this.MessageBoxDemo.Text = "MessageBox Demo";
            this.MessageBoxDemo.UseVisualStyleBackColor = true;
            this.MessageBoxDemo.Click += new System.EventHandler(this.MessageBoxDemo_Click);
            // 
            // TextBoxDemo
            // 
            this.TextBoxDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDemo.Location = new System.Drawing.Point(182, 148);
            this.TextBoxDemo.Name = "TextBoxDemo";
            this.TextBoxDemo.Size = new System.Drawing.Size(160, 74);
            this.TextBoxDemo.TabIndex = 5;
            this.TextBoxDemo.Text = "TextBox Demo";
            this.TextBoxDemo.UseVisualStyleBackColor = true;
            this.TextBoxDemo.Click += new System.EventHandler(this.TextBoxDemo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 251);
            this.Controls.Add(this.TextBoxDemo);
            this.Controls.Add(this.MessageBoxDemo);
            this.Controls.Add(this.TotalTextBox);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.SubTotalTextBox);
            this.Controls.Add(this.Subtotal);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Subtotal;
        private System.Windows.Forms.TextBox SubTotalTextBox;
        private System.Windows.Forms.Label Total;
        private System.Windows.Forms.TextBox TotalTextBox;
        private System.Windows.Forms.Button MessageBoxDemo;
        private System.Windows.Forms.Button TextBoxDemo;
    }
}

