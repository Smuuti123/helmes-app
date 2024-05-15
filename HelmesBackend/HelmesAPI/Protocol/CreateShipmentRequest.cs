using HelmesAPI.Models;

namespace HelmesAPI.Protocol
{
    public class CreateShipmentRequest
    {
        public required string ShipmentNumber { get; set; } 
        public Airport KnownAirport { get; set; }

        public required string FlightNumber { get; set; }

        public DateTime FlightDate { get; set; }
        
    }
}