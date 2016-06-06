using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.Utils
{
    class Logging
    {
        public static void Output(string s)
        {
            using (var streamWriter = new StreamWriter("c://temp/myfileCommands.txt", true))
            {
                streamWriter.WriteLine(s);
            }
        }
    }
}
