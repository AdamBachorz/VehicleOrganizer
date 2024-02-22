using VehicleOrganizer.Domain.Abstractions.Utils;

namespace VehicleOrganizer.Core
{
    public static class CommonPool
    {
        public static bool IsDebugMode = EnvUtils.IsDev();
    }
}
