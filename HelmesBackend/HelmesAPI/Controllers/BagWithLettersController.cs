using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HelmesAPI.Protocol;
using System.Text.RegularExpressions;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BagWithLettersController : ControllerBase
{
    private readonly AppDbContext _context;

    public BagWithLettersController(AppDbContext context)
    {
        _context = context;
    }

    private IActionResult ValidateLetter(CreateBagWithLettersRequest letter)
    {
        if(!Regex.IsMatch(letter.BagNumber, @"^[A-Za-z0-9]{1,15}$"))
        {
            return BadRequest("Bag Number can only be 15 characters (No special symbols)");
        }
        if(letter.CountOfLetters < 1)
        {
            return BadRequest("Count of letters cant be zero");
        }
        if(letter.Price < 0 || letter.Weight < 0)
        {
            return BadRequest("Either price or weight cannot be negative");
        }
        //Weight max 3 decimals allowed after comma
        if((decimal)(Math.Round(letter.Weight * 1000) / 1000) != letter.Weight)
        {
            return BadRequest("Weight max 3 decimals allowed after comma");
        }
        //Price max 2 decimals allowed after comma
        if((decimal)(Math.Round(letter.Price * 100) / 100) != letter.Price)
        {
            return BadRequest("Price max 2 decimals allowed after comma");
        }
        return null;
    }

    [HttpPost("{shipmentId}")]
    public async Task<IActionResult> CreateBagWithLetters([FromRoute] int shipmentId, [FromBody] CreateBagWithLettersRequest createBagWithLettersRequest)
    {
        IActionResult validationResult = ValidateLetter(createBagWithLettersRequest);
        
        if (validationResult != null)
        {
            return validationResult;
        }

        var shipment = await _context.Shipments.FindAsync(shipmentId);
        if(shipment == null || shipment.Status == Status.FINALIZED)
        {
            return BadRequest("ALREADY FINALIZED");
        }

        if(await _context.BagWithLetters.AnyAsync(b => b.BagNumber == createBagWithLettersRequest.BagNumber))
        {
            return BadRequest("Bag number Must be unique");
        }
        BagWithLetters bag = new()
        {
            BagNumber = createBagWithLettersRequest.BagNumber,
            CountOfLetters = createBagWithLettersRequest.CountOfLetters,
            Weight = createBagWithLettersRequest.Weight,
            Price = createBagWithLettersRequest.Price
        };

        shipment.Bags.Add(bag);
        _context.BagWithLetters.Add(bag);

        try
        {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBagWithLetters), new { id = bag.Id }, bag);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "A database error occurred: " + ex.Message);
        }
    }

    [HttpGet("{id}")]       
    public async Task<ActionResult<BagWithLetters>> GetBagWithLetters(int id)
    {
        var bagOfLetters = await _context.BagWithLetters.FindAsync(id);

        if(bagOfLetters == null)
        {
            return NotFound();
        }

        return bagOfLetters;
    }
}
