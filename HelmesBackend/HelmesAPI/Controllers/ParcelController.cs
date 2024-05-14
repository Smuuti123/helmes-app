using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelmesAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

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
            return BadRequest("Parcle with the same parcel number already exists");
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
