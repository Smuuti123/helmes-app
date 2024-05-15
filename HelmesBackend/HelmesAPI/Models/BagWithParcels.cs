namespace HelmesAPI.Models
{
    public class BagWithParcels
    {
        public int Id { get; set;}
        public string? BagNumber { get; set; } //Check the comment in docs(liiga pikk)
        public List<Parcel> ListOfParcels{ get; set; } = new List<Parcel>(); //List cannot be empty by the moment of dinalizing shipment
    }
}