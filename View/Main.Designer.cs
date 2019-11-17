namespace View
{
    partial class Main
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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonResume = new System.Windows.Forms.Button();
            this.ButtonPause = new System.Windows.Forms.Button();
            this.CheckBoxCenterOfGravity = new System.Windows.Forms.CheckBox();
            this.CheckBoxSheepSight = new System.Windows.Forms.CheckBox();
            this.CheckBoxShepherdSight = new System.Windows.Forms.CheckBox();
            this.labelFitness = new System.Windows.Forms.Label();
            this.ButtonStepForward = new System.Windows.Forms.Button();
            this.ButtonStepBack = new System.Windows.Forms.Button();
            this.CheckBoxShepherdsPath = new System.Windows.Forms.CheckBox();
            this.CheckBoxSheepPath = new System.Windows.Forms.CheckBox();
            this.ButtonSavePositions = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelEra = new System.Windows.Forms.Label();
            this.labelOptimizationParameters = new System.Windows.Forms.Label();
            this.labelShepherds = new System.Windows.Forms.Label();
            this.buttonLoadOptimizationParameters = new System.Windows.Forms.Button();
            this.buttonLoadShepherds = new System.Windows.Forms.Button();
            this.openFileDialogOptimizationParameters = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogShepherds = new System.Windows.Forms.OpenFileDialog();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.buttonRandomizePositions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(618, 255);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonResume
            // 
            this.ButtonResume.Enabled = false;
            this.ButtonResume.Location = new System.Drawing.Point(779, 255);
            this.ButtonResume.Name = "ButtonResume";
            this.ButtonResume.Size = new System.Drawing.Size(75, 23);
            this.ButtonResume.TabIndex = 2;
            this.ButtonResume.Text = "Resume";
            this.ButtonResume.UseVisualStyleBackColor = true;
            this.ButtonResume.Click += new System.EventHandler(this.ButtonResume_Click);
            // 
            // ButtonPause
            // 
            this.ButtonPause.Enabled = false;
            this.ButtonPause.Location = new System.Drawing.Point(698, 255);
            this.ButtonPause.Name = "ButtonPause";
            this.ButtonPause.Size = new System.Drawing.Size(75, 23);
            this.ButtonPause.TabIndex = 3;
            this.ButtonPause.Text = "Pause";
            this.ButtonPause.UseVisualStyleBackColor = true;
            this.ButtonPause.Click += new System.EventHandler(this.ButtonPause_Click);
            // 
            // CheckBoxCenterOfGravity
            // 
            this.CheckBoxCenterOfGravity.AutoSize = true;
            this.CheckBoxCenterOfGravity.Location = new System.Drawing.Point(618, 140);
            this.CheckBoxCenterOfGravity.Name = "CheckBoxCenterOfGravity";
            this.CheckBoxCenterOfGravity.Size = new System.Drawing.Size(103, 17);
            this.CheckBoxCenterOfGravity.TabIndex = 5;
            this.CheckBoxCenterOfGravity.Text = "Center of Sheep";
            this.CheckBoxCenterOfGravity.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSheepSight
            // 
            this.CheckBoxSheepSight.AutoSize = true;
            this.CheckBoxSheepSight.Location = new System.Drawing.Point(618, 163);
            this.CheckBoxSheepSight.Name = "CheckBoxSheepSight";
            this.CheckBoxSheepSight.Size = new System.Drawing.Size(112, 17);
            this.CheckBoxSheepSight.TabIndex = 6;
            this.CheckBoxSheepSight.Text = "Sheep sight range";
            this.CheckBoxSheepSight.UseVisualStyleBackColor = true;
            // 
            // CheckBoxShepherdSight
            // 
            this.CheckBoxShepherdSight.AutoSize = true;
            this.CheckBoxShepherdSight.Location = new System.Drawing.Point(618, 186);
            this.CheckBoxShepherdSight.Name = "CheckBoxShepherdSight";
            this.CheckBoxShepherdSight.Size = new System.Drawing.Size(102, 17);
            this.CheckBoxShepherdSight.TabIndex = 7;
            this.CheckBoxShepherdSight.Text = "Shepherds sight";
            this.CheckBoxShepherdSight.UseVisualStyleBackColor = true;
            // 
            // labelFitness
            // 
            this.labelFitness.AutoSize = true;
            this.labelFitness.Location = new System.Drawing.Point(618, 409);
            this.labelFitness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelFitness.Name = "labelFitness";
            this.labelFitness.Size = new System.Drawing.Size(82, 13);
            this.labelFitness.TabIndex = 8;
            this.labelFitness.Text = "Total distances:";
            // 
            // ButtonStepForward
            // 
            this.ButtonStepForward.Enabled = false;
            this.ButtonStepForward.Location = new System.Drawing.Point(617, 284);
            this.ButtonStepForward.Name = "ButtonStepForward";
            this.ButtonStepForward.Size = new System.Drawing.Size(76, 23);
            this.ButtonStepForward.TabIndex = 9;
            this.ButtonStepForward.Text = "Step Forward";
            this.ButtonStepForward.UseVisualStyleBackColor = true;
            this.ButtonStepForward.Click += new System.EventHandler(this.ButtonStepForward_Click);
            // 
            // ButtonStepBack
            // 
            this.ButtonStepBack.Enabled = false;
            this.ButtonStepBack.Location = new System.Drawing.Point(699, 284);
            this.ButtonStepBack.Name = "ButtonStepBack";
            this.ButtonStepBack.Size = new System.Drawing.Size(74, 23);
            this.ButtonStepBack.TabIndex = 10;
            this.ButtonStepBack.Text = "Step Back";
            this.ButtonStepBack.UseVisualStyleBackColor = true;
            this.ButtonStepBack.Click += new System.EventHandler(this.ButtonStepBack_Click);
            // 
            // CheckBoxShepherdsPath
            // 
            this.CheckBoxShepherdsPath.AutoSize = true;
            this.CheckBoxShepherdsPath.Location = new System.Drawing.Point(618, 232);
            this.CheckBoxShepherdsPath.Name = "CheckBoxShepherdsPath";
            this.CheckBoxShepherdsPath.Size = new System.Drawing.Size(101, 17);
            this.CheckBoxShepherdsPath.TabIndex = 11;
            this.CheckBoxShepherdsPath.Text = "Shepherds path";
            this.CheckBoxShepherdsPath.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSheepPath
            // 
            this.CheckBoxSheepPath.AutoSize = true;
            this.CheckBoxSheepPath.Location = new System.Drawing.Point(618, 209);
            this.CheckBoxSheepPath.Name = "CheckBoxSheepPath";
            this.CheckBoxSheepPath.Size = new System.Drawing.Size(81, 17);
            this.CheckBoxSheepPath.TabIndex = 12;
            this.CheckBoxSheepPath.Text = "Sheep path";
            this.CheckBoxSheepPath.UseVisualStyleBackColor = true;
            // 
            // ButtonSavePositions
            // 
            this.ButtonSavePositions.Location = new System.Drawing.Point(618, 381);
            this.ButtonSavePositions.Name = "ButtonSavePositions";
            this.ButtonSavePositions.Size = new System.Drawing.Size(115, 23);
            this.ButtonSavePositions.TabIndex = 13;
            this.ButtonSavePositions.Text = "Save Positions";
            this.ButtonSavePositions.UseVisualStyleBackColor = true;
            this.ButtonSavePositions.Click += new System.EventHandler(this.ButtonSavePositions_Click);
            // 
            // labelEra
            // 
            this.labelEra.AutoSize = true;
            this.labelEra.Location = new System.Drawing.Point(617, 473);
            this.labelEra.Name = "labelEra";
            this.labelEra.Size = new System.Drawing.Size(29, 13);
            this.labelEra.TabIndex = 14;
            this.labelEra.Text = "Era: ";
            // 
            // labelOptimizationParameters
            // 
            this.labelOptimizationParameters.AutoSize = true;
            this.labelOptimizationParameters.Location = new System.Drawing.Point(615, 11);
            this.labelOptimizationParameters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelOptimizationParameters.Name = "labelOptimizationParameters";
            this.labelOptimizationParameters.Size = new System.Drawing.Size(123, 13);
            this.labelOptimizationParameters.TabIndex = 17;
            this.labelOptimizationParameters.Text = "Optimization Parameters:";
            // 
            // labelShepherds
            // 
            this.labelShepherds.AutoSize = true;
            this.labelShepherds.Location = new System.Drawing.Point(615, 57);
            this.labelShepherds.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelShepherds.Name = "labelShepherds";
            this.labelShepherds.Size = new System.Drawing.Size(61, 13);
            this.labelShepherds.TabIndex = 18;
            this.labelShepherds.Text = "Shepherds:";
            // 
            // buttonLoadOptimizationParameters
            // 
            this.buttonLoadOptimizationParameters.Location = new System.Drawing.Point(617, 29);
            this.buttonLoadOptimizationParameters.Name = "buttonLoadOptimizationParameters";
            this.buttonLoadOptimizationParameters.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadOptimizationParameters.TabIndex = 19;
            this.buttonLoadOptimizationParameters.Text = "Load";
            this.buttonLoadOptimizationParameters.UseVisualStyleBackColor = true;
            this.buttonLoadOptimizationParameters.Click += new System.EventHandler(this.buttonLoadOptimizationParameters_Click);
            // 
            // buttonLoadShepherds
            // 
            this.buttonLoadShepherds.Location = new System.Drawing.Point(617, 75);
            this.buttonLoadShepherds.Name = "buttonLoadShepherds";
            this.buttonLoadShepherds.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadShepherds.TabIndex = 20;
            this.buttonLoadShepherds.Text = "Load";
            this.buttonLoadShepherds.UseVisualStyleBackColor = true;
            this.buttonLoadShepherds.Click += new System.EventHandler(this.buttonLoadShepherds_Click);
            // 
            // openFileDialogOptimizationParameters
            // 
            this.openFileDialogOptimizationParameters.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogOptimizationParameters_FileOk);
            // 
            // openFileDialogShepherds
            // 
            this.openFileDialogShepherds.FileName = "openFileDialog1";
            this.openFileDialogShepherds.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogShepherds_FileOk);
            // 
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(779, 284);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(75, 23);
            this.buttonRandomize.TabIndex = 21;
            this.buttonRandomize.Text = "Randomize";
            this.buttonRandomize.UseVisualStyleBackColor = true;
            this.buttonRandomize.Click += new System.EventHandler(this.ButtonRandomizePositions_Click);
            // 
            // buttonRandomizePositions
            // 
            this.buttonRandomizePositions.Location = new System.Drawing.Point(618, 334);
            this.buttonRandomizePositions.Name = "buttonRandomizePositions";
            this.buttonRandomizePositions.Size = new System.Drawing.Size(115, 23);
            this.buttonRandomizePositions.TabIndex = 22;
            this.buttonRandomizePositions.Text = "Randomize Positions";
            this.buttonRandomizePositions.UseVisualStyleBackColor = true;
            this.buttonRandomizePositions.Click += new System.EventHandler(this.ButtonRandomizePositions_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 637);
            this.Controls.Add(this.buttonRandomizePositions);
            this.Controls.Add(this.buttonRandomize);
            this.Controls.Add(this.buttonLoadShepherds);
            this.Controls.Add(this.buttonLoadOptimizationParameters);
            this.Controls.Add(this.labelShepherds);
            this.Controls.Add(this.labelOptimizationParameters);
            this.Controls.Add(this.labelEra);
            this.Controls.Add(this.ButtonSavePositions);
            this.Controls.Add(this.CheckBoxSheepPath);
            this.Controls.Add(this.CheckBoxShepherdsPath);
            this.Controls.Add(this.ButtonStepBack);
            this.Controls.Add(this.ButtonStepForward);
            this.Controls.Add(this.labelFitness);
            this.Controls.Add(this.CheckBoxShepherdSight);
            this.Controls.Add(this.CheckBoxSheepSight);
            this.Controls.Add(this.CheckBoxCenterOfGravity);
            this.Controls.Add(this.ButtonPause);
            this.Controls.Add(this.ButtonResume);
            this.Controls.Add(this.ButtonStart);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.Text = "Herding Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.World_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FWorld_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonResume;
        private System.Windows.Forms.Button ButtonPause;
        private System.Windows.Forms.CheckBox CheckBoxCenterOfGravity;
        private System.Windows.Forms.CheckBox CheckBoxSheepSight;
        private System.Windows.Forms.CheckBox CheckBoxShepherdSight;
        private System.Windows.Forms.Label labelFitness;
        private System.Windows.Forms.Button ButtonStepForward;
        private System.Windows.Forms.Button ButtonStepBack;
        private System.Windows.Forms.CheckBox CheckBoxShepherdsPath;
        private System.Windows.Forms.CheckBox CheckBoxSheepPath;
        private System.Windows.Forms.Button ButtonSavePositions;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Label labelEra;
        private System.Windows.Forms.Label labelOptimizationParameters;
        private System.Windows.Forms.Label labelShepherds;
        private System.Windows.Forms.Button buttonLoadOptimizationParameters;
        private System.Windows.Forms.Button buttonLoadShepherds;
        private System.Windows.Forms.OpenFileDialog openFileDialogOptimizationParameters;
        private System.Windows.Forms.OpenFileDialog openFileDialogShepherds;
        private System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.Button buttonRandomizePositions;
    }
}