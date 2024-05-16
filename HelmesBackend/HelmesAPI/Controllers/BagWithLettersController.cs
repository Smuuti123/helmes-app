using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
    [HttpPost("{shipmentId}")]
    public async Task<IActionResult> CreateBagWithLetters(int shipmentId, BagWithLetters bag)
    {
        var shipment = await _context.Shipments.FindAsync(shipmentId);
        if(shipment == null || shipment.Status == Status.FINALIZED)
        {
            return BadRequest("ALREADY FINALIZED");
        }

        if(await _context.BagWithLetters.AnyAsync(b => b.BagNumber == bag.BagNumber))
        {
            return BadRequest("Bag number Must be unique");
        }
        if(bag.CountOfLetters < 0)
        {
            return BadRequest("Bag cannot be empty");
        }

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
