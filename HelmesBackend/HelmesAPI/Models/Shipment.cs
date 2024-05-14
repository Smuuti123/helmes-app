using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAPI.Models
{
    public class Shipment
    {
        public int Id { get; set;}
        public string? ShipmentNumber { get; set; } //Format “XXX-XXXXXX”, where X – letter or digit. Must be unique within entire database
        public Airport KnownAirport { get; set; }
        public string? FlightDate { get; set; } //Format “LLNNNN”, where L – letter, N – digit
        public List<Parcel> ListOfParcels{ get; set; } = new List<Parcel>(); //List cannot be empty by the moment of dinalizing shipment
    }

    public enum Airport
    {
        TLL = 1,
        RIX = 2,
        HEL = 3
    }
}