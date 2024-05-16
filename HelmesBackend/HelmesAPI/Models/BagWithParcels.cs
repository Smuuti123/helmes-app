namespace HelmesAPI.Models

{
    public class BagWithParcels : Bag
    {
        public List<Parcel> ListOfParcels{ get; set; } = new List<Parcel>(); //List cannot be empty by the moment of dinalizing shipment
    }
}