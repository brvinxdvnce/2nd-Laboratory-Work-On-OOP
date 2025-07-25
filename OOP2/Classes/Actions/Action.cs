using OOP2.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.Classes.Ingredients;

namespace OOP2.Classes.Actions
{
    internal abstract class Action : IElement
    {
        public string name => this.GetType().Name;

        public List<IElement> content = new List<IElement>();

        public virtual void execute(int tabsCount = 0)
        {
            Console.WriteLine(new string(' ', tabsCount * 2) + "→ " + this.name);
            if (this.content != null)
                foreach (var t in this.content)
                {
                    t.execute(tabsCount + 1);
                }
        }
    }
}
