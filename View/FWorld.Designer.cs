namespace View
{
    partial class FWorld
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonResume = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.checkBoxCentreOfGravity = new System.Windows.Forms.CheckBox();
            this.checkBoxSheepSight = new System.Windows.Forms.CheckBox();
            this.checkBoxShepardSight = new System.Windows.Forms.CheckBox();
            this.labelFitness = new System.Windows.Forms.Label();
            this.buttonStepForward = new System.Windows.Forms.Button();
            this.buttonStepBack = new System.Windows.Forms.Button();
            this.checkBoxShepardsPath = new System.Windows.Forms.CheckBox();
            this.checkBoxSheepPath = new System.Windows.Forms.CheckBox();
            this.buttonSavePositions = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelEra = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(682, 151);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonResume
            // 
            this.buttonResume.Enabled = false;
            this.buttonResume.Location = new System.Drawing.Point(844, 151);
            this.buttonResume.Name = "buttonResume";
            this.buttonResume.Size = new System.Drawing.Size(75, 23);
            this.buttonResume.TabIndex = 2;
            this.buttonResume.Text = "Resume";
            this.buttonResume.UseVisualStyleBackColor = true;
            this.buttonResume.Click += new System.EventHandler(this.buttonResume_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Enabled = false;
            this.buttonPause.Location = new System.Drawing.Point(763, 151);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 3;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // checkBoxCentreOfGravity
            // 
            this.checkBoxCentreOfGravity.AutoSize = true;
            this.checkBoxCentreOfGravity.Location = new System.Drawing.Point(682, 12);
            this.checkBoxCentreOfGravity.Name = "checkBoxCentreOfGravity";
            this.checkBoxCentreOfGravity.Size = new System.Drawing.Size(103, 17);
            this.checkBoxCentreOfGravity.TabIndex = 5;
            this.checkBoxCentreOfGravity.Text = "Centre of Sheep";
            this.checkBoxCentreOfGravity.UseVisualStyleBackColor = true;
            // 
            // checkBoxSheepSight
            // 
            this.checkBoxSheepSight.AutoSize = true;
            this.checkBoxSheepSight.Location = new System.Drawing.Point(682, 35);
            this.checkBoxSheepSight.Name = "checkBoxSheepSight";
            this.checkBoxSheepSight.Size = new System.Drawing.Size(112, 17);
            this.checkBoxSheepSight.TabIndex = 6;
            this.checkBoxSheepSight.Text = "Sheep sight range";
            this.checkBoxSheepSight.UseVisualStyleBackColor = true;
            // 
            // checkBoxShepardSight
            // 
            this.checkBoxShepardSight.AutoSize = true;
            this.checkBoxShepardSight.Location = new System.Drawing.Point(682, 58);
            this.checkBoxShepardSight.Name = "checkBoxShepardSight";
            this.checkBoxShepardSight.Size = new System.Drawing.Size(96, 17);
            this.checkBoxShepardSight.TabIndex = 7;
            this.checkBoxShepardSight.Text = "Shepards sight";
            this.checkBoxShepardSight.UseVisualStyleBackColor = true;
            // 
            // labelFitness
            // 
            this.labelFitness.AutoSize = true;
            this.labelFitness.Location = new System.Drawing.Point(679, 287);
            this.labelFitness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelFitness.Name = "labelFitness";
            this.labelFitness.Size = new System.Drawing.Size(43, 13);
            this.labelFitness.TabIndex = 8;
            this.labelFitness.Text = "Fitness:";
            // 
            // buttonStepForward
            // 
            this.buttonStepForward.Enabled = false;
            this.buttonStepForward.Location = new System.Drawing.Point(682, 180);
            this.buttonStepForward.Name = "buttonStepForward";
            this.buttonStepForward.Size = new System.Drawing.Size(115, 23);
            this.buttonStepForward.TabIndex = 9;
            this.buttonStepForward.Text = "Step Forward";
            this.buttonStepForward.UseVisualStyleBackColor = true;
            this.buttonStepForward.Click += new System.EventHandler(this.buttonStepForward_Click);
            // 
            // buttonStepBack
            // 
            this.buttonStepBack.Enabled = false;
            this.buttonStepBack.Location = new System.Drawing.Point(804, 180);
            this.buttonStepBack.Name = "buttonStepBack";
            this.buttonStepBack.Size = new System.Drawing.Size(115, 23);
            this.buttonStepBack.TabIndex = 10;
            this.buttonStepBack.Text = "Step Back";
            this.buttonStepBack.UseVisualStyleBackColor = true;
            this.buttonStepBack.Click += new System.EventHandler(this.buttonStepBack_Click);
            // 
            // checkBoxShepardsPath
            // 
            this.checkBoxShepardsPath.AutoSize = true;
            this.checkBoxShepardsPath.Location = new System.Drawing.Point(682, 104);
            this.checkBoxShepardsPath.Name = "checkBoxShepardsPath";
            this.checkBoxShepardsPath.Size = new System.Drawing.Size(95, 17);
            this.checkBoxShepardsPath.TabIndex = 11;
            this.checkBoxShepardsPath.Text = "Shepards path";
            this.checkBoxShepardsPath.UseVisualStyleBackColor = true;
            // 
            // checkBoxSheepPath
            // 
            this.checkBoxSheepPath.AutoSize = true;
            this.checkBoxSheepPath.Location = new System.Drawing.Point(682, 81);
            this.checkBoxSheepPath.Name = "checkBoxSheepPath";
            this.checkBoxSheepPath.Size = new System.Drawing.Size(81, 17);
            this.checkBoxSheepPath.TabIndex = 12;
            this.checkBoxSheepPath.Text = "Sheep path";
            this.checkBoxSheepPath.UseVisualStyleBackColor = true;
            // 
            // buttonSavePositions
            // 
            this.buttonSavePositions.Location = new System.Drawing.Point(682, 224);
            this.buttonSavePositions.Name = "buttonSavePositions";
            this.buttonSavePositions.Size = new System.Drawing.Size(115, 23);
            this.buttonSavePositions.TabIndex = 13;
            this.buttonSavePositions.Text = "Save Positions";
            this.buttonSavePositions.UseVisualStyleBackColor = true;
            this.buttonSavePositions.Click += new System.EventHandler(this.buttonSavePositions_Click);
            // 
            // labelEra
            // 
            this.labelEra.AutoSize = true;
            this.labelEra.Location = new System.Drawing.Point(679, 327);
            this.labelEra.Name = "labelEra";
            this.labelEra.Size = new System.Drawing.Size(29, 13);
            this.labelEra.TabIndex = 14;
            this.labelEra.Text = "Era: ";
            // 
            // FWorld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 612);
            this.Controls.Add(this.labelEra);
            this.Controls.Add(this.buttonSavePositions);
            this.Controls.Add(this.checkBoxSheepPath);
            this.Controls.Add(this.checkBoxShepardsPath);
            this.Controls.Add(this.buttonStepBack);
            this.Controls.Add(this.buttonStepForward);
            this.Controls.Add(this.labelFitness);
            this.Controls.Add(this.checkBoxShepardSight);
            this.Controls.Add(this.checkBoxSheepSight);
            this.Controls.Add(this.checkBoxCentreOfGravity);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonResume);
            this.Controls.Add(this.buttonStart);
            this.DoubleBuffered = true;
            this.Name = "FWorld";
            this.Text = "Simulation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FWorld_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FWorld_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonResume;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.CheckBox checkBoxCentreOfGravity;
        private System.Windows.Forms.CheckBox checkBoxSheepSight;
        private System.Windows.Forms.CheckBox checkBoxShepardSight;
        private System.Windows.Forms.Label labelFitness;
        private System.Windows.Forms.Button buttonStepForward;
        private System.Windows.Forms.Button buttonStepBack;
        private System.Windows.Forms.CheckBox checkBoxShepardsPath;
        private System.Windows.Forms.CheckBox checkBoxSheepPath;
        private System.Windows.Forms.Button buttonSavePositions;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelEra;
    }
}