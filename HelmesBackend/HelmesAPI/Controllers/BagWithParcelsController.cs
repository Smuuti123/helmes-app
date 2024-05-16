using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BagWithParcelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BagWithParcelsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> CreateBagWithParcels(BagWithParcels bag)
    {
        if(await _context.BagWithParcels.AnyAsync(b => b.BagNumber == bag.BagNumber))
        {
            return BadRequest("Bag number must be unique");
        }

        if(bag.ListOfParcels == null || !bag.ListOfParcels.Any())
        {
            return BadRequest("List of parcels cannot be empyt");
        }

        _context.BagWithParcels.Add(bag);

        try
        {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBagWithParcels), new { id = bag.Id }, bag);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "A database error occurred: " + ex.Message);
        }
    }

    // [HttpPost("{bagId}/Parcel/{parcelId}")]
    // private async Task<IActionResult> AddParcel()
    // {

    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<BagWithParcels>> GetBagWithParcels(int id)
    {
        var bagOfParcels = await _context.BagWithParcels.FindAsync(id);

        if(bagOfParcels == null)
        {
            return NotFound();
        }
        
        return bagOfParcels;
    }
}
