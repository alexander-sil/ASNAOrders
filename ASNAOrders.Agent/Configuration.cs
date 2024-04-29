using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Agent
{
    internal class Configuration
    {
        public static int LinesPerFile { get; set; }

        public static bool OneFile { get; set; }

        public static string DBConnectionString { get; set; } = "";

        public static string[] DBSelectedColumns { get; set; } = new string[0];

        public static string DatabaseQuery { get; set; } = "";

        public static string FTPServerAddress { get; set; } = "";

        public static string FTPServerUsername { get; set; } = "";

        public static string FTPServerPassword { get; set; } = "";

        public static string FTPServerPrefix { get; set; } = "";
    }
}
