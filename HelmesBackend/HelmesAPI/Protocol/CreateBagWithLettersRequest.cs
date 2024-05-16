using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelmesAPI.Protocol
{
    public class CreateBagWithLettersRequest
    {
        public string? BagNumber { get; set; } //Vaata docsi
        public int CountOfLetters { get; set; } //Cant be zero
        public decimal Weight { get; set; } //Max 3 decimals allowed after comma
        public decimal Price { get; set; } //Max 2 decimals allowed after comma
    }
}