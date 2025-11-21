using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhaneh
{
    internal class Detail
    {
        public Book B1;
        public Place P1;
        public string print()
        {
            return B1.print() + "," + P1.print();
        }
    }
}
