using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.Classes.Interfaces
{
    internal interface IElement
    {
        string name { get; }
        void execute(int tabsCount = 0);
    }
}