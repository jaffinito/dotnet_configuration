using log4net;
using log4net.Config;
using NewRelic.AgentConfiguration.Input;
using NewRelic.AgentConfiguration.Output;
using NewRelic.AgentConfiguration.Parts;
using NewRelic.AgentConfiguration.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace NewRelic.AgentConfiguration
{
    public partial class MainForm : Form
    {
        public static string CurrentSection = "";
        public static string ConfigPath = "";
        public static string XsdPath = "";
        public bool ShowAdvanced = false;

        private System.Timers.Timer _savedTimer;
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));


        //dark grey: 71 71 71
        //dark blue mouse over: 36 107 152

        /// <summary>
        /// Constructor for the primary Form control.  Sets up the mouse hooks for the dropsdowns, and the time for the error/save message.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\enabledebug") || File.Exists(System.Windows.Forms.Application.StartupPath + "\\enabledebug.txt"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("NewRelic.AgentConfiguration.Resources.log4net_config.xml"))
                {
                    XmlConfigurator.Configure(resource);
                }
                log.Info(" - log4net started.");
            }

            _savedTimer = new System.Timers.Timer(2500);
            _savedTimer.SynchronizingObject = this;
            _savedTimer.Elapsed += SavedTimer_Elapsed;

            try
            {
                UpdateDebuggingMenu();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            FileLocation.BasePath();

            try
            {
                //Mouseover stuff
                MouseHookProcedure = new HookProc(MouseHookProc);
                hHook = NativeMethods.SetWindowsHookEx(WH_MOUSE, MouseHookProcedure, (IntPtr)0, AppDomain.GetCurrentThreadId());
                //END
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Sets the Debug dropdown to the correct current debug level.  Not dynamic (does not monitor filesystem).
        /// </summary>
        private void UpdateDebuggingMenu(bool fromClick = false)
        {
            var debug = new AgentDebug();
            var currentLevel = debug.GetLogLevel();
            log.Debug("CurrentLevel - " + currentLevel);
            var debugPanel = this.Controls.Find("DebuggingPanel", true)[0] as Panel;
            foreach (var control in debugPanel.Controls)
            {
                var checkBox = control as CheckBox;
                if (checkBox != null)
                {
                    if (checkBox.Name == currentLevel + "CheckBox")
                    {
                        checkBox.Checked = true;
                    }
                    else
                    {
                        checkBox.Checked = false;
                    }
                }
            }

            if (currentLevel == "nofile" && fromClick)
            {
                Saved.Text = "The newrelic.config file could not be located.";
                Saved.BackColor = Color.DarkRed;
                Saved.Visible = true;
                _savedTimer.Start();
            }
        }

        /// <summary>
        /// Click event handler for the buttons INSIDE the Debug dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDebugLevelClick(object sender, EventArgs e)
        {
            var newDebugLevel = (sender as CheckBox).Text.ToLower();
            var debug = new AgentDebug();
            debug.SetLogLevel((AgentDebug.LogLevel)Enum.Parse(typeof(AgentDebug.LogLevel), newDebugLevel));
            UpdateDebuggingMenu(true);
        }

        /// <summary>
        /// Elapsed event handler for the error/save message box.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SavedTimer_Elapsed(Object source, ElapsedEventArgs e)
        {
            Saved.Visible = false;
        }

        /// <summary>
        /// Click event handler for the Open button.  This handles finding and opening the XSD file and the CONFIG file.
        /// Will always prompt for CONFIG and only prompts for XSD if it cannot locate it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            LoadingLabel.Visible = true;
            var reader = new XsdReader();
            ConfigurationFile.Xsd = new Element();

            if (reader.ProcessSchema(FileLocation.XsdFile))
            {
                XsdPath = FileLocation.XsdFile;
            }
            else
            {
                var openFile = new OpenFileDialog();
                openFile.Title = "Select the newrelic.xsd file";
                openFile.InitialDirectory = "c:\\ProgramData\\New Relic\\.Net Agent";
                openFile.FileName = "newrelic.xsd";
                openFile.Filter = "xsd files (*.xsd)|*.xsd|All files (*.*)|*.*";
                openFile.FilterIndex = 0;
                openFile.Multiselect = false;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //reader = new XsdReader();
                        reader.ProcessSchema(openFile.FileName);
                        XsdPath = openFile.FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        if (DialogResult.Yes == MessageBox.Show("Do you want to use the built in newrelic.xsd file?", "Use built in newrelic.xsd?", MessageBoxButtons.YesNo))
                        {
                            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("NewRelic.AgentConfiguration.Resources.newrelic.xsd"))
                            {
                                using (var file = new FileStream(Application.StartupPath + "\\newrelic.xsd", FileMode.Create, FileAccess.Write))
                                {
                                    resource.CopyTo(file);
                                }
                            }

                            reader.ProcessSchema(Application.StartupPath + "\\newrelic.xsd");
                            XsdPath = Application.StartupPath + "\\newrelic.xsd";
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }

            if (ConfigurationFile.Xsd != null)
            {
                var openFile = new OpenFileDialog();
                openFile.Title = "Select the newrelic.config file";
                openFile.InitialDirectory = Path.GetDirectoryName(FileLocation.ConfigFile);
                openFile.FileName = "newrelic.config";
                openFile.Filter = "config files (*.config)|*.config|All files (*.*)|*.*";
                openFile.FilterIndex = 0;
                openFile.Multiselect = false;
                openFile.RestoreDirectory = true;

                
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    
                    try
                    {
                        var xm = new XmlWalker();
                        xm.ProcessXml(openFile.FileName);
                        ConfigPath = openFile.FileName;
                        var cm = new ConfigMerge();
                        LoadUI();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        if (DialogResult.Yes == MessageBox.Show("Do you want to use the built in newrelic.config file?", "Use built in newrelic.config?", MessageBoxButtons.YesNo))
                        {
                            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("NewRelic.AgentConfiguration.Resources.newrelic.config"))
                            {
                                using (var file = new FileStream(Application.StartupPath + "\\newrelic.config", FileMode.Create, FileAccess.Write))
                                {
                                    resource.CopyTo(file);
                                }
                            }

                            var xm = new XmlWalker();
                            xm.ProcessXml(Application.StartupPath + "\\newrelic.config");
                            ConfigPath = Application.StartupPath + "\\newrelic.config";
                            var cm = new ConfigMerge();
                            LoadUI();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
            LoadingLabel.Visible = false;
        }

        /// <summary>
        /// Click event handler for saving the new config file. Validates prior to saving.  Displays the error/save message box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                var reader = new FormReader(ContentFlow);

                var cv = new ConfigValidator();
                if (cv.Validate(true) != 0)
                {
                    throw new InvalidDataException("XSD-based Validation Failed.  Check for errors.");
                }

                var cf = new ConfigWriter();
                cf.Write();
                Saved.Text = "Saved newrelic.config successfully";
                Saved.BackColor = Color.FromArgb(244, 144, 0);
                Saved.Visible = true;
                _savedTimer.Start();
            }
            catch (Exception ex)
            {
                log.Error("01 - " + ex.Message);
                Saved.Text = ex.Message;
                Saved.BackColor = Color.DarkRed;
                Saved.Visible = true;
                _savedTimer.Start();
            }
        }

        /// <summary>
        /// Click event handler for saving the new config file to a new location. Validates prior to saving.  Displays the error/save message box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileAs_Click(object sender, EventArgs e)
        {
            try
            {
                var reader = new FormReader(ContentFlow);

                var cv = new ConfigValidator();
                if (cv.Validate(true) != 0)
                {
                    throw new InvalidDataException("XSD-based Validation Failed.  Check for errors.");
                }

                var cf = new ConfigWriter();

                var safeFile = new SaveFileDialog();
                safeFile.Title = "Select the new location for the newrelic.config file";
                safeFile.FileName = "newrelic.config";
                safeFile.Filter = "config files (*.config)|*.config|All files (*.*)|*.*";
                safeFile.FilterIndex = 0;
                safeFile.RestoreDirectory = true;

                if (safeFile.ShowDialog() == DialogResult.OK)
                {
                    cf.Write(safeFile.FileName);
                }
                
                Saved.Text = "Saved newrelic.config successfully";
                Saved.BackColor = Color.FromArgb(244, 144, 0);
                Saved.Visible = true;
                _savedTimer.Start();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Saved.Text = ex.Message;
                Saved.BackColor = Color.DarkRed;
                Saved.Visible = true;
                _savedTimer.Start();
            }
        }

        /// <summary>
        /// Build and displays the user interface for the tool based on th4e CONFIG and XSD file.
        /// This includes both the sidebar and the editing pane.
        /// </summary>
        public void LoadUI()
        {
            if (MenuBarFlow.Controls.Count > -1)
            {
                MenuBarFlow.SuspendLayout();
                MenuBarFlow.Controls.Clear();
                MenuBarFlow.Controls.Add(FormElements.Controls.MenuButton(ConfigurationFile.Xsd, MainFormToolTip));

                foreach (var element in ConfigurationFile.Xsd.Elements)
                {
                    if (!element.IsAdvanced)
                    {
                        MenuBarFlow.Controls.Add(FormElements.Controls.MenuButton(element, MainFormToolTip));
                    }
                    else if (element.IsAdvanced && ShowAdvanced)
                    {
                        MenuBarFlow.Controls.Add(FormElements.Controls.MenuButton(element, MainFormToolTip));
                    }
                }

                MenuBarFlow.ResumeLayout();
            }

            ContentFlow.SuspendLayout();
            ContentFlow.Controls.Clear();

            var ui = new UIBuilder(ContentFlow, MainFormToolTip);
            ui.Build(ShowAdvanced);
            ReloadSection();
            ContentFlow.ResumeLayout();
        }

        /// <summary>
        /// Rebuilds and reloads a section when needed.
        /// </summary>
        public void ReloadSection()
        {
            if (!String.IsNullOrWhiteSpace(CurrentSection))
            {
                var flowControls = this.Controls.Find("ContentFlow", false);
                var flow = flowControls[0] as FlowLayoutPanel;

                foreach (Control control in flow.Controls)
                {
                    control.Visible = false;
                }

                var headers = this.Controls.Find("lHeaderLabel", true);
                var header = headers[0] as Label;

                var controls = flow.Controls.Find(CurrentSection, false);
                header.Text = (controls[0].Tag as Element).Documentation;

                header.Visible = true;
                controls[0].Visible = true;
            }
        }

        /// <summary>
        /// Click event handler for the Validate button on the main form. Uses the error/save dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateXml_Click(object sender, EventArgs e)
        {
            var cv = new ConfigValidator();
            int errorCount = cv.Validate();
            if (errorCount > 0)
            {
                Saved.Text = errorCount + " errors found, see validation.txt";
                Saved.BackColor = Color.DarkRed;
            }
            else if (errorCount == 0)
            {
                Saved.Text = "Valid";
                Saved.BackColor = Color.FromArgb(244, 144, 0);
            }
            else if (errorCount == -1)
            {
                Saved.Text = "Trouble reading the config or xsd files.";
                Saved.BackColor = Color.DarkRed;
            }

            Saved.Visible = true;
            _savedTimer.Start();
        }

        /// <summary>
        /// Click event handler to open/close the Advanced dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvancedMenu_Click(object sender, EventArgs e)
        {
            AdvancedPanel.BringToFront();
            if (AdvancedPanel.Visible)
            {
                AdvancedPanel.Visible = false;
            }
            else
            {
                AdvancedPanel.Visible = true;
            }
        }

        /// <summary>
        /// Click event handler to open/close the Debugging dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebuggingMenu_Click(object sender, EventArgs e)
        {
            DebuggingPanel.BringToFront();
            if (DebuggingPanel.Visible)
            {
                DebuggingPanel.Visible = false;
            }
            else
            {
                DebuggingPanel.Visible = true;
            }
        }

        /// <summary>
        /// Click event handler for the show advanced items button INSIDE the Advanced dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAdvancedCheckBox_Click(object sender, EventArgs e)
        {
            AdvancedPanel.Visible = false;
            if(showAdvancedCheckBox.Checked)
            {
                showAdvancedCheckBox.Text = "Hide Advanced Items";
            }
            else
            {
                showAdvancedCheckBox.Text = "Show Advanced Items";
            }
            FormReader reader = new FormReader(ContentFlow);
            MultiLineCollection.Clear();
            ShowAdvanced = showAdvancedCheckBox.Checked;
            LoadUI();
        }

        /// <summary>
        /// Allows for clicking anywhere in the main form and closing of the dropdowns.
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

            if (nCode >= 0 && wParam.ToInt32() == WM_LBUTTONUP)
            {
                var mousePosition = this.PointToClient(Control.MousePosition);
                var control = this.GetChildAtPoint(mousePosition);
                if (control != this.AdvancedPanel && control != this.AdvancedMenu && AdvancedPanel.Visible)
                {
                    if (AdvancedPanel.Visible)
                    {
                        AdvancedPanel.Visible = false;
                    }
                }

                if (control != this.DebuggingPanel && control != this.DebuggingMenu && DebuggingPanel.Visible)
                {
                    if (DebuggingPanel.Visible)
                    {
                        DebuggingPanel.Visible = false;
                    }
                }
            }

            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        #region Pinvoke for mouse stuff
        //PInvoke Sig for dropdowns
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static int hHook = 0;
        public int WH_MOUSE = 7;
        private const int WM_LBUTTONUP = 0x0202;
        private HookProc MouseHookProcedure;

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
        #endregion
    }      
}