using VehicleOrganizer.Domain.Abstractions.Enums;

namespace VehicleOrganizer.Domain.Abstractions.Extensions
{
    public static class VehicleTypeExtensions
    {
        public static bool IsOilBased(this VehicleType vehicleType) 
            => vehicleType == VehicleType.Car || vehicleType == VehicleType.Truck || vehicleType == VehicleType.Bus;
    }
}
