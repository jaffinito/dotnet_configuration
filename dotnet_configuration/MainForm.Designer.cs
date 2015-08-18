namespace NewRelic.AgentConfiguration
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
            this.ContentFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.MenuBarFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.MainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.OpenFile = new System.Windows.Forms.Button();
            this.SaveFile = new System.Windows.Forms.Button();
            this.TitleHeader = new System.Windows.Forms.Label();
            this.Saved = new System.Windows.Forms.Label();
            this.ValidateXml = new System.Windows.Forms.Button();
            this.AdvancedMenu = new System.Windows.Forms.Button();
            this.AdvancedPanel = new System.Windows.Forms.Panel();
            this.showAdvancedCheckBox = new System.Windows.Forms.CheckBox();
            this.DebuggingMenu = new System.Windows.Forms.Button();
            this.DebuggingPanel = new System.Windows.Forms.Panel();
            this.offCheckBox = new System.Windows.Forms.CheckBox();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.traceCheckBox = new System.Windows.Forms.CheckBox();
            this.finestCheckBox = new System.Windows.Forms.CheckBox();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.infoCheckBox = new System.Windows.Forms.CheckBox();
            this.DebugWarning = new System.Windows.Forms.Label();
            this.SaveFileAs = new System.Windows.Forms.Button();
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.AdvancedPanel.SuspendLayout();
            this.DebuggingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentFlow
            // 
            this.ContentFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentFlow.AutoScroll = true;
            this.ContentFlow.BackColor = System.Drawing.Color.White;
            this.ContentFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ContentFlow.Location = new System.Drawing.Point(152, 60);
            this.ContentFlow.Margin = new System.Windows.Forms.Padding(0);
            this.ContentFlow.Name = "ContentFlow";
            this.ContentFlow.Padding = new System.Windows.Forms.Padding(10);
            this.ContentFlow.Size = new System.Drawing.Size(880, 448);
            this.ContentFlow.TabIndex = 0;
            this.ContentFlow.WrapContents = false;
            // 
            // MenuBarFlow
            // 
            this.MenuBarFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MenuBarFlow.BackColor = System.Drawing.Color.White;
            this.MenuBarFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.MenuBarFlow.Location = new System.Drawing.Point(0, 60);
            this.MenuBarFlow.Margin = new System.Windows.Forms.Padding(0);
            this.MenuBarFlow.Name = "MenuBarFlow";
            this.MenuBarFlow.Size = new System.Drawing.Size(152, 448);
            this.MenuBarFlow.TabIndex = 1;
            // 
            // OpenFile
            // 
            this.OpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.OpenFile.FlatAppearance.BorderSize = 0;
            this.OpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.OpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenFile.ForeColor = System.Drawing.Color.White;
            this.OpenFile.Location = new System.Drawing.Point(0, 30);
            this.OpenFile.Margin = new System.Windows.Forms.Padding(0);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(50, 30);
            this.OpenFile.TabIndex = 2;
            this.OpenFile.Text = "Open";
            this.OpenFile.UseVisualStyleBackColor = false;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.SaveFile.FlatAppearance.BorderSize = 0;
            this.SaveFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.SaveFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.SaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveFile.ForeColor = System.Drawing.Color.White;
            this.SaveFile.Location = new System.Drawing.Point(50, 30);
            this.SaveFile.Margin = new System.Windows.Forms.Padding(0);
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(48, 30);
            this.SaveFile.TabIndex = 4;
            this.SaveFile.Text = "Save";
            this.SaveFile.UseVisualStyleBackColor = false;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // TitleHeader
            // 
            this.TitleHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.TitleHeader.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleHeader.ForeColor = System.Drawing.Color.White;
            this.TitleHeader.Location = new System.Drawing.Point(0, 0);
            this.TitleHeader.Margin = new System.Windows.Forms.Padding(0);
            this.TitleHeader.Name = "TitleHeader";
            this.TitleHeader.Size = new System.Drawing.Size(855, 30);
            this.TitleHeader.TabIndex = 0;
            this.TitleHeader.Text = ".Net Agent Configuration Tool";
            this.TitleHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Saved
            // 
            this.Saved.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Saved.AutoSize = true;
            this.Saved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.Saved.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Saved.ForeColor = System.Drawing.Color.White;
            this.Saved.Location = new System.Drawing.Point(357, 30);
            this.Saved.MinimumSize = new System.Drawing.Size(287, 30);
            this.Saved.Name = "Saved";
            this.Saved.Size = new System.Drawing.Size(287, 30);
            this.Saved.TabIndex = 5;
            this.Saved.Text = "Saved newrelic.config successfully";
            this.Saved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Saved.Visible = false;
            // 
            // ValidateXml
            // 
            this.ValidateXml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.ValidateXml.FlatAppearance.BorderSize = 0;
            this.ValidateXml.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.ValidateXml.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.ValidateXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ValidateXml.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValidateXml.ForeColor = System.Drawing.Color.White;
            this.ValidateXml.Location = new System.Drawing.Point(167, 30);
            this.ValidateXml.Margin = new System.Windows.Forms.Padding(0);
            this.ValidateXml.Name = "ValidateXml";
            this.ValidateXml.Size = new System.Drawing.Size(144, 30);
            this.ValidateXml.TabIndex = 6;
            this.ValidateXml.Text = "Validate Live Config";
            this.ValidateXml.UseVisualStyleBackColor = false;
            this.ValidateXml.Click += new System.EventHandler(this.ValidateXml_Click);
            // 
            // AdvancedMenu
            // 
            this.AdvancedMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.AdvancedMenu.FlatAppearance.BorderSize = 0;
            this.AdvancedMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(107)))), ((int)(((byte)(152)))));
            this.AdvancedMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(107)))), ((int)(((byte)(152)))));
            this.AdvancedMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdvancedMenu.Font = new System.Drawing.Font("Verdana", 8F);
            this.AdvancedMenu.ForeColor = System.Drawing.Color.White;
            this.AdvancedMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AdvancedMenu.Location = new System.Drawing.Point(950, 0);
            this.AdvancedMenu.Margin = new System.Windows.Forms.Padding(0);
            this.AdvancedMenu.Name = "AdvancedMenu";
            this.AdvancedMenu.Size = new System.Drawing.Size(82, 30);
            this.AdvancedMenu.TabIndex = 7;
            this.AdvancedMenu.Text = "Advanced v";
            this.AdvancedMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AdvancedMenu.UseVisualStyleBackColor = false;
            this.AdvancedMenu.Click += new System.EventHandler(this.AdvancedMenu_Click);
            // 
            // AdvancedPanel
            // 
            this.AdvancedPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedPanel.BackColor = System.Drawing.Color.White;
            this.AdvancedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvancedPanel.Controls.Add(this.showAdvancedCheckBox);
            this.AdvancedPanel.Location = new System.Drawing.Point(802, 31);
            this.AdvancedPanel.Name = "AdvancedPanel";
            this.AdvancedPanel.Size = new System.Drawing.Size(230, 38);
            this.AdvancedPanel.TabIndex = 0;
            this.AdvancedPanel.Visible = false;
            // 
            // showAdvancedCheckBox
            // 
            this.showAdvancedCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.showAdvancedCheckBox.FlatAppearance.BorderSize = 0;
            this.showAdvancedCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.showAdvancedCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.showAdvancedCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.showAdvancedCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showAdvancedCheckBox.Location = new System.Drawing.Point(0, 0);
            this.showAdvancedCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.showAdvancedCheckBox.Name = "showAdvancedCheckBox";
            this.showAdvancedCheckBox.Size = new System.Drawing.Size(230, 38);
            this.showAdvancedCheckBox.TabIndex = 3;
            this.showAdvancedCheckBox.Text = "Show Advanced Items";
            this.showAdvancedCheckBox.UseVisualStyleBackColor = true;
            this.showAdvancedCheckBox.Click += new System.EventHandler(this.showAdvancedCheckBox_Click);
            // 
            // DebuggingMenu
            // 
            this.DebuggingMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DebuggingMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.DebuggingMenu.FlatAppearance.BorderSize = 0;
            this.DebuggingMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(107)))), ((int)(((byte)(152)))));
            this.DebuggingMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(107)))), ((int)(((byte)(152)))));
            this.DebuggingMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DebuggingMenu.Font = new System.Drawing.Font("Verdana", 8F);
            this.DebuggingMenu.ForeColor = System.Drawing.Color.White;
            this.DebuggingMenu.Location = new System.Drawing.Point(857, 0);
            this.DebuggingMenu.Margin = new System.Windows.Forms.Padding(0);
            this.DebuggingMenu.Name = "DebuggingMenu";
            this.DebuggingMenu.Size = new System.Drawing.Size(91, 30);
            this.DebuggingMenu.TabIndex = 8;
            this.DebuggingMenu.Text = "Debugging v";
            this.DebuggingMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DebuggingMenu.UseVisualStyleBackColor = false;
            this.DebuggingMenu.Click += new System.EventHandler(this.DebuggingMenu_Click);
            // 
            // DebuggingPanel
            // 
            this.DebuggingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DebuggingPanel.BackColor = System.Drawing.Color.White;
            this.DebuggingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DebuggingPanel.Controls.Add(this.offCheckBox);
            this.DebuggingPanel.Controls.Add(this.allCheckBox);
            this.DebuggingPanel.Controls.Add(this.traceCheckBox);
            this.DebuggingPanel.Controls.Add(this.finestCheckBox);
            this.DebuggingPanel.Controls.Add(this.debugCheckBox);
            this.DebuggingPanel.Controls.Add(this.infoCheckBox);
            this.DebuggingPanel.Controls.Add(this.DebugWarning);
            this.DebuggingPanel.Location = new System.Drawing.Point(802, 31);
            this.DebuggingPanel.Name = "DebuggingPanel";
            this.DebuggingPanel.Size = new System.Drawing.Size(230, 265);
            this.DebuggingPanel.TabIndex = 3;
            this.DebuggingPanel.Visible = false;
            // 
            // offCheckBox
            // 
            this.offCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.offCheckBox.FlatAppearance.BorderSize = 0;
            this.offCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.offCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.offCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.offCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.offCheckBox.Location = new System.Drawing.Point(0, 225);
            this.offCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.offCheckBox.Name = "offCheckBox";
            this.offCheckBox.Size = new System.Drawing.Size(230, 38);
            this.offCheckBox.TabIndex = 10;
            this.offCheckBox.Text = "Off";
            this.offCheckBox.UseVisualStyleBackColor = true;
            this.offCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // allCheckBox
            // 
            this.allCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.allCheckBox.FlatAppearance.BorderSize = 0;
            this.allCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.allCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.allCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.allCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.allCheckBox.Location = new System.Drawing.Point(0, 187);
            this.allCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(230, 38);
            this.allCheckBox.TabIndex = 9;
            this.allCheckBox.Text = "All";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // traceCheckBox
            // 
            this.traceCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.traceCheckBox.FlatAppearance.BorderSize = 0;
            this.traceCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.traceCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.traceCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.traceCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traceCheckBox.Location = new System.Drawing.Point(0, 149);
            this.traceCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.traceCheckBox.Name = "traceCheckBox";
            this.traceCheckBox.Size = new System.Drawing.Size(230, 38);
            this.traceCheckBox.TabIndex = 8;
            this.traceCheckBox.Text = "Trace";
            this.traceCheckBox.UseVisualStyleBackColor = true;
            this.traceCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // finestCheckBox
            // 
            this.finestCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.finestCheckBox.FlatAppearance.BorderSize = 0;
            this.finestCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.finestCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.finestCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.finestCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.finestCheckBox.Location = new System.Drawing.Point(0, 111);
            this.finestCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.finestCheckBox.Name = "finestCheckBox";
            this.finestCheckBox.Size = new System.Drawing.Size(230, 38);
            this.finestCheckBox.TabIndex = 7;
            this.finestCheckBox.Text = "Finest";
            this.finestCheckBox.UseVisualStyleBackColor = true;
            this.finestCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.debugCheckBox.FlatAppearance.BorderSize = 0;
            this.debugCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.debugCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.debugCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.debugCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.debugCheckBox.Location = new System.Drawing.Point(0, 73);
            this.debugCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(230, 38);
            this.debugCheckBox.TabIndex = 6;
            this.debugCheckBox.Text = "Debug";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            this.debugCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // infoCheckBox
            // 
            this.infoCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.infoCheckBox.FlatAppearance.BorderSize = 0;
            this.infoCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.infoCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.infoCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.infoCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infoCheckBox.Location = new System.Drawing.Point(0, 35);
            this.infoCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.infoCheckBox.Name = "infoCheckBox";
            this.infoCheckBox.Size = new System.Drawing.Size(230, 38);
            this.infoCheckBox.TabIndex = 4;
            this.infoCheckBox.Text = "Info";
            this.infoCheckBox.UseVisualStyleBackColor = true;
            this.infoCheckBox.Click += new System.EventHandler(this.OnDebugLevelClick);
            // 
            // DebugWarning
            // 
            this.DebugWarning.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugWarning.Location = new System.Drawing.Point(0, 0);
            this.DebugWarning.Margin = new System.Windows.Forms.Padding(0);
            this.DebugWarning.Name = "DebugWarning";
            this.DebugWarning.Size = new System.Drawing.Size(229, 28);
            this.DebugWarning.TabIndex = 0;
            this.DebugWarning.Text = "Changes are made immediately";
            this.DebugWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveFileAs
            // 
            this.SaveFileAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.SaveFileAs.FlatAppearance.BorderSize = 0;
            this.SaveFileAs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.SaveFileAs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(136)))), ((int)(((byte)(154)))));
            this.SaveFileAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveFileAs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveFileAs.ForeColor = System.Drawing.Color.White;
            this.SaveFileAs.Location = new System.Drawing.Point(98, 30);
            this.SaveFileAs.Margin = new System.Windows.Forms.Padding(0);
            this.SaveFileAs.Name = "SaveFileAs";
            this.SaveFileAs.Size = new System.Drawing.Size(69, 30);
            this.SaveFileAs.TabIndex = 9;
            this.SaveFileAs.Text = "Save As";
            this.SaveFileAs.UseVisualStyleBackColor = false;
            this.SaveFileAs.Click += new System.EventHandler(this.SaveFileAs_Click);
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.AutoSize = true;
            this.LoadingLabel.BackColor = System.Drawing.Color.White;
            this.LoadingLabel.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadingLabel.ForeColor = System.Drawing.Color.DimGray;
            this.LoadingLabel.Location = new System.Drawing.Point(443, 240);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(146, 29);
            this.LoadingLabel.TabIndex = 0;
            this.LoadingLabel.Text = "Loading...";
            this.LoadingLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.ClientSize = new System.Drawing.Size(1032, 508);
            this.Controls.Add(this.LoadingLabel);
            this.Controls.Add(this.SaveFileAs);
            this.Controls.Add(this.AdvancedPanel);
            this.Controls.Add(this.DebuggingPanel);
            this.Controls.Add(this.DebuggingMenu);
            this.Controls.Add(this.AdvancedMenu);
            this.Controls.Add(this.ValidateXml);
            this.Controls.Add(this.Saved);
            this.Controls.Add(this.TitleHeader);
            this.Controls.Add(this.SaveFile);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.MenuBarFlow);
            this.Controls.Add(this.ContentFlow);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1040, 535);
            this.Name = "MainForm";
            this.Text = ".Net Agent Configuration Tool";
            this.AdvancedPanel.ResumeLayout(false);
            this.DebuggingPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ContentFlow;
        private System.Windows.Forms.FlowLayoutPanel MenuBarFlow;
        private System.Windows.Forms.ToolTip MainFormToolTip;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Button SaveFile;
        private System.Windows.Forms.Label TitleHeader;
        private System.Windows.Forms.Label Saved;
        private System.Windows.Forms.Button ValidateXml;
        private System.Windows.Forms.Button AdvancedMenu;
        private System.Windows.Forms.Panel AdvancedPanel;
        private System.Windows.Forms.Button DebuggingMenu;
        private System.Windows.Forms.Panel DebuggingPanel;
        private System.Windows.Forms.Label DebugWarning;
        private System.Windows.Forms.CheckBox showAdvancedCheckBox;
        private System.Windows.Forms.CheckBox offCheckBox;
        private System.Windows.Forms.CheckBox allCheckBox;
        private System.Windows.Forms.CheckBox traceCheckBox;
        private System.Windows.Forms.CheckBox finestCheckBox;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.CheckBox infoCheckBox;
        private System.Windows.Forms.Button SaveFileAs;
        private System.Windows.Forms.Label LoadingLabel;
    }
}

