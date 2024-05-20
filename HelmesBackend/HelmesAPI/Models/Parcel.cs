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
            ParcelNumber = CreateParcelRequest.ParcelNumber;
            RecipientName = CreateParcelRequest.RecipientName;
            DestinationCountry = CreateParcelRequest.DestinationCountry;
            Weight = CreateParcelRequest.Weight;
            Price = CreateParcelRequest.Price;
        }
    }
}