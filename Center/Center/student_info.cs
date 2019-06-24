using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center
{
    public class student_info
    {
        public string name;
        public string Class;
        public string group;
        public string id;
        public Bitmap b;
        public student_info(string name, string Class, string group, string id, Bitmap b)
        {
            this.name = name;
            this.Class = Class;
            this.group = group;
            this.id = id;
            this.b = b;
        }
    }
}
