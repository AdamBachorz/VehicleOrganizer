﻿using BachorzLibrary.Common.Extensions;

namespace VehicleOrganizer.DesktopApp.Extensions
{
    public static class ComboBoxExtensions
    {
        public static void LoadWithEnums<E>(this ComboBox comboBox, bool useEnumDescriptions, bool autoPickFirstItem = false) where E : Enum
        {
            comboBox.Items.Clear();
            foreach (Enum value in Enum.GetValues(typeof(E)))
            {
                comboBox.Items.Add(useEnumDescriptions && value.Description().HasValue() ? value.Description() : value);
            }

            comboBox.SelectedIndex = autoPickFirstItem ? 0 : -1;
        }
        
    }
}
