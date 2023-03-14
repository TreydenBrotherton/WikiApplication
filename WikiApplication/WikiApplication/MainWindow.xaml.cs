using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        int index;
        
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        // --- Methods --- //
        // Check if both Radio buttons are valid
        private bool areBothRadioButtonsValid()
        {
            return rdoLinear.IsChecked == true || rdoNonLinear.IsChecked == true;
        }
        // Add Method
        private void Add()
        {
            // Creates a new object of Information
            Information newInformation = new Information();
          
            // Checks if this object has linear or non linear radio button clicked, which button is clicked in Information.cs
            // This helps add to the functionality of when selecting an entry, the correct radio button is re-checked
            if (rdoLinear.IsChecked == true)
            {
                newInformation.isLinear = true;
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                newInformation.isLinear = false;
            }

            // Combo box - Checks if user has selected an option that is an option or if they leave it empty
            if (CheckIfInputsAreValid())
            {
                // Checks the currently selected Radio button Index and Type (Linear/Non-Linear), assigns each variable to this object
                CheckRadioButtonValue();
                newInformation.rdoSelectedIndex = SelectedRadioButtonIndex();
                newInformation.rdoSelectedType = SelectedRadioButton();
                

                // Gets the attributes from Information.cs and adds the users input to related attribute
                newInformation.category = cbCategory.SelectedItem.ToString()!; // "!" Tells the compiler that the expression cannot be null
                newInformation.name = txtboxName.Text;
                newInformation.definition = txtboxDef.Text;

                // Adds the object to the Wiki List
                Wiki.Add(newInformation);
            }
            else
            {
                MessageBox.Show("You are missing some information. Please fill out all fields");
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
        // Valid Name Method (Changed the name to IsNameDuplciate as it makes more sense to me)
        private bool IsNameDuplciate()
        {
            // returns true if theres an entry with the same name in Wiki, returns false if its not a duplciate
            bool isDuplicate = Wiki.Exists(e => e.name.Equals(txtboxName.Text));

            if (isDuplicate)
            {
                SortandDisplay();
                Clear();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Group Box Methods (Highlight and return) // This will be 3 methods, one method will call the other two methods
        // Gets the structure of the selected radio button
        private string SelectedRadioButton()
        {
            if (rdoLinear.IsChecked == true)
            {
                return "Linear Structure";
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                return "Non-Linear Structure";
            }
            else
            {
                return "";
            }
        }
        // Gets the index of the currently selected Radio Button
        private int SelectedRadioButtonIndex()
        {
            if (rdoLinear.IsChecked == true)
            {
                index = 0;
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                index = 1;
            }
            else
            {
                index = -1;
            }
            return index;
         }
        // Method to call the other two methods
        private void CheckRadioButtonValue()
        {
            SelectedRadioButton();
            SelectedRadioButtonIndex();
        }
        // Delete Method
        private void DeleteEntry()
        {
            object selectedItem = lvData.SelectedItem;
            // Checks if user has selected an item or not
            if (selectedItem != null)
            {
                // Creates a message box to show when user has selected an item and has clicked delete
                MessageBoxResult userResult = MessageBox.Show("Are you sure you want to delete this entry?", "Delete", MessageBoxButton.YesNo);
                // if user clicks Yes, deletes item, if user clicks no then it cancels the action
                if (userResult == MessageBoxResult.Yes)
                {
                    lvData.Items.Remove(selectedItem);
                    Wiki.Remove((Information)selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an entry to delete");
            }
        }

        // Edit Method
        private void EditEntry()
        {
            int selectedIndex = lvData.SelectedIndex;
           
            if (selectedIndex != -1)
            {
                // Get the data object for the selected item
                Information dataObject = (Information)lvData.SelectedItem;

                // Update the data object's properties
                dataObject.name = txtboxName.Text;
                dataObject.category = cbCategory.SelectedItem.ToString()!;
                dataObject.definition = txtboxDef.Text;
                dataObject.rdoSelectedIndex = SelectedRadioButtonIndex();
                dataObject.rdoSelectedType = SelectedRadioButton();

                // Update the radio button values
                if(rdoLinear.IsChecked == true)
                {
                    dataObject.isLinear = true;
                }
                else 
                {
                    dataObject.isLinear = false;
                }
            }
            else
            {
                Clear();
            }
        }
       
       
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
            txtboxSearchInput.Clear();
            cbCategory.SelectedItem= null;
            rdoLinear.IsChecked = false;
            rdoNonLinear.IsChecked = false;
        }
        // Save Method
        // Load Method
        // Check if all input boxes are valid or not
        private bool CheckIfInputsAreValid()
        {
            if (!string.IsNullOrEmpty(txtboxName.Text) && !string.IsNullOrEmpty(txtboxDef.Text) &&
                cbCategory.SelectedIndex >= 0 && areBothRadioButtonsValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // --- Buttons & Events --- //

        // Add Button
        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            // if IsDuplciate returns false adds entry to Wiki, sorts and then displays
            if (IsNameDuplciate() == false)
            {
                Add();
                SortandDisplay();
                Clear();
            }
            else
            {
                MessageBox.Show("Found a duplicate");
            }
        }
        // Window Loaded - When the application starts this Method is automatically ran
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadComboBox();
            
        }
        // Selected Entry Event - When you click on an entry in the list view, this will display its items back into its related controls
        private void Selected_Entry(object sender, SelectionChangedEventArgs e)
        {

            Information selectedItem = (Information)lvData.SelectedItem;
            if (selectedItem != null)
            {
                txtboxName.Text = selectedItem.name;
                txtboxDef.Text = selectedItem.definition;
                cbCategory.SelectedItem = selectedItem.category;

                if(selectedItem.isLinear == true)
                {
                    rdoLinear.IsChecked = true;
                }
                else
                {
                    rdoNonLinear.IsChecked = true;
                }
            }
            
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteEntry();
            Clear();
            SortandDisplay();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!IsNameDuplciate() && CheckIfInputsAreValid())
            {
                EditEntry();
                Clear();
                SortandDisplay();
            }
            else
            {
                MessageBox.Show("Found an error whilst trying to edit this item, try again");
                Clear();
                SortandDisplay();
            }
           
            
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
