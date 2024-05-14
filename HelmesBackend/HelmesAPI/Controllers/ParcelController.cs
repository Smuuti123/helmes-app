using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelmesAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        _context.Parcels.Add(parcel);
        await _context.SaveChangesAsync();
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
