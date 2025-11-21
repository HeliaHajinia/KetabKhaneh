using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhaneh
{
    internal class Book
    {
        public string Title { get; set; }
        public string Bookmarker { get; set; }
        public string Year { get; set; }
        public int id { get; set; }
        public string print()
        {
            return "نام کتاب:" + Title + "," + "نویسنده:" + Bookmarker + "," + "سال انتشار:" + Year;
        }
    }
}
