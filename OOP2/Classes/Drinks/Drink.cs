using OOP2.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.Classes.Drinks
{
    internal class Drink
    {
        public OOP2.Classes.Actions.Action rootNode;

        public string name;

        public void print()
        {
            Console.WriteLine(this.name);
            rootNode.execute();
            Console.WriteLine();
        }
    }
}
