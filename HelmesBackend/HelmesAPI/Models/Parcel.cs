using HelmesAPI.Protocol;

namespace HelmesAPI.Models
{
    public class Parcel
    {
        public int Id { get; set;}
        public string? ParcelNumber { get; set; } //Format “LLNNNNNNLL”, Where L-letter, N-digit Must be unique whitin entire database
        public string? RecipientName { get; set; }  //Max length 100 characters
        public string? DestinationCountry { get; set; } //2-letters code, e.g. “EE”, “LV”, “FI”
        public decimal Weight { get; set; } //Max 3 decimals allowed after comma
        public decimal Price { get; set; } //Max 2 decimals allowed after comma

        public Parcel()
        {

        }
        
        public Parcel(CreateParcelRequest CreateParcelRequest)
        {
            this.ParcelNumber = CreateParcelRequest.ParcelNumber;
            this.RecipientName = CreateParcelRequest.RecipientName;
            this.DestinationCountry = CreateParcelRequest.DestinationCountry;
            this.Weight = CreateParcelRequest.Weight;
            this.Price = CreateParcelRequest.Price;
        }
    }
}