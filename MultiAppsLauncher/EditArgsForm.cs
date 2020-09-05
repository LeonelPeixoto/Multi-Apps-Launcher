using System;
using System.Windows.Forms;

namespace MultiAppsLauncher
{
    public partial class EditArgsForm : Form
    {
        /// <summary>
        /// The Main Form that calls this form.
        /// </summary>
        private MainForm parentForm;

        private string arguments;
        private int applicationIndex;


        // ----------------------------- Form Functions -----------------------------------------

        /// <summary>
        /// Edit arguments from constructor.
        /// </summary>
        /// <param name="parentForm">Object that send the event.</param>
        /// <param name="arguments">Existing arguments of the application.</param>
        /// <param name="applicationIndex">Index of the application.</param>
        public EditArgsForm(MainForm parentForm, string arguments, int applicationIndex)
        {
            InitializeComponent();
            Hide();
            this.parentForm = parentForm;
            this.arguments = arguments;
            this.applicationIndex = applicationIndex;

            arguments_textBox.Text = this.arguments;

            this.StartPosition = FormStartPosition.Manual;
            this.SetDesktopLocation(this.parentForm.DesktopLocation.X + 10, this.parentForm.DesktopLocation.Y + 40);
        }

        /// <summary>
        /// From closing event.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void EditArgsForm_FormClosing(object sender, EventArgs e)
        {
            Hide();
            parentForm.Enabled = true;
            parentForm.Focus();
        }

        // ----------------------------- Events -----------------------------------------

        /// <summary>
        /// Single click event of the OK button.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void OK_button_Click(object sender, EventArgs e)
        {
            arguments = arguments_textBox.Text;
            parentForm.SetApplicationArgument(arguments, applicationIndex);
            Close();
        }

        /// <summary>
        /// Single click event of the Cancel button.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Change the icon of the Drag & Drop event.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void EditArgsForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                if(((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
                    e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Use a file as an argument with the Drag & Drop event.
        /// </summary>
        /// <param name="sender">Object that send the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void EditArgsForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            arguments_textBox.Text = files[0];
        }
    }
}
