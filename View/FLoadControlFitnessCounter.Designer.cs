namespace View
{
    partial class FLoadControlFitnessCounter
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
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListBoxSimulations = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(410, 375);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(75, 23);
            this.ButtonLoad.TabIndex = 5;
            this.ButtonLoad.Text = "Load";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Saved Simulations:";
            // 
            // ListBoxSimulations
            // 
            this.ListBoxSimulations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ListBoxSimulations.FormattingEnabled = true;
            this.ListBoxSimulations.ItemHeight = 16;
            this.ListBoxSimulations.Location = new System.Drawing.Point(12, 29);
            this.ListBoxSimulations.Name = "ListBoxSimulations";
            this.ListBoxSimulations.Size = new System.Drawing.Size(473, 340);
            this.ListBoxSimulations.TabIndex = 3;
            // 
            // FLoadControlFitnessCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 402);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBoxSimulations);
            this.Name = "FLoadControlFitnessCounter";
            this.Text = "Load Control Fitness Counter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FLoadControlFitnessCounter_FormClosed);
            this.Load += new System.EventHandler(this.FLoadControlFitnessCounter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ListBoxSimulations;
    }
}