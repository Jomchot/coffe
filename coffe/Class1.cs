using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffe
{
    internal class ComboBoxMyItems
    {
        public int value { get; set; }
        public string name { get; set; }
        public ComboBoxMyItems(int value, string name) { 
            this.value = value;
            this.name = name;
        }
        public override string ToString() {
            return name;
        }
    }
}
