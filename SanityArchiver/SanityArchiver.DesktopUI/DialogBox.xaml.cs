using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SDKSample
{
    /// <summary>
    /// Dialog box
    /// </summary>
    public partial class DialogBox : Window
    {
        /// <summary>
        /// Dialog box
        /// </summary>
        public DialogBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the new filename
        /// </summary>
        public string FileName => FileNameField.Text;

        /// <summary>
        /// Gets the new file extension
        /// </summary>
        public string Extension => ExtensionField.Text;

        /// <summary>
        /// Gets the new visibility property.
        /// </summary>
        public string Visibility => VisibilityField.Text;

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}