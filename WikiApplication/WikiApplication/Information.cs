using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApplication
{
    class Information : IComparable<Information>
    {
        private string Name { get; set; }
        private string Category { get; set; }
        private string Structure { get; set; }
        private string Definition { get; set; }

        // Constructor 
        public Information(string name, string category, string structure, string definition)
        {
            this.Name = name;
            this.Category = category;
            this.Structure = structure;
            this.Definition = definition;
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
