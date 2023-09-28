using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleOrganizer.Domain.Abstractions
{
    public static class Codes
    {
        public const string AppName = nameof(VehicleOrganizer);
        public static string MainPath = Path.Combine(@"C:\", Directories.Main, AppName);

        public struct Directories
        {
            public const string Main = "ABSolutions";
        }
        
        public struct Files
        {
            public static string DefaultUser = Path.Combine(MainPath, "DefaultUser.json");
        }


    }
}
