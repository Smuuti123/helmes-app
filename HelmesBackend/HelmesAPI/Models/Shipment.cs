using System.Diagnostics.CodeAnalysis;
using HelmesAPI.Protocol;

namespace HelmesAPI.Models
{
    public class Shipment
    {
        public int Id { get; set;}
        public required string ShipmentNumber { get; set; } //Format “XXX-XXXXXX”, where X – letter or digit. Must be unique within entire database
        public Airport Airport { get; set; } 
        public required string FlightNumber { get; set; } //Format “LLNNNN”, where L – letter, N – digit
        public DateTime FlightDate { get; set; } 
        public Status Status { get; set; }
        public List<Bag> Bags { get; set; } = new List<Bag>();

        public Shipment()
        {}

        [SetsRequiredMembers]
        public Shipment(CreateShipmentRequest CreateShipmentRequest) 
        {
            ShipmentNumber = CreateShipmentRequest.ShipmentNumber;
            Airport = CreateShipmentRequest.KnownAirport;
            FlightDate = CreateShipmentRequest.FlightDate;
            FlightNumber = CreateShipmentRequest.FlightNumber;
            Status = Status.CREATED;
        }
        
    }

    public enum Airport
    {
        TLL = 1,
        RIX = 2,
        HEL = 3
    }

    public enum Status 
    {
        CREATED = 1,
        FINALIZED = 2
    }
}