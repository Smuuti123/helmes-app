using HelmesAPI.Protocol;

namespace HelmesAPI.Models
{
    public class BagWithLetters : Bag
    {
    private CreateBagWithLettersRequest createBagWithLettersRequest;

    public BagWithLetters()
    {
      Type = "letters";
    }

    public BagWithLetters(CreateBagWithLettersRequest createBagWithLettersRequest)
    {
      this.createBagWithLettersRequest = createBagWithLettersRequest;
    }

    public int CountOfLetters { get; set; } 
    public decimal Weight { get; set; } 
    public decimal Price { get; set; } 
    }
}