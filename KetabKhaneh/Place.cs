using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhaneh
{
    internal class Place
    {
        public long Column { get; set; }
        public long Line { get; set; }
        public int BookId { get; set; }
        public string print()
        {
            return "ستون:" + Column + "," + "سطر:" + Line;
        }
    }
}
