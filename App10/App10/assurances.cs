using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App10
{
    public class Assurances
    {
        public string Name { get; set; }
        public string Number { get; set; }
        static public ObservableCollection<Assurances> Items = new ObservableCollection<Assurances>();
        
        public Assurances(string name, string number)
        {
             
            this.Name = name;
            this.Number = number;

        }
       
       
    }
}
