using BachorzLibrary.Common;
using BachorzLibrary.Common.DbModel;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Utils;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class User : Entity<Guid>
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public Details Details { get; set; }
        public string Email { get; set; }
        public string ReferenceCode { get; set; } = Codes.None;

        [NotMapped]
        [JsonIgnore]
        public bool IsEmailOk => Email is not null ? Regex.IsMatch(Email, Consts.RegexPatterns.Email) : false;
        
        [NotMapped]
        [JsonIgnore]
        public bool IsWorthy => ReferenceCode.Equals(Codes.ReferenceCode);
        
        [NotMapped]
        [JsonIgnore]
        public static User Default { get; set; }

        public static void RefreshData(User user) 
            => File.WriteAllText(EnvUtils.GetValueDependingOnEnvironment(Codes.Files.DefaultUser, Codes.Files.DefaultUserProd), JsonConvert.SerializeObject(user));
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
