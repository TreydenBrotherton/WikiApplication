﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            Wiki.Add(new Information("Array", "Array", "C", "D"));
            Wiki.Sort();
            foreach (var item in Wiki) 
            {
                lvData.Items.Add(item);
            }
        }
    }
}
