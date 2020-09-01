using System.Windows.Forms;

namespace MultiAppsLauncher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_AddApp = new System.Windows.Forms.Button();
            this.button_removeApp = new System.Windows.Forms.Button();
            this.button_clearList = new System.Windows.Forms.Button();
            this.button_launch = new System.Windows.Forms.Button();
            this.checkBox_launchOnStart = new System.Windows.Forms.CheckBox();
            this.listBox_appsList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editArgumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_EditArgs = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.delayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBox_MinimizeToTray = new System.Windows.Forms.CheckBox();
            this.checkBox_StartMinimized = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button_AddApp
            // 
            this.button_AddApp.Location = new System.Drawing.Point(12, 12);
            this.button_AddApp.Name = "button_AddApp";
            this.button_AddApp.Size = new System.Drawing.Size(81, 23);
            this.button_AddApp.TabIndex = 1;
            this.button_AddApp.Text = "Add APP";
            this.button_AddApp.UseVisualStyleBackColor = true;
            this.button_AddApp.Click += new System.EventHandler(this.button_AddApp_Click);
            // 
            // button_removeApp
            // 
            this.button_removeApp.Location = new System.Drawing.Point(99, 12);
            this.button_removeApp.Name = "button_removeApp";
            this.button_removeApp.Size = new System.Drawing.Size(81, 23);
            this.button_removeApp.TabIndex = 2;
            this.button_removeApp.Text = "Remove APP";
            this.button_removeApp.UseVisualStyleBackColor = true;
            this.button_removeApp.Click += new System.EventHandler(this.button_removeApp_Click);
            // 
            // button_clearList
            // 
            this.button_clearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clearList.Location = new System.Drawing.Point(501, 12);
            this.button_clearList.Name = "button_clearList";
            this.button_clearList.Size = new System.Drawing.Size(81, 23);
            this.button_clearList.TabIndex = 3;
            this.button_clearList.Text = "Clear";
            this.button_clearList.UseVisualStyleBackColor = true;
            this.button_clearList.Click += new System.EventHandler(this.button_clearList_Click);
            // 
            // button_launch
            // 
            this.button_launch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_launch.Location = new System.Drawing.Point(501, 271);
            this.button_launch.Name = "button_launch";
            this.button_launch.Size = new System.Drawing.Size(81, 23);
            this.button_launch.TabIndex = 4;
            this.button_launch.Text = "Launch APPs";
            this.button_launch.UseVisualStyleBackColor = true;
            this.button_launch.Click += new System.EventHandler(this.button_launch_Click);
            // 
            // checkBox_launchOnStart
            // 
            this.checkBox_launchOnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_launchOnStart.AutoSize = true;
            this.checkBox_launchOnStart.Location = new System.Drawing.Point(12, 275);
            this.checkBox_launchOnStart.Name = "checkBox_launchOnStart";
            this.checkBox_launchOnStart.Size = new System.Drawing.Size(112, 17);
            this.checkBox_launchOnStart.TabIndex = 5;
            this.checkBox_launchOnStart.Text = "Launch on startup";
            this.checkBox_launchOnStart.UseVisualStyleBackColor = true;
            // 
            // listBox_appsList
            // 
            this.listBox_appsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_appsList.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox_appsList.FormattingEnabled = true;
            this.listBox_appsList.Location = new System.Drawing.Point(12, 41);
            this.listBox_appsList.Name = "listBox_appsList";
            this.listBox_appsList.Size = new System.Drawing.Size(570, 225);
            this.listBox_appsList.TabIndex = 6;
            this.listBox_appsList.DoubleClick += new System.EventHandler(this.listBox_appsList_DoubleClick);
            this.listBox_appsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_appsList_MouseDown);
            this.listBox_appsList.SelectedIndexChanged += new System.EventHandler(this.listBox_appsList_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.editArgumentsToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 70);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.launchToolStripMenuItem.Text = "Launch";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.launchToolStripMenuItem_Click);
            // 
            // editArgumentsToolStripMenuItem
            // 
            this.editArgumentsToolStripMenuItem.Name = "editArgumentsToolStripMenuItem";
            this.editArgumentsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.editArgumentsToolStripMenuItem.Text = "Edit Arguments";
            this.editArgumentsToolStripMenuItem.Click += new System.EventHandler(this.editArgumentsToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // button_EditArgs
            // 
            this.button_EditArgs.Location = new System.Drawing.Point(186, 12);
            this.button_EditArgs.Name = "button_EditArgs";
            this.button_EditArgs.Size = new System.Drawing.Size(89, 23);
            this.button_EditArgs.TabIndex = 8;
            this.button_EditArgs.Text = "Edit Arguments";
            this.button_EditArgs.UseVisualStyleBackColor = true;
            this.button_EditArgs.Click += new System.EventHandler(this.button_EditArgs_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 299);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(594, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Status";
            // 
            // delayNumericUpDown
            // 
            this.delayNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delayNumericUpDown.Location = new System.Drawing.Point(396, 272);
            this.delayNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.delayNumericUpDown.Name = "delayNumericUpDown";
            this.delayNumericUpDown.Size = new System.Drawing.Size(73, 20);
            this.delayNumericUpDown.TabIndex = 12;
            this.delayNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(472, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "ms";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(346, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Delay:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Minimized to system tray";
            this.notifyIcon.BalloonTipTitle = "-";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "-";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // checkBox_MinimizeToTray
            // 
            this.checkBox_MinimizeToTray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_MinimizeToTray.AutoSize = true;
            this.checkBox_MinimizeToTray.Location = new System.Drawing.Point(232, 275);
            this.checkBox_MinimizeToTray.Name = "checkBox_MinimizeToTray";
            this.checkBox_MinimizeToTray.Size = new System.Drawing.Size(98, 17);
            this.checkBox_MinimizeToTray.TabIndex = 15;
            this.checkBox_MinimizeToTray.Text = "Minimize to tray";
            this.checkBox_MinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // checkBox_StartMinimized
            // 
            this.checkBox_StartMinimized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_StartMinimized.AutoSize = true;
            this.checkBox_StartMinimized.Location = new System.Drawing.Point(130, 275);
            this.checkBox_StartMinimized.Name = "checkBox_StartMinimized";
            this.checkBox_StartMinimized.Size = new System.Drawing.Size(96, 17);
            this.checkBox_StartMinimized.TabIndex = 16;
            this.checkBox_StartMinimized.Text = "Start minimized";
            this.checkBox_StartMinimized.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 321);
            this.Controls.Add(this.checkBox_StartMinimized);
            this.Controls.Add(this.checkBox_MinimizeToTray);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayNumericUpDown);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_EditArgs);
            this.Controls.Add(this.listBox_appsList);
            this.Controls.Add(this.checkBox_launchOnStart);
            this.Controls.Add(this.button_launch);
            this.Controls.Add(this.button_clearList);
            this.Controls.Add(this.button_removeApp);
            this.Controls.Add(this.button_AddApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(610, 263);
            this.Name = "MainForm";
            this.Text = "Multi Apps Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_AddApp;
        private System.Windows.Forms.Button button_removeApp;
        private System.Windows.Forms.Button button_clearList;
        private System.Windows.Forms.Button button_launch;
        private System.Windows.Forms.CheckBox checkBox_launchOnStart;
        private System.Windows.Forms.ListBox listBox_appsList;
        private Button button_EditArgs;
        private ContextMenuStrip contextMenuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private ToolStripMenuItem launchToolStripMenuItem;
        private ToolStripMenuItem editArgumentsToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private NumericUpDown delayNumericUpDown;
        private Label label1;
        private Label label2;
        private NotifyIcon notifyIcon;
        private CheckBox checkBox_MinimizeToTray;
        private CheckBox checkBox_StartMinimized;
    }
}

