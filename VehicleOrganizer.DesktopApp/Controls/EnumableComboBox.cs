using System.ComponentModel;

namespace VehicleOrganizer.DesktopApp.Controls
{
    public class EnumableComboBox<E> : ComboBox where E : Enum
    {
        //public E[] EnumLookup { get; set; } = Enum.GetValues<E>();
    }
}
