﻿namespace VehicleOrganizer.Domain.Abstractions
{
    public static class Codes
    {
        public const string AppName = nameof(VehicleOrganizer);
        public const string AdminEmail = "adam.bachorz1702@gmail.com";
        public static string MainPath = Path.Combine(@"C:\", Directories.Main, AppName, Directories.Data);

        public struct Defaults
        {
            public const int DaysToRemindAboutActivity = 30;
            public const int DaysToRemindAboutInsuranceTermination = 30;
            public const int DaysToRemindAboutTechnicalReview = 30;
            public const int MileageToRemindAboutActivity = 500;
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
