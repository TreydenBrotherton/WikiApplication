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
        int index = -1;
        
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        // --- Methods --- //
        // Add Method
        private void Add()
        {
            // Creates a new object of Information
            Information newInformation = new Information();
            bool bothRdoChecked = true;
            // Checks if this object has linear or non linear radio button clicked, which button is clicked in Information.cs
            // This helps add to the functionality of when selecting an entry, the correct radio button is re-checked
            if (rdoLinear.IsChecked == true)
            {
                newInformation.isRadio1Checked = true;
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                newInformation.isRadio2Checked = true;
            }
            else
            {
                bothRdoChecked = false;
            }


            // Combo box - Checks if user has selected an option that is an option or if they leave it empty
            if (CheckIfInputsAreValid() && bothRdoChecked != false)
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
                MessageBox.Show("Found a duplicate");
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

            index = -1;
            if (rdoLinear.IsChecked == true)
            {
                index = 0;
            }
            else if (rdoNonLinear.IsChecked == true)
            {
                index = 1;
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
            object selectedItem = lvData.SelectedItem;
            Information newInformation = new Information();
            try
            {
                if (selectedItem != null)
                {
                    // Creates an object of the selected item
                    Information dataObject = (Information)selectedItem;

                    // resets both radio button checks to false
                    dataObject.isRadio1Checked = false;
                    dataObject.isRadio2Checked = false;

                    // sets the selected items variables
                    dataObject.name = txtboxName.Text;
                    dataObject.category = cbCategory.SelectedItem.ToString()!;
                    dataObject.definition = txtboxDef.Text;

                    // Checks for what radio button is clicked and stores it
                    CheckRadioButtonValue();
                    dataObject.rdoSelectedIndex = SelectedRadioButtonIndex();
                    dataObject.rdoSelectedType = SelectedRadioButton();

                    // gets stored value of which radio button is clicked
                    if (rdoLinear.IsChecked == true)
                    {
                        dataObject.isRadio1Checked = true;
                        if (dataObject.isRadio1Checked)
                        {
                            rdoLinear.IsChecked = true;
                        }
                        else if (dataObject.isRadio2Checked)
                        {
                            rdoNonLinear.IsChecked = true;
                        }
                    }
                    else if (rdoNonLinear.IsChecked == true)
                    {
                        dataObject.isRadio2Checked = true;
                    }
                }
            } catch (Exception)
            {
                MessageBox.Show("Exception Caught");
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
                cbCategory.SelectedItem != null)
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
        }
        // Window Loaded - When the application starts this Method is automatically ran
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadComboBox();
            
        }
        // Selected Entry Event - When you click on an entry in the list view, this will display its items back into its related controls
        private void Selected_Entry(object sender, SelectionChangedEventArgs e)
        {
            var wikiItems = (Information)((ListView)sender).SelectedItem;
           
            if (wikiItems != null)
            {
                txtboxName.Text = wikiItems.name;
                cbCategory.SelectedItem = wikiItems.category;
                txtboxDef.Text = wikiItems.definition;

                // Checks which radio button was checked on creation of object, checks the correct button when entry is selected
                if (wikiItems.isRadio1Checked)
                {
                    rdoLinear.IsChecked = true;
                }
                else if (wikiItems.isRadio2Checked)
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
            int selectedItem = lvData.SelectedIndex;
            if (selectedItem != -1 && CheckIfInputsAreValid())
            {
                EditEntry();
                Clear();
                SortandDisplay();
                
            }
            else
            {

                Clear();
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
