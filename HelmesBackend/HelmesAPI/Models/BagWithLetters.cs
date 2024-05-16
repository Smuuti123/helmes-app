using HelmesAPI.Protocol;

namespace HelmesAPI.Models
{
    public class BagWithLetters : Bag
    {
        public BagWithLetters()
        {
        }

        public int CountOfLetters { get; set; } //Cant be zero
        public decimal Weight { get; set; } //Max 3 decimals allowed after comma
        public decimal Price { get; set; } //Max 2 decimals allowed after comma
    }
}