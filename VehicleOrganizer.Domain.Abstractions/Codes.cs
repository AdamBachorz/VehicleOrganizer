namespace VehicleOrganizer.Domain.Abstractions
{
    public static class Codes
    {
        public const string AppName = nameof(VehicleOrganizer);
        public static string MainPath = Path.Combine(@"C:\", Directories.Main, AppName, Directories.Data);

        public struct Defaults//
        {
            public const int DaysToRemind = 30;
            public const int MileageToRemind = 500;
        }

        public struct Directories
        {
            public const string Main = "ABSolutions";
            public const string Data = nameof(Data);
        }
        
        public struct Files
        {
            public static string DefaultUser = Path.Combine(MainPath, "DefaultUser.json");
            public static string DevConfig = Path.Combine(MainPath, "appsettings.Development.json");
            public static string ProdConfig = Path.Combine(MainPath, "appsettings.Production.json");
        }


    }
}
