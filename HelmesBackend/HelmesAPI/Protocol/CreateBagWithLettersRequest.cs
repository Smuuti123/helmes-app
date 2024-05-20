namespace HelmesAPI.Protocol
{
    public class CreateBagWithLettersRequest
    {
        public string? BagNumber { get; set; }
        public int CountOfLetters { get; set; }
        public decimal Weight { get; set; } 
        public decimal Price { get; set; }
    }
}