using HelmesAPI.Protocol;

namespace HelmesAPI.Models

{
    public class BagWithParcels : Bag
    {
        public List<Parcel> ListOfParcels{ get; set; }

        public BagWithParcels() {
            ListOfParcels = new List<Parcel>();
        }

        public BagWithParcels(CreateBagWithParcelsRequest request) {
            BagNumber = request.BagNumber;
            ListOfParcels = new List<Parcel>();
            Type =  "parcel";
        }
    }
}