namespace VehicleOrganizer.Domain.Abstractions.Utils
{
    public static class FileUtils
    {
        public static string GetProperFileByEnv(string devFile, string prodFile)
        {
#if DEBUG
            return devFile;
#else
            return prodFile;
#endif
        }
    }
}
