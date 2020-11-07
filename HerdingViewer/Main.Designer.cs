namespace HerdingViewer
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
            this.ButtonSaveHerding = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelStep = new System.Windows.Forms.Label();
            this.checkBoxAnimationMode = new System.Windows.Forms.CheckBox();
            this.richTextBoxParameters = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxShepherds = new System.Windows.Forms.RichTextBox();
            this.checkBoxRandom = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonStart.Location = new System.Drawing.Point(603, 661);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(150, 50);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonResume
            // 
            this.ButtonResume.Enabled = false;
            this.ButtonResume.Location = new System.Drawing.Point(684, 717);
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
            this.ButtonPause.Location = new System.Drawing.Point(603, 717);
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
            this.CheckBoxCenterOfGravity.Location = new System.Drawing.Point(603, 638);
            this.CheckBoxCenterOfGravity.Name = "CheckBoxCenterOfGravity";
            this.CheckBoxCenterOfGravity.Size = new System.Drawing.Size(103, 17);
            this.CheckBoxCenterOfGravity.TabIndex = 5;
            this.CheckBoxCenterOfGravity.Text = "Center of Sheep";
            this.CheckBoxCenterOfGravity.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSheepSight
            // 
            this.CheckBoxSheepSight.AutoSize = true;
            this.CheckBoxSheepSight.Location = new System.Drawing.Point(712, 638);
            this.CheckBoxSheepSight.Name = "CheckBoxSheepSight";
            this.CheckBoxSheepSight.Size = new System.Drawing.Size(112, 17);
            this.CheckBoxSheepSight.TabIndex = 6;
            this.CheckBoxSheepSight.Text = "Sheep sight range";
            this.CheckBoxSheepSight.UseVisualStyleBackColor = true;
            // 
            // CheckBoxShepherdSight
            // 
            this.CheckBoxShepherdSight.AutoSize = true;
            this.CheckBoxShepherdSight.Location = new System.Drawing.Point(830, 638);
            this.CheckBoxShepherdSight.Name = "CheckBoxShepherdSight";
            this.CheckBoxShepherdSight.Size = new System.Drawing.Size(102, 17);
            this.CheckBoxShepherdSight.TabIndex = 7;
            this.CheckBoxShepherdSight.Text = "Shepherds sight";
            this.CheckBoxShepherdSight.UseVisualStyleBackColor = true;
            // 
            // labelFitness
            // 
            this.labelFitness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFitness.Location = new System.Drawing.Point(708, 610);
            this.labelFitness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelFitness.Name = "labelFitness";
            this.labelFitness.Size = new System.Drawing.Size(300, 13);
            this.labelFitness.TabIndex = 8;
            this.labelFitness.Text = "Sum of distances:";
            // 
            // ButtonStepForward
            // 
            this.ButtonStepForward.Enabled = false;
            this.ButtonStepForward.Location = new System.Drawing.Point(765, 717);
            this.ButtonStepForward.Name = "ButtonStepForward";
            this.ButtonStepForward.Size = new System.Drawing.Size(87, 23);
            this.ButtonStepForward.TabIndex = 9;
            this.ButtonStepForward.Text = "Step Forward";
            this.ButtonStepForward.UseVisualStyleBackColor = true;
            this.ButtonStepForward.Click += new System.EventHandler(this.ButtonStepForward_Click);
            // 
            // ButtonStepBack
            // 
            this.ButtonStepBack.Enabled = false;
            this.ButtonStepBack.Location = new System.Drawing.Point(858, 717);
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
            this.CheckBoxShepherdsPath.Location = new System.Drawing.Point(1025, 638);
            this.CheckBoxShepherdsPath.Name = "CheckBoxShepherdsPath";
            this.CheckBoxShepherdsPath.Size = new System.Drawing.Size(101, 17);
            this.CheckBoxShepherdsPath.TabIndex = 11;
            this.CheckBoxShepherdsPath.Text = "Shepherds path";
            this.CheckBoxShepherdsPath.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSheepPath
            // 
            this.CheckBoxSheepPath.AutoSize = true;
            this.CheckBoxSheepPath.Location = new System.Drawing.Point(938, 638);
            this.CheckBoxSheepPath.Name = "CheckBoxSheepPath";
            this.CheckBoxSheepPath.Size = new System.Drawing.Size(81, 17);
            this.CheckBoxSheepPath.TabIndex = 12;
            this.CheckBoxSheepPath.Text = "Sheep path";
            this.CheckBoxSheepPath.UseVisualStyleBackColor = true;
            // 
            // ButtonSaveHerding
            // 
            this.ButtonSaveHerding.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ButtonSaveHerding.Location = new System.Drawing.Point(759, 661);
            this.ButtonSaveHerding.Name = "ButtonSaveHerding";
            this.ButtonSaveHerding.Size = new System.Drawing.Size(150, 50);
            this.ButtonSaveHerding.TabIndex = 13;
            this.ButtonSaveHerding.Text = "Save Herding";
            this.ButtonSaveHerding.UseVisualStyleBackColor = true;
            this.ButtonSaveHerding.Click += new System.EventHandler(this.ButtonSavePositions_Click);
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStep.Location = new System.Drawing.Point(600, 610);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(41, 13);
            this.labelStep.TabIndex = 14;
            this.labelStep.Text = "Step: ";
            // 
            // checkBoxAnimationMode
            // 
            this.checkBoxAnimationMode.AutoSize = true;
            this.checkBoxAnimationMode.Location = new System.Drawing.Point(1132, 638);
            this.checkBoxAnimationMode.Name = "checkBoxAnimationMode";
            this.checkBoxAnimationMode.Size = new System.Drawing.Size(72, 17);
            this.checkBoxAnimationMode.TabIndex = 22;
            this.checkBoxAnimationMode.Text = "Animation";
            this.checkBoxAnimationMode.UseVisualStyleBackColor = true;
            // 
            // richTextBoxParameters
            // 
            this.richTextBoxParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBoxParameters.Location = new System.Drawing.Point(12, 237);
            this.richTextBoxParameters.Name = "richTextBoxParameters";
            this.richTextBoxParameters.Size = new System.Drawing.Size(585, 503);
            this.richTextBoxParameters.TabIndex = 23;
            this.richTextBoxParameters.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Shepherds:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Parameters:";
            // 
            // richTextBoxShepherds
            // 
            this.richTextBoxShepherds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBoxShepherds.Location = new System.Drawing.Point(12, 25);
            this.richTextBoxShepherds.Name = "richTextBoxShepherds";
            this.richTextBoxShepherds.Size = new System.Drawing.Size(585, 193);
            this.richTextBoxShepherds.TabIndex = 26;
            this.richTextBoxShepherds.Text = "";
            // 
            // checkBoxRandom
            // 
            this.checkBoxRandom.AutoSize = true;
            this.checkBoxRandom.Location = new System.Drawing.Point(1096, 661);
            this.checkBoxRandom.Name = "checkBoxRandom";
            this.checkBoxRandom.Size = new System.Drawing.Size(108, 17);
            this.checkBoxRandom.TabIndex = 28;
            this.checkBoxRandom.Text = "RandomPositions";
            this.checkBoxRandom.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 752);
            this.Controls.Add(this.checkBoxRandom);
            this.Controls.Add(this.richTextBoxShepherds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxParameters);
            this.Controls.Add(this.checkBoxAnimationMode);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.ButtonSaveHerding);
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
        private System.Windows.Forms.Button ButtonSaveHerding;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.CheckBox checkBoxAnimationMode;
        private System.Windows.Forms.RichTextBox richTextBoxParameters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBoxShepherds;
        private System.Windows.Forms.CheckBox checkBoxRandom;
    }
}