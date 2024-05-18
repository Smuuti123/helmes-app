using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HelmesAPI.Protocol;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;


namespace HelmesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BagWithParcelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BagWithParcelsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("Shipment/{shipmentId}")]
    public async Task<IActionResult> CreateBagWithParcels(CreateBagWithParcelsRequest request, int shipmentId)
    {
        var shipment = await _context.Shipments.FindAsync(shipmentId);
        if(shipment == null || shipment.Status == Status.FINALIZED)
        {
            return BadRequest("ALREADY FINALIZED");
        }

        if(await _context.BagWithParcels.AnyAsync(b => b.BagNumber == request.BagNumber))
        {
            return BadRequest("Bag number must be unique");
        }


        BagWithParcels bag = new(request);
        shipment.Bags.Add(bag);

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

    [HttpPost("{bagId}/AddParcel")]
    public async Task<IActionResult> AddParcelToBag(int bagId, CreateParcelRequest createParcelRequest)
    {
        var bag = await _context.BagWithParcels.Include(b => b.ListOfParcels).FirstOrDefaultAsync(b => b.Id == bagId);

        if(bag ==  null)
        {
            return NotFound("Bag not found");
        }

        var shipment = await _context.Shipments.Include(s => s.Bags).FirstOrDefaultAsync(s => s.Bags.Any(b => b.Id == bagId));
        if(shipment == null || shipment.Status == Status.FINALIZED)
        {
            return BadRequest("Shipment is already finalized");
        }

        IActionResult validationResult = ValidateParcel(createParcelRequest);
        if(validationResult != null)
        {
            return validationResult;
        }

        if(await _context.Parcels.AnyAsync(p => p.ParcelNumber == createParcelRequest.ParcelNumber))
        {
            return BadRequest("Parcel with same Parcel number already exists");
        }

        Parcel parcel = new(createParcelRequest);
        bag.ListOfParcels.Add(parcel);

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "An error occurred while adding the parcel to the bag: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BagWithParcels>> GetBagWithParcels(int id)
    {
        var bagOfParcels = await _context.BagWithParcels.Include(b => b.ListOfParcels).FirstOrDefaultAsync(b => b.Id == id);

        if(bagOfParcels == null)
        {
            return NotFound();
        }
        
        return bagOfParcels;
    }

    private IActionResult ValidateParcel(CreateParcelRequest parcel)
    {
         //Setting up the format
        if(!Regex.IsMatch(parcel.ParcelNumber, @"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$"))
        {
            return BadRequest("Parcel number must be in format “LLNNNNNNLL”, where L is letter, N is digit");
        }
        //Adding max lenght for the name
        if(parcel.RecipientName.Length > 100)
        {
            return BadRequest("Name cannot be more than 100 characters");
        }
        //Destination can only be 2-letters code, e.g. “EE”, “LV”, “FI”
        if(parcel.DestinationCountry.Length != 2 || !Regex.IsMatch(parcel.DestinationCountry, @"^[A-Z]{2}$"))
        {
            return BadRequest("Destination can only type in, using 2 letters code, e.g. (EE, LV, FI)");
        }
        //Both of values cannot be negative
        if(parcel.Price < 0 || parcel.Weight < 0)
        {
            return BadRequest("Either price or weight cannot be negative");
        }
        //Weight max 3 decimals allowed after comma
        if((decimal)(Math.Round(parcel.Weight * 1000) / 1000) != parcel.Weight)
        {
            return BadRequest("Weight max 3 decimals allowed after comma");
        }
        //Price max 2 decimals allowed after comma
        if((decimal)(Math.Round(parcel.Price * 100) / 100) != parcel.Price)
        {
            return BadRequest("Price max 2 decimals allowed after comma");
        }
        return null;
    }
}
