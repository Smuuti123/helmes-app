using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using HelmesAPI.Protocol;
using Microsoft.EntityFrameworkCore;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ShipmentsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> CreateShipment(CreateShipmentRequest createShipmentRequest)
    {
        Shipment shipment = new(createShipmentRequest); 
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetShipment), new { id = shipment.Id}, shipment);
    }

    [HttpPost("{id}/finalize")]
    public async Task<IActionResult> FinalizeShipment(int id)
    {
        var shipment = await _context.Shipments.Include(s => s.Bags).FirstOrDefaultAsync(s => s.Id == id);

        if(shipment == null)
        {
            return NotFound();
        }

        if(shipment.Status == Status.FINALIZED)
        {
            return BadRequest("Already finalized");
        }

        foreach(var bag in shipment.Bags)
        {
            if(bag is BagWithParcels parcelBag && !parcelBag.ListOfParcels.Any())
            {
                return BadRequest("Parcel bag has to contain one pracel");
            }
            else if(bag is BagWithLetters letterBag && letterBag.CountOfLetters < 1)
            {
                return BadRequest("Letter bag must contain one letter");
            }
        }

        shipment.Status = Status.FINALIZED;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Shipment>> GetShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);

        if(shipment == null)
        {
            return NotFound();
        }
        
        return shipment;
    }
}
