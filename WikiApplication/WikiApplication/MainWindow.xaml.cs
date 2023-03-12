using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WikiApplication
{
    
    public partial class MainWindow : Window
    {   
        
        List<Information> Wiki= new List<Information>();
        Information newInformation = new Information();
        public MainWindow()
        {
            InitializeComponent();
        }
        // --- Methods --- //
        // Add Method
        // ComboBox on Form Load Method 
        private void loadComboBox()
        {
            try
            {
                string[] options = File.ReadAllLines("Options.txt"); // Reads the content of Options.txt into an string array

                foreach (string option in options) // Adds each item in Options.txt to the combo box
                {
                    cbCategory.Items.Add(option);
                }
            }
            catch (FileNotFoundException) // If file does not exist, shows error, closes application
            {
                MessageBox.Show("FileNotFoundException Caught.. Closing application");
                Close();
            }
            catch (Exception) // catches any other exception, shows error, closes application
            {
                MessageBox.Show("Exception Caught.. Closing Application");
                Close();
            }
        }
        // Valid Name Method
        // Group Box Methods (Highlight and return)
        // Delete Method
        // Edit Method
        // Sort and Display Method
        // Built-in Binary Search method
        // Clear Method (Reset all boxes and buttons)
        // Save Method
        // Load Method

        // --- Buttons & Events --- //
        // Add Button
        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            
        }
        // Window Loaded - When the application starts this Method is automatically ran
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadComboBox();
        }
        // Search Button
        // Edit Button
        // Delete Button
        // Save Button
        // Load Button
        // ListView Selected Event
        // Double Click Name Event
    }
}
