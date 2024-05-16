using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelmesAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using HelmesAPI.Protocol;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelController : ControllerBase
{
    private readonly AppDbContext _context;
    public ParcelController(AppDbContext context)
    {
        _context = context;
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

    [HttpPost] //Adding parcel
    public async Task<IActionResult> CreateParcel(CreateParcelRequest  createParcelRequest)
    {
        IActionResult validationResult = ValidateParcel(createParcelRequest);
        if(validationResult != null)
        {
            return validationResult;
        }
        //Checking if there is a paricel with the same Parcel number
        if(await _context.Parcels.AnyAsync(p => p.ParcelNumber == createParcelRequest.ParcelNumber))
        {
            return BadRequest("Parcel with the same parcel number already exists");
        }

        Parcel parcel = new(createParcelRequest);

        _context.Parcels.Add(parcel);
         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateException)
         {
            return StatusCode(500, "Error occured while saving the parcel");
         }

        return CreatedAtAction(nameof(GetParcel), new {id =  parcel.Id}, parcel);
    }

    //Find parcels, using id
    [HttpGet("{id}")]
    public async Task<ActionResult<Parcel>> GetParcel(int id)
    {
        var parcel = await _context.Parcels.FindAsync(id);

        if(parcel == null)
        {
            return NotFound();
        }
        
        return parcel;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParcel(int id, Parcel updatedParcel)
    {



        var parcel = await _context.Parcels.FindAsync(id);
        if(parcel == null)
        {
            return NotFound();
        }

        if(parcel.ParcelNumber != updatedParcel.ParcelNumber)
         {
            var maybeParcel = await _context.Parcels.Where(parcel => parcel.ParcelNumber == updatedParcel.ParcelNumber).FirstOrDefaultAsync();
            if(maybeParcel != null) {
                return BadRequest();
            }
        }
        
        //Updating the values
        parcel.ParcelNumber = updatedParcel.ParcelNumber;
        parcel.RecipientName = updatedParcel.RecipientName;
        parcel.DestinationCountry = updatedParcel.DestinationCountry;
        parcel.Weight = updatedParcel.Weight;
        parcel.Price = updatedParcel.Price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if(!_context.Parcels.Any(p => p.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParcel(int id)
    {
        var parcel = await _context.Parcels.FindAsync(id);

        if(parcel == null)
        {
            return NotFound();
        }

        _context.Parcels.Remove(parcel);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException)
        {
            return BadRequest("Error while deleting parcel");
        }

        return NoContent();
    }
}
