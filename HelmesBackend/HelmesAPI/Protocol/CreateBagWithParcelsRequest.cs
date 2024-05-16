using HelmesAPI.Models;

namespace HelmesAPI.Protocol
{
    public class CreateBagWithParcelsRequest
    {
        public string? BagNumber { get; set; }
        public List<Parcel> ListOfParcels{ get; set; } = new List<Parcel>();

    }
}