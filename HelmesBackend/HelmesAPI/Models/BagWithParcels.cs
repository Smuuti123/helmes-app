using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAPI.Models
{
    public class BagWithParcels
    {
        public string? bagNumber { get; set; } //Check the comment in docs(liiga pikk)
        public List<Parcel> listOfParcels{ get; set; } = new List<Parcel>(); //List cannot be empty by the moment of dinalizing shipment
    }
}