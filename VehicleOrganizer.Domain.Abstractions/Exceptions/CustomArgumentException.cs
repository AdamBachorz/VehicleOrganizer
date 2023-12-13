namespace VehicleOrganizer.Domain.Abstractions.Exceptions
{
    public class CustomArgumentException : ArgumentException
    {
        public CustomArgumentException(string? message) : base(message)
        {
        }
    }
}
