using System.ComponentModel;

namespace VehicleOrganizer.Domain.Abstractions.Enums
{
    public enum VehicleType
    {
        [Description("Samochód")]
        Car,

        [Description("Ciężarówka")]
        Truck,

        [Description("Autobus")]
        Bus,

        [Description("Motocykl")]
        Motorcycle,
        
        [Description("Motorower")]
        Scooter,

        [Description("Przyczepa")]
        Trailer,

        [Description("Inne")]
        Other,
    }
}
