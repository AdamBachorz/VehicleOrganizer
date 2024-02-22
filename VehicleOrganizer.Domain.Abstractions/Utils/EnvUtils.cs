namespace VehicleOrganizer.Domain.Abstractions.Utils
{
    public static class EnvUtils
    {
        public static bool IsDev()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static bool IsProduction() => !IsDev();

        public static void PerformDependingOnEnvironment(Action onDev, Action onProd)
        {
            if (IsDev())
            {
                onDev();
            }
            else
            {
                onProd();
            }
        }

        public static async Task PerformDependingOnEnvironmentAsync(Func<Task> onDev, Func<Task> onProd)
        {
            if (IsDev())
            {
                await onDev();
            }
            else
            {
                await onProd();
            }
        }

        public static V GetValueDependingOnEnvironment<V>(V devValue, V prodValue) => IsDev() ? devValue : prodValue;
    }
}
