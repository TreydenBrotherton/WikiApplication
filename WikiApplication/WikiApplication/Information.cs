using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApplication
{
    class Information : IComparable<Information>
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
            set { Name = value; }
        }

        public string category
        {
            get { return Category; }
            set { Category = value; }
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
        public bool isRadio1Checked { get; set; }
        public bool isRadio2Checked { get; set; }
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
            return Name + "\t\t\t" + Category;
        }

        // Uses IComparable Interface to implement the CompareTo method, sorts by Name
        public int CompareTo(Information other)
        {
            return Name.CompareTo(other.Name);
        }

    }
}
