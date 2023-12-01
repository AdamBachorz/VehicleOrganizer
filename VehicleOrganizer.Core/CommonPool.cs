using BachorzLibrary.Common.Utils;

namespace VehicleOrganizer.Core
{
    public static class CommonPool
    {
        public static bool IsDebugMode = EnvUtils.GetValueDependingOnEnvironment(true, false);
    }
}
