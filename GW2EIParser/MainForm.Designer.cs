﻿namespace GW2EIParser
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
                _settingsForm.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnParse = new System.Windows.Forms.Button();
            this.btnPopulate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonState = new System.Windows.Forms.DataGridViewButtonColumn();
            this.operatorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logFileWatcher = new System.IO.FileSystemWatcher();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.labWatchingDir = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operatorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logFileWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(12, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(279, 20);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "将EVTC文件拖拽至此";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnParse
            // 
            this.btnParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParse.Location = new System.Drawing.Point(525, 307);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(154, 23);
            this.btnParse.TabIndex = 10;
            this.btnParse.Text = "全部分析";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.BtnParseClick);
            // 
            // btnPopulate
            // 
            this.btnPopulate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPopulate.Location = new System.Drawing.Point(97, 331);
            this.btnPopulate.Name = "btnPopulate";
            this.btnPopulate.Size = new System.Drawing.Size(154, 23);
            this.btnPopulate.TabIndex = 10;
            this.btnPopulate.Text = "从文件目录中添加";
            this.btnPopulate.UseVisualStyleBackColor = true;
            this.btnPopulate.Click += new System.EventHandler(this.BtnPopulateFromDirectory);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(525, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "全部取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSettings.Location = new System.Drawing.Point(16, 331);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 15;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettingsClick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(604, 333);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowDrop = true;
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.AutoGenerateColumns = false;
            this.dgvFiles.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.locationDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.ButtonState});
            this.dgvFiles.DataSource = this.operatorBindingSource;
            this.dgvFiles.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFiles.Location = new System.Drawing.Point(16, 46);
            this.dgvFiles.MultiSelect = false;
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.Size = new System.Drawing.Size(663, 255);
            this.dgvFiles.TabIndex = 17;
            this.dgvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvFilesCellContentClick);
            this.dgvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.DgvFilesDragDrop);
            this.dgvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.DgvFilesDragEnter);
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "位置";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            this.locationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "状态";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ButtonState
            // 
            this.ButtonState.DataPropertyName = "ButtonText";
            this.ButtonState.HeaderText = "";
            this.ButtonState.Name = "ButtonState";
            this.ButtonState.ReadOnly = true;
            // 
            // operatorBindingSource
            // 
            this.operatorBindingSource.DataSource = typeof(GW2EIParser.FormOperationController);
            // 
            // logFileWatcher
            // 
            this.logFileWatcher.EnableRaisingEvents = true;
            this.logFileWatcher.IncludeSubdirectories = true;
            this.logFileWatcher.SynchronizingObject = this;
            this.logFileWatcher.Created += new System.IO.FileSystemEventHandler(this.LogFileWatcher_Created);
            this.logFileWatcher.Renamed += new System.IO.RenamedEventHandler(this.LogFileWatcher_Renamed);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(16, 357);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(29, 13);
            this.VersionLabel.TabIndex = 17;
            this.VersionLabel.Text = "V1.3";
            // 
            // labWatchingDir
            // 
            this.labWatchingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labWatchingDir.AutoEllipsis = true;
            this.labWatchingDir.Location = new System.Drawing.Point(16, 312);
            this.labWatchingDir.Name = "labWatchingDir";
            this.labWatchingDir.Size = new System.Drawing.Size(504, 13);
            this.labWatchingDir.TabIndex = 18;
            this.labWatchingDir.Text = "Watching log dir";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(696, 375);
            this.Controls.Add(this.labWatchingDir);
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.btnPopulate);
            this.Controls.Add(this.lblHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "激战2EI日志分析_Evaliz(吟游神)汉化";
            this.TransparencyKey = System.Drawing.Color.OrangeRed;
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operatorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logFileWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Button btnPopulate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.BindingSource operatorBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ButtonState;
        private System.Windows.Forms.Label VersionLabel;
        private System.IO.FileSystemWatcher logFileWatcher;
        private System.Windows.Forms.Label labWatchingDir;
    }
}

