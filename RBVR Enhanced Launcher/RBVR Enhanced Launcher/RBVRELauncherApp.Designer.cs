﻿namespace RBVR_Enhanced_Launcher
{
    partial class RBVRELauncherApp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RBVRELauncherApp));
            this.pickFileButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.noDeleteCheck = new System.Windows.Forms.CheckBox();
            this.Outputbtn = new System.Windows.Forms.Button();
            this.volumeAdjustCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.folderName = new System.Windows.Forms.Label();
            this.Outputtxt = new System.Windows.Forms.Label();
            this.buildButton = new System.Windows.Forms.Button();
            this.contentIdTextBox = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.tspeedMult = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tspeedMult)).BeginInit();
            this.SuspendLayout();
            // 
            // pickFileButton
            // 
            this.pickFileButton.Location = new System.Drawing.Point(6, 19);
            this.pickFileButton.Name = "pickFileButton";
            this.pickFileButton.Size = new System.Drawing.Size(92, 23);
            this.pickFileButton.TabIndex = 0;
            this.pickFileButton.Text = "Pick Folder(s)";
            this.pickFileButton.UseVisualStyleBackColor = true;
            this.pickFileButton.Click += new System.EventHandler(this.pickFileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.clearButton);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.pickFileButton);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 205);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1: Pick Patch(es)";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(104, 19);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(844, 147);
            this.listBox1.TabIndex = 3;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tspeedMult);
            this.groupBox2.Controls.Add(this.noDeleteCheck);
            this.groupBox2.Controls.Add(this.Outputbtn);
            this.groupBox2.Controls.Add(this.volumeAdjustCheckBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(4, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(875, 74);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2: Select your RBVR.exe file";
            // 
            // noDeleteCheck
            // 
            this.noDeleteCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.noDeleteCheck.AutoSize = true;
            this.noDeleteCheck.Location = new System.Drawing.Point(509, 45);
            this.noDeleteCheck.Name = "noDeleteCheck";
            this.noDeleteCheck.Size = new System.Drawing.Size(166, 17);
            this.noDeleteCheck.TabIndex = 7;
            this.noDeleteCheck.Text = "Don\'t Delete Existing Patches";
            this.noDeleteCheck.UseVisualStyleBackColor = true;
            this.noDeleteCheck.CheckedChanged += new System.EventHandler(this.noDeleteCheck_CheckedChanged);
            // 
            // Outputbtn
            // 
            this.Outputbtn.Location = new System.Drawing.Point(12, 41);
            this.Outputbtn.Name = "Outputbtn";
            this.Outputbtn.Size = new System.Drawing.Size(107, 23);
            this.Outputbtn.TabIndex = 3;
            this.Outputbtn.Text = "Pick RBVR Folder";
            this.Outputbtn.UseVisualStyleBackColor = true;
            this.Outputbtn.Click += new System.EventHandler(this.Outputbtn_Click);
            // 
            // volumeAdjustCheckBox
            // 
            this.volumeAdjustCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeAdjustCheckBox.AutoSize = true;
            this.volumeAdjustCheckBox.Location = new System.Drawing.Point(681, 45);
            this.volumeAdjustCheckBox.Name = "volumeAdjustCheckBox";
            this.volumeAdjustCheckBox.Size = new System.Drawing.Size(169, 17);
            this.volumeAdjustCheckBox.TabIndex = 6;
            this.volumeAdjustCheckBox.Text = "Use RB1-4 base Track Speed";
            this.volumeAdjustCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Track Speed Multiplier:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.folderName);
            this.groupBox3.Controls.Add(this.Outputtxt);
            this.groupBox3.Controls.Add(this.buildButton);
            this.groupBox3.Controls.Add(this.contentIdTextBox);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(4, 294);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(875, 40);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 3: Run RBVR";
            // 
            // folderName
            // 
            this.folderName.AutoSize = true;
            this.folderName.Location = new System.Drawing.Point(83, 21);
            this.folderName.Name = "folderName";
            this.folderName.Size = new System.Drawing.Size(0, 13);
            this.folderName.TabIndex = 8;
            // 
            // Outputtxt
            // 
            this.Outputtxt.AutoSize = true;
            this.Outputtxt.Location = new System.Drawing.Point(9, 21);
            this.Outputtxt.Name = "Outputtxt";
            this.Outputtxt.Size = new System.Drawing.Size(65, 13);
            this.Outputtxt.TabIndex = 7;
            this.Outputtxt.Text = "RBVR Path:";
            // 
            // buildButton
            // 
            this.buildButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buildButton.Location = new System.Drawing.Point(775, 11);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(75, 23);
            this.buildButton.TabIndex = 2;
            this.buildButton.Text = "Run";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // contentIdTextBox
            // 
            this.contentIdTextBox.AutoSize = true;
            this.contentIdTextBox.Location = new System.Drawing.Point(98, 16);
            this.contentIdTextBox.Name = "contentIdTextBox";
            this.contentIdTextBox.Size = new System.Drawing.Size(0, 13);
            this.contentIdTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 337);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Log:";
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(4, 353);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(850, 104);
            this.logBox.TabIndex = 5;
            // 
            // tspeedMult
            // 
            this.tspeedMult.DecimalPlaces = 2;
            this.tspeedMult.Location = new System.Drawing.Point(131, 19);
            this.tspeedMult.Name = "tspeedMult";
            this.tspeedMult.Size = new System.Drawing.Size(48, 20);
            this.tspeedMult.TabIndex = 8;
            this.tspeedMult.ValueChanged += new System.EventHandler(this.tspeedMult_ValueChanged);
            // 
            // RBVRELauncherApp
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 460);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RBVRELauncherApp";
            this.Text = "RBVR Enhanced Launcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tspeedMult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pickFileButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label contentIdTextBox;
        private System.Windows.Forms.CheckBox volumeAdjustCheckBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.Button Outputbtn;
        private System.Windows.Forms.Label Outputtxt;
        private System.Windows.Forms.Label folderName;
        private System.Windows.Forms.CheckBox noDeleteCheck;
        private System.Windows.Forms.NumericUpDown tspeedMult;
    }
}
