using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAPI.Models
{
    public class BagWithLetters
    {
        public string? bagNumber { get; set; } //Vaata docsi
        public int countOfLetters { get; set; } //Cant be zero
        public decimal weight { get; set; } //Max 3 decimals allowed after comma
        public decimal price { get; set; } //Max 2 decimals allowed after comma
    }
}