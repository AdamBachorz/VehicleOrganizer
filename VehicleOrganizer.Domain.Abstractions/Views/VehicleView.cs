﻿namespace VehicleOrganizer.Domain.Abstractions.Views
{
    public class VehicleView
    {
        public int Reference { get; set; }
        public string Name { get; set; }
        public string YearOfProduction { get; set; }
        public string VehicleType { get; set; }
        public bool IsOilBased { get; set; }
        public string OilType { get; set; }
        public string PurchaseDate { get; set; }
        public string RegistrationDate { get; set; }
        public string InsuranceConclusion { get; set; }
        public string InsuranceTermination { get; set; }
        public string LastTechnicalReview { get; set; }
        public string NextTechnicalReview { get; set; }
        public string LatestMileage { get; set; }
        public string DaysToInsuranceExpires { get; set; }
        public string DaysToNextTechnicalReview { get; set; }
    }
}
