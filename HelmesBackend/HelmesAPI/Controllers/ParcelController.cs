using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelmesAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;

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

    [HttpPost] //Adding parcel
    public async Task<IActionResult> CreateParcel(Parcel parcel)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        //Setting up the format
        if(!Regex.IsMatch(parcel.ParcelNumber, @"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$"))
        {
            return BadRequest("Parcel number must be in format “LLNNNNNNLL”, where L is letter, N is digit");
        }
        //Parcels have to be unique
        if( await _context.Parcels.AnyAsync(p => p.ParcelNumber == parcel.ParcelNumber))
        {
            return BadRequest("Parcel with the same parcel number already exists");
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
            return BadRequest("");
        }
        //Price max 2 decimals allowed after comma
        if((decimal)(Math.Round(parcel.Price * 100) / 100) != parcel.Price)
        {
            return BadRequest("");
        }

    
        _context.Parcels.Add(parcel);
         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateException)
         {
            return StatusCode(500, "Error occured while saving the parcel");
         }
        
        
        return CreatedAtAction(nameof(GetParcel), new {parcel.Id}, parcel);
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
}
