using BachorzLibrary.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        [NotMapped]
        public Details Details { get; set; } 
        public string Email { get; set; }

        [NotMapped]
        public bool IsEmailOk => Email is not null ? Regex.IsMatch(Email, Consts.RegexPatterns.Email) : false;
        [NotMapped]
        public static User Default => JsonConvert.DeserializeObject<User>(File.ReadAllText(Codes.Files.DefaultUser));
    }

    public struct Details
    {
        public Details()
        {
        }

        public string FirstName { get; set; } = "I";
        public string LastName { get; set; } = "N";
        public string IdNumber { get; set; } = "nr";
        public string Address { get; set; } = "A";
        public string PostalCode { get; set; } = "KP";
        public string City { get; set; } = "M";
    }
}
