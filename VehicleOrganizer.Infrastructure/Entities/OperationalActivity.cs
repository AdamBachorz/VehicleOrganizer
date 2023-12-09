using BachorzLibrary.Common.DbModel;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class OperationalActivity : Entity
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastOperationDate { get; set; } = DateTime.Now.Date;
        public int MileageWhenPerformed { get; set; }
        public bool IsDateOperated { get; set; }

        /// <summary>
        /// How many kilometers can car be driven to the next this type operation 
        /// </summary>
        public int MileageStep { get; set; }

        /// <summary>
        /// How many years can pass to the next this type operation 
        /// </summary>
        public int YearsStep { get; set; }

        /// <summary>
        /// Date when last reminder was send/announced
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? ReminderDate { get; set; }

        [NotMapped]
        public DateTime NextOperationDate => LastOperationDate.AddYears(YearsStep);
        [NotMapped]
        public int NextOperationAtMilage => MileageWhenPerformed + MileageStep;

        public int ToNextAct(DateTime referenceDate) => 
            IsDateOperated ? (int)(NextOperationDate - referenceDate).TotalDays : NextOperationAtMilage - Vehicle.LatestMileage;

        public int DaysAfterLastReminder(DateTime referenceDate) => ReminderDate.HasValue 
            ? (int)(referenceDate - ReminderDate.Value).TotalDays 
            : Codes.Defaults.DaysAboveWhichAnotherReminderCanBeSent + 1;

        public string SummaryPrompt(DateTime referenceDate, bool shortVersion = false) => 
            (!shortVersion ? $"{Name} - " : string.Empty) + $"Pozostało: {ToNextAct(referenceDate)} {(IsDateOperated ? "dni" : "kilometrów")}";
    }
}
