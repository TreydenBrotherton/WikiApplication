﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WikiApplication
{
    [Serializable]
    class Information : IComparable<Information>, IComparer<Information>, INotifyPropertyChanged
    {
        // variables
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        // Create separate setters and getters for each variable

        public string name
        {
            get { return Name; }
            set
            {
                if (Name != value)
                {
                    Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string category
        {
            get { return Category; }
            set
            {
                if (Category != value)
                {
                    Category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        public string structure
        {
            get { return Structure; }
            set { Structure = value; }
        }

        public string definition
        {
            get { return Definition; }
            set { Definition = value; }
        }

        public bool isLinear { get; set; }
        public int rdoSelectedIndex { get; set; }
        public string rdoSelectedType { get; set; }
        // Constructor 
        public Information(string name, string category, string structure, string definition)
        {
            this.Name = name;
            this.Category = category;
            this.Structure = structure;
            this.Definition = definition;
        }

        public Information()
        {
        }

        // overrides toString so when displaying items from Wiki, only name and category displays
        public override string ToString()
        {
            return Name + " --> " + Category;
        }
        // Uses IComparable Interface to implement the CompareTo method, sorts by Name
        public int CompareTo(Information other)
        {
            return Name.CompareTo(other.Name);
        }

        public int Compare(Information? x, Information? y)
        {
            return x.Name.CompareTo(y.Name);
        }

        // Used to display 
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
