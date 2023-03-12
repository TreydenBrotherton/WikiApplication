using System;
using System.Collections.Generic;
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
        // Search Button
        // Edit Button
        // Delete Button
        // Save Button
        // Load Button
        // ListView Selected Event
        // Double Click Name Event
    }
}
