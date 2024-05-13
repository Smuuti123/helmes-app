using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAPI.Models
{
    public class Parcel
    {
        public string? parcelNumber { get; set; } //Format “LLNNNNNNLL”, Where L-letter, N-digit Must be unique whitin entire database
        public string? recipientName { get; set; }  //Max length 100 characters
        public string? destinationCountry { get; set; } //2-letters code, e.g. “EE”, “LV”, “FI”
        public decimal weight { get; set; } //Max 3 decimals allowed after comma
        public decimal price { get; set; } //Max 2 decimals allowed after comma
    }
}