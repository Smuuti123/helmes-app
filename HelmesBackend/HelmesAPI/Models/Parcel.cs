using HelmesAPI.Protocol;

namespace HelmesAPI.Models
{
    public class Parcel
    {
        public int Id { get; set;}
        public string? ParcelNumber { get; set; } 
        public string? RecipientName { get; set; }  
        public string? DestinationCountry { get; set; } 
        public decimal Weight { get; set; } 
        public decimal Price { get; set; }

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