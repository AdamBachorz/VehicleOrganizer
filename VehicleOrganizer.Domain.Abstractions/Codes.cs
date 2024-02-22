using VehicleOrganizer.Domain.Abstractions.Utils;

namespace VehicleOrganizer.Domain.Abstractions
{
    public static class Codes
    {
        public const string AppName = nameof(VehicleOrganizer);
        public const string AdminEmail = "adam.bachorz1702@gmail.com";
        public static string MainPath = Path.Combine(@"C:\", Directories.Main, AppName);
        public const string ReferenceCode = "b32ba5a7-9812-4bac-9c37-005c86c176e2";
        public const string None = "N/A";
        public const string VehicleSoldIndicator = "[SPRZEDANY]";

        public struct Defaults
        {
            public const int DaysToRemindAboutActivity = 30;
            public const int DaysToRemindAboutInsuranceTermination = 30;
            public const int DaysToRemindAboutTechnicalReview = 30;
            public const int DaysAboveWhichAnotherReminderCanBeSent = 7;
            public const int MileageToRemindAboutActivity = 500;
        }

        public struct Icos
        {
            public const string Edit = "✎";
            public const string Delete = "🗑";
        }

        public struct Directories
        {
            public static string EnvSubdirectory = EnvUtils.GetValueDependingOnEnvironment(Dev, Prod);
            public const string Main = "ABSolutions";
            public const string Dev = nameof(Dev);
            public const string Prod = nameof(Prod);
            public const string Data = nameof(Data);
            public const string Exceptions = nameof(Exceptions);
        }
        
        public struct Files
        {
            public static string DefaultUser = Path.Combine(MainPath, Directories.Data, "DefaultUser.json");
            public static string DefaultUserProd = Path.Combine(MainPath, Directories.Data, "DefaultUserProd.json");
            public static string DevConfig = Path.Combine(MainPath, Directories.Data, "appsettings.Development.json"); // use "PostDevelopment" name to check before prod
            public static string ProdConfig = Path.Combine(MainPath, Directories.Data, "appsettings.Production.json");
        }


    }
}
