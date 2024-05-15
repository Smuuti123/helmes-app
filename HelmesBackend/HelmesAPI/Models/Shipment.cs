namespace HelmesAPI.Models
{
    public class Shipment
    {
        public int Id { get; set;}
        public bool IsFinalized { get; set;}
        public string? ShipmentNumber { get; set; } //Format “XXX-XXXXXX”, where X – letter or digit. Must be unique within entire database
        public Airport KnownAirport { get; set; }
        public string? FlightDate { get; set; } //Format “LLNNNN”, where L – letter, N – digit
        public List<BagWithParcels> BagsWithParcels { get; set; } = new List<BagWithParcels>();
        public List<BagWithLetters> BagsWithLetters { get; set; } = new List<BagWithLetters>();
        
    }

    public enum Airport
    {
        TLL = 1,
        RIX = 2,
        HEL = 3
    }
}