using OOP2.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.Classes.Ingredients
{
    internal abstract class Ingredient : IElement
    {
        private int _netMass;
        public string name => this.GetType().Name;

        public int netMass {
            get { return _netMass; }
            set { _netMass = Math.Max(1, value); }
        }

        public void execute(int tabsCount = 0)
        {
            Console.WriteLine(new string(' ', tabsCount * 2) + " " + this.name + $"({this.netMass})");
        }
    }
}
