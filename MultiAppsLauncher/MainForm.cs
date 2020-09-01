using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MultiAppsLauncher
{
    public partial class MainForm : Form
    {
        private bool thisProcessIsAllowed = false;

        /// <summary>
        /// Path of file where the settings of this process is saved.
        /// </summary>
        private string settingsFilePath = Application.StartupPath + "\\" + Process.GetCurrentProcess().ProcessName + ".ini";

        private string launchOnStartupTag = "launchOnStartup";
        private string startMinimizedTag = "startMinimized";
        private string minimizeToTrayTag = "minimizeToTray";
        private string delayTag = "delay";
        private string xCoordTag = "xCoord";
        private string yCoordTag = "yCoord";
        private string widthTag = "width";
        private string heightTag = "height";
        private string maximizedTag = "maximized";

        /// <summary>
        /// Path of file where the current list of applications is saved.
        /// </summary>
        private string appsListFilePath = Application.StartupPath + "\\AppsList.txt";

        /// <summary>
        /// Current list of applications.
        /// </summary>
        private List<ApplicationInfo> appsList = new List<ApplicationInfo>();

        private object clipboard;
        private bool keyProcessed;

        private OpenFileDialog openFileDialog = new OpenFileDialog();


        // ----------------------------- Form Functions -----------------------------------------

        /// <summary>
        /// Main from constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Main from load event.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            thisProcessIsAllowed = AllowMultipleInstancesOfThisProcess(false);

            Debug(this.ProductName + " started.");

            LoadSettingsFromDisk();
            LoadApplicationsListFromDisk();

            RefreshButtons();

            Debug("Ready.");

            if (checkBox_launchOnStart.Checked)
                LaunchApplications();
        }

        /// <summary>
        /// Main from closing event.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!thisProcessIsAllowed)
                return;

            SaveSettingsToDisk();
        }

        // ----------------------------- Event Handlers -----------------------------------------

        /// <summary>
        /// Add application button click event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void button_AddApp_Click(object sender, EventArgs e)
        {
            openFileDialog.Reset();
            openFileDialog.Filter = "Executable(*.exe,*.bat,*.com)|*.exe;*.bat;*.com";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length > 0)
                {
                    AddApplicationsToCurrentList(openFileDialog.FileNames);

                    if (appsList.Count > 0)
                        SaveApplicationsListToDisk();
                }
            }

            RefreshButtons();
        }

        /// <summary>
        /// Remove application button click event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void button_removeApp_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            RemoveApplicationFromCurrentList(i);
        }

        /// <summary>
        /// Clear list button click event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void button_clearList_Click(object sender, EventArgs e)
        {
            listBox_appsList.Items.Clear();
            appsList.Clear();
            RefreshButtons();
            Debug("APPs List cleared.");
        }

        /// <summary>
        /// Edit application arguments button click event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void button_EditArgs_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            EditApplicationArguments(i);
        }

        /// <summary>
        /// Launch applications button click event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void button_launch_Click(object sender, EventArgs e)
        {
            LaunchApplications();
        }

        /// <summary>
        /// List of applications mouse down event handler (selection).
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void listBox_appsList_MouseDown(object sender, MouseEventArgs e)
        {
            listBox_appsList.SelectedIndex = listBox_appsList.IndexFromPoint(e.X, e.Y);

            if (e.Button == MouseButtons.Right)
            {
                int i = listBox_appsList.SelectedIndex;

                if (i >= 0 && i < appsList.Count)
                {
                    if (appsList.Count > 0)
                    {
                        contextMenuStrip1.Visible = true;
                        launchToolStripMenuItem.Visible = true;
                        editArgumentsToolStripMenuItem.Visible = true;
                        removeToolStripMenuItem.Visible = true;
                    }
                }
                else
                {
                    contextMenuStrip1.Visible = false;
                    launchToolStripMenuItem.Visible = false;
                    editArgumentsToolStripMenuItem.Visible = false;
                    removeToolStripMenuItem.Visible = false;
                }
            }

            RefreshButtons();
        }

        /// <summary>
        /// List of applications mouse click event handler (selection).
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        /*private void listBox_appsList_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            if (i >= 0 && i < appsList.Count)
                RefreshButtons();
        }*/

        /// <summary>
        /// List of applications mouse double click event handler (launch).
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void listBox_appsList_DoubleClick(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            if(i >= 0 && i < appsList.Count)
                LaunchApplication(listBox_appsList.Items[i].ToString(), appsList[i].applicationPath, appsList[i].applicationArguments);
        }

        /// <summary>
        /// Selected index of the list of applications has changed event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void listBox_appsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        /// <summary>
        /// Drag & Drop enter event handler (change the icon).
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Add aplication(s) with the Drag & Drop event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (AddApplicationsToCurrentList(files) > 0)
                SaveApplicationsListToDisk();
        }

        /// <summary>
        /// Main form resize event handler - Minimize to system tray.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (checkBox_MinimizeToTray.Checked && this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon.BalloonTipTitle = Application.ProductName;
                this.notifyIcon.Text = Application.ProductName;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Notify Icon double click event handler - Restore from system tray.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        /// <summary>
        /// Launch aplication with the right mouse button menu event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            if (i >= 0 && i < appsList.Count)
                LaunchApplication(listBox_appsList.Items[i].ToString(), appsList[i].applicationPath, appsList[i].applicationArguments);
        }

        /// <summary>
        /// Edit aplication arguments with the right mouse button menu event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void editArgumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            EditApplicationArguments(i);
        }

        /// <summary>
        /// Remove aplication with the right mouse button menu event handler.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = listBox_appsList.SelectedIndex;
            RemoveApplicationFromCurrentList(i);
        }

        /// <summary>
        /// Key down event handler for this Main Form.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyProcessed)
                return;

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                int i = listBox_appsList.SelectedIndex;
                if (i >= 0 && i < appsList.Count)
                {
                    if (listBox_appsList.Items[i] != null)
                    {
                        clipboard = appsList[i].Clone();
                        keyProcessed = true;
                        Debug("Copy: " + listBox_appsList.Items[i].ToString());
                    }
                }
            }
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (clipboard != null && clipboard.GetType().ToString().Equals("MultiAppsLauncher.ApplicationInfo"))
                {
                    if (AddApplicationToCurrentList((ApplicationInfo)clipboard, appsList.Count))
                        SaveApplicationsListToDisk();
                    keyProcessed = true;
                }
            }
        }

        /// <summary>
        /// Key up event handler for this Main Form.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            keyProcessed = false;
        }

        // ----------------------------- Main Functions -----------------------------------------

        /// <summary>
        /// Enable or disable multiples instance of this process.
        /// </summary>
        /// <param name="multipleInstances">If true, allow multiple instances. Else, allow only one instance.</param>
        private bool AllowMultipleInstancesOfThisProcess(bool multipleInstances)
        {
            if (multipleInstances)
                return true;

            string thisProcessName = Process.GetCurrentProcess().ProcessName;

            Process[] processes = Process.GetProcessesByName(thisProcessName);

            if (processes.Length > 1)
            {
                thisProcessIsAllowed = false;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Only one instance of " + this.ProductName + " is allowed.", "Warning", buttons, MessageBoxIcon.Exclamation);
                Close();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set the arguments of an existing application.
        /// </summary>
        /// <param name="arguments">Arguments of the application.</param>
        /// <param name="applicationIndex">Index of the application in the current applications list.</param>
        public void SetApplicationArgument(string arguments, int applicationIndex)
        {
            int i = listBox_appsList.SelectedIndex;

            if (applicationIndex == i && !appsList[i].applicationArguments.Equals(arguments))
            {
                appsList[i].applicationArguments = arguments;
                listBox_appsList.Items[i] = BuildAppListItem(appsList[i], applicationIndex);
                SaveApplicationsListToDisk();
            }
        }

        /// <summary>
        /// Add a list of applications fullPaths to the current list of applications.
        /// </summary>
        /// <param name="applicationsPath">List of applications fullPaths.</param>
        private int AddApplicationsToCurrentList(IEnumerable<string> applicationsPath)
        {
            ApplicationInfo[] applicationInfos = new ApplicationInfo[applicationsPath.Count()];
            for (int i = 0; i < applicationsPath.Count(); i++)
            {
                if(applicationsPath.ElementAt(i).Length > 0)
                    applicationInfos[i] = new ApplicationInfo(applicationsPath.ElementAt(i));
            }
            return AddApplicationsToCurrentList(applicationInfos);
        }

        /// <summary>
        /// Add a list of applications to the current list of applications.
        /// </summary>
        /// <param name="applicationInfos">List of applications data.</param>
        private int AddApplicationsToCurrentList(IEnumerable<ApplicationInfo> applicationInfos)
        {
            int applicationsAdded = 0;
            int index = appsList.Count();
            //string fileExtension;
            //string[] allowedExtensions = { "exe", "bat", "com" };

            for (int i = 0; i < applicationInfos.Count(); i++)
            {
                if (applicationInfos.ElementAt(i) == null)
                    continue;

                //fileExtension = applicationInfos.ElementAt(i).applicationPath.Split('.').Last().ToLower();
                //if (!allowedExtensions.Contains(fileExtension))
                  //  continue;

                if (AddApplicationToCurrentList(applicationInfos.ElementAt(i), index))
                {
                    index++;
                    applicationsAdded++;
                }
            }

            RefreshButtons();

            return applicationsAdded;
        }

        /// <summary>
        /// Add an application to the current list of applications.
        /// </summary>
        /// <param name="applicationInfo">The application data.</param>
        /// <param name="applicationIndex">Index of the application in the list.</param>
        private bool AddApplicationToCurrentList(ApplicationInfo applicationInfo, int applicationIndex)
        {
            if (applicationInfo == null)
                return false;

            if (!File.Exists(applicationInfo.applicationPath))
                return false;

            string listItem = BuildAppListItem(applicationInfo, applicationIndex);
            if (listItem != null) {
                listBox_appsList.Items.Add(listItem);
                appsList.Add(applicationInfo);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Build the text to be diplayed on the applications list of this form.
        /// </summary>
        /// <param name="applicationInfo">The application data.</param>
        /// <param name="applicationIndex">Index of the application in the list.</param>
        private string BuildAppListItem(ApplicationInfo applicationInfo, int applicationIndex)
        {
            string indexStr, fileName;

            applicationIndex++;

            if (applicationIndex < 10)
                indexStr = "0" + applicationIndex + " - ";
            else
                indexStr = "" + applicationIndex + " - ";
            string[] parts = applicationInfo.applicationPath.Split('\\');
            if (parts.Length > 0)
            {
                fileName = parts.Last();
                return (indexStr + fileName + " " + applicationInfo.applicationArguments);
            }
            return null;
        }

        /// <summary>
        /// Remove an application from the current list of applications.
        /// </summary>
        /// <param name="applicationIndex">Index of the application in the list.</param>
        private bool RemoveApplicationFromCurrentList(int applicationIndex)
        {
            if (applicationIndex < 0 || applicationIndex >= appsList.Count)
                return false;

            listBox_appsList.Items.RemoveAt(applicationIndex);
            appsList.RemoveAt(applicationIndex);
            Debug("APP removed.");

            RefreshButtons();

            if (appsList.Count != 0)
                SaveApplicationsListToDisk();

            return true;
        }

        /// <summary>
        /// Edit the arguments of an application.
        /// </summary>
        /// <param name="applicationIndex">Index of the application in the list.</param>
        private void EditApplicationArguments(int applicationIndex)
        {
            if (applicationIndex < 0 || applicationIndex >= appsList.Count)
                return;

            EditArgsForm editArgsForm = new EditArgsForm(this, appsList[applicationIndex].applicationArguments, applicationIndex);
            editArgsForm.Show();
            this.Enabled = false;
        }

        /// <summary>
        /// Refresh the state of the buttons of this form.
        /// </summary>
        private void RefreshButtons()
        {
            int i = listBox_appsList.SelectedIndex;

            if (appsList.Count > 0)
            {
                button_clearList.Enabled = true;
                button_launch.Enabled = true;

                if (i >= 0 && i < appsList.Count)
                {
                    button_removeApp.Enabled = true;
                    button_EditArgs.Enabled = true;
                }
                else
                {
                    button_removeApp.Enabled = false;
                    button_EditArgs.Enabled = false;
                }
            }
            else
            {
                button_clearList.Enabled = false;
                button_removeApp.Enabled = false;
                button_launch.Enabled = false;
                button_EditArgs.Enabled = false;
            }
        }

        /// <summary>
        /// Launch the applications extisting in the current list of applications.
        /// </summary>
        private void LaunchApplications()
        {
            for (int i = 0; i < appsList.Count; i++)
            {
                if (appsList[i] != null)
                {
                    try
                    {
                        listBox_appsList.SelectedIndex = i;
                        LaunchApplication(listBox_appsList.Items[i].ToString(), appsList[i].applicationPath, appsList[i].applicationArguments);
                        Thread.Sleep((int)delayNumericUpDown.Value);
                    }
                    catch(Exception ex) {
                        Debug(ex.Message);
                    }
                    finally { }
                }
            }
        }

        /// <summary>
        /// Launch the process of an application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="applicationPath">Full path of the application.</param>
        /// <param name="arguments">Arguments of the application.</param>
        private bool LaunchApplication(string applicationName, string applicationPath, string arguments)
        {
            if (!File.Exists(applicationPath))
            {
                Debug(applicationPath + " does not exist.");
                return false;
            }

            try
            {
                Debug("Launching: " + applicationPath + " " + arguments);

                string[] pathDirectories = applicationPath.Split('\\');
                string applicationDirectory = "";
                for (int i = 0; i < pathDirectories.Length-1; i++)
                {
                    applicationDirectory += pathDirectories[i] + '\\';
                }

                ProcessStartInfo startInfo = new ProcessStartInfo(applicationPath);
                startInfo.WorkingDirectory = applicationDirectory;
                startInfo.Arguments = arguments;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = false;
                startInfo.RedirectStandardError = false;

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                Debug(applicationName + " Launched.");
                return true;
            }
            catch (Exception ex)
            {
                Debug(applicationName + " Failed: " + ex.Message);

                // If not an executable maybe its a data file, so try to open with the default program
                Process fileOpener = new Process();
                fileOpener.StartInfo.FileName = "explorer";
                fileOpener.StartInfo.Arguments = applicationPath;
                fileOpener.Start();
            }
            finally
            {
                Debug(applicationName + " opened with default program.");
            }
            return false;
        }

        /// <summary>
        /// Load the list of applications from the disk.
        /// </summary>
        private bool LoadApplicationsListFromDisk()
        {
            if (File.Exists(appsListFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(appsListFilePath);
                    ApplicationInfo[] applicationInfos = new ApplicationInfo[lines.Length];
                    string[] pathAndArgs;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if(lines[i].Length > 0)
                        {
                            pathAndArgs = lines[i].Split('|');
                            if (pathAndArgs.Length > 0)
                            {
                                applicationInfos[i] = new ApplicationInfo(pathAndArgs[0]);
                                if (pathAndArgs.Length > 1)
                                    applicationInfos[i].applicationArguments = pathAndArgs[1];
                            }
                        }
                    }

                    AddApplicationsToCurrentList(applicationInfos);

                    Debug("APPs list loaded.");
                    return true;
                }
                catch (Exception ex)
                {
                    Debug(ex.Message);
                    MessageBox.Show("Unable to load the APPs list.");
                    return false;
                }
                finally
                {

                }
            }
            else
                Debug("APPs list file does not exist.");

            return false;
        }

        /// <summary>
        /// Save the list of applications to the disk.
        /// </summary>
        private bool SaveApplicationsListToDisk()
        {
            if (appsList.Count == 0)
                return false;

            try
            {
                List<string> lines = new List<string>();
                for (int i = 0; i < appsList.Count; i++)
                {
                    lines.Add(appsList[i].ToString());
                }

                File.WriteAllLines(appsListFilePath, lines);
                Debug("APPs list saved.");
                return true;
            }
            catch (Exception ex)
            {
                Debug(ex.Message);
                MessageBox.Show("Unable to save the APPs list.");
            }
            finally
            {

            }
            return false;
        }

        /// <summary>
        /// Load the settings of this process from the disk.
        /// </summary>
        private bool LoadSettingsFromDisk()
        {
            if (File.Exists(settingsFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(settingsFilePath);
                    string[] parameterAndValue;
                    string parameter, value;

                    bool launchOnStart = false;
                    bool startMinimized = false;
                    bool minimizeToTray = false;
                    float delay = 0;
                    int xCoord = this.DesktopLocation.X;
                    int yCoord = this.DesktopLocation.Y;
                    int width = this.Width;
                    int height = this.Height;
                    bool maximized = this.WindowState == FormWindowState.Maximized;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines.Length > 0)
                        {
                            parameterAndValue = lines[i].Split('=');
                            if (parameterAndValue.Length > 1)
                            {
                                parameter = parameterAndValue[0].Replace(" ", "");
                                value = parameterAndValue[1].Replace(" ", "");

                                if (parameter.Equals(launchOnStartupTag))
                                    bool.TryParse(value, out launchOnStart);
                                else if (parameter.Equals(startMinimizedTag))
                                    bool.TryParse(value, out startMinimized);
                                else if (parameter.Equals(minimizeToTrayTag))
                                    bool.TryParse(value, out minimizeToTray);
                                else if (parameter.Equals(delayTag))
                                    float.TryParse(value, out delay);
                                else if (parameter.Equals(xCoordTag))
                                    int.TryParse(value, out xCoord);
                                else if (parameter.Equals(yCoordTag))
                                    int.TryParse(value, out yCoord);
                                else if (parameter.Equals(widthTag))
                                    int.TryParse(value, out width);
                                else if (parameter.Equals(heightTag))
                                    int.TryParse(value, out height);
                                else if (parameter.Equals(maximizedTag))
                                    bool.TryParse(value, out maximized);
                            }
                        }
                    }

                    checkBox_launchOnStart.Checked = launchOnStart;
                    checkBox_StartMinimized.Checked = startMinimized;
                    checkBox_MinimizeToTray.Checked = minimizeToTray;
                    delayNumericUpDown.Value = (decimal)delay;
                    if (maximized)
                        this.WindowState = FormWindowState.Maximized;
                    else
                    {
                        this.Width = width;
                        this.Height = height;
                        this.StartPosition = FormStartPosition.Manual;
                        this.SetDesktopLocation(xCoord, yCoord);

                        if (checkBox_StartMinimized.Checked)
                            this.WindowState = FormWindowState.Minimized;
                    }

                    Debug("Application settings loaded.");
                    return true;
                }
                catch (Exception ex)
                {
                    Debug(ex.Message);
                    MessageBox.Show("Unable to load the application settings.");
                    return false;
                }
                finally
                {

                }
            }
            else
                Debug("Application settings ini file does not exist.");

            return false;
        }

        /// <summary>
        /// Save the settings of this process to the disk.
        /// </summary>
        private bool SaveSettingsToDisk()
        {
            try
            {
                bool maximized = this.WindowState == FormWindowState.Maximized;

                List<string> lines = new List<string>();
                string equal = " = ";

                lines.Add(launchOnStartupTag + equal + checkBox_launchOnStart.Checked.ToString());
                lines.Add(startMinimizedTag + equal + checkBox_StartMinimized.Checked.ToString());
                lines.Add(minimizeToTrayTag + equal + checkBox_MinimizeToTray.Checked.ToString());
                lines.Add(delayTag + equal + (int)delayNumericUpDown.Value);
                lines.Add(xCoordTag + equal + this.DesktopLocation.X);
                lines.Add(yCoordTag + equal + this.DesktopLocation.Y);
                lines.Add(widthTag + equal + this.Width);
                lines.Add(heightTag + equal + this.Height);
                lines.Add(maximizedTag + equal + maximized);

                File.WriteAllLines(settingsFilePath, lines);
                Debug("Application settings saved.");
                return true;
            }
            catch (Exception ex)
            {
                Debug(ex.Message);
                MessageBox.Show("Unable to save the application settings.");
            }
            finally
            {

            }
            return false;
        }

        /// <summary>
        /// Display a message on the status bar of this window form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        private void Debug(string message)
        {
            statusLabel.Text = message;
        }
    }
}
