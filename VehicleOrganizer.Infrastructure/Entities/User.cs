using BachorzLibrary.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 

        public bool IsEmailOk => Email is not null ? Regex.IsMatch(Email, Consts.RegexPatterns.Email) : false;

        public static User Default => JsonConvert.DeserializeObject<User>(File.ReadAllText(Codes.Files.DefaultUser));
    }
}
