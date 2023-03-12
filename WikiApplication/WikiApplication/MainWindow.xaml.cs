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
        
        List<Information> Wiki= new List<Information>(); // Wiki List of type Information

        
        public MainWindow()
        {
            InitializeComponent();
        }
        // --- Methods --- //
        // Add Method
        private void Add()
        {
            // Combo box - Checks if user has selected an option that is an option or if they leave it empty
            if (cbCategory.SelectedItem != null && !string.IsNullOrEmpty(txtboxName.Text) && !string.IsNullOrEmpty(txtboxDef.Text))
            {
                // Instance of Information Class, I use this to access the public getters and setters in Information.cs
                Information newInformation = new Information();

                // checks what radio button is selected, sets the structure to selected option as a string
                if (rdoLinear.IsChecked == true)
                {
                    newInformation.structure = "Linear";
                }
                else if (rdoNonLinear.IsChecked == true)
                {
                    newInformation.structure = "Non-Linear";
                }
                else
                {
                    MessageBox.Show("Please select a Structure Option");
                }


                newInformation.category = cbCategory.SelectedItem.ToString()!; // "!" Tells the compiler that the expression cannot be null
                newInformation.name = txtboxName.Text;
                newInformation.definition = txtboxDef.Text;
                Wiki.Add(newInformation);
            }
            else
            {
                MessageBox.Show("Please make sure the information is filled out in its appropriate fields");
            }
           
        }
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
        private void SortandDisplay()
        {
            lvData.Items.Clear();
            Wiki.Sort();
           foreach(var item in Wiki)
            {
                lvData.Items.Add(item);
            }    
        }
        // Built-in Binary Search method
        // Clear Method (Reset all boxes and buttons)
        private void Clear()
        {
            txtboxName.Clear();
            txtboxDef.Clear();
            cbCategory.SelectedItem= null;
            rdoLinear.IsChecked = false;
            rdoNonLinear.IsChecked = false;
        }
        // Save Method
        // Load Method

        // --- Buttons & Events --- //
        // Add Button
        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            Add();
            SortandDisplay();
            Clear();
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
