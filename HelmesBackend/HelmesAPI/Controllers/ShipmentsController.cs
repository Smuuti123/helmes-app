using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using HelmesAPI.Protocol;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ShipmentsController(AppDbContext context)
    {
        _context = context;
    }
    
    private IActionResult ValidateShipment(CreateShipmentRequest shipment)
    {
        if(!Regex.IsMatch(shipment.ShipmentNumber, @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$"))
        {
            return BadRequest("Shipment number must be in format 'XXX-XXXXXX', where X is a letter or digit.");
        }
        if(_context.Shipments.Any(s => s.ShipmentNumber == shipment.ShipmentNumber))
        {
            return BadRequest("Shipment number must be unique.");
        }

        if (!Enum.IsDefined(typeof(Airport), shipment.KnownAirport))
        {
            return BadRequest("Invalid airport. Possible values are 'TLL', 'RIX', 'HEL'.");
        }
        if (!Regex.IsMatch(shipment.FlightNumber, @"^[A-Za-z]{2}\d{4}$"))
        {
            return BadRequest("Flight number has to be in format 'LLNNNN', where L is a letter and N is a digit.");
        }
        if(shipment.FlightDate < DateTime.Now)
        {
            return BadRequest("Shipment/flight cannot be in the past");
        }
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShipment(CreateShipmentRequest createShipmentRequest)
    {
        IActionResult validationResult = ValidateShipment(createShipmentRequest);
        if (validationResult != null)
        {
            return validationResult;
        }

        Shipment shipment = new(createShipmentRequest); 
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetShipment), new { id = shipment.Id}, shipment);
    }

    [HttpPost("{id}/Finalize")]
    public async Task<IActionResult> FinalizeShipment(int id)
    {
        var shipment = await _context.Shipments.Include(s => s.Bags).ThenInclude(b => (b as BagWithParcels).ListOfParcels).FirstOrDefaultAsync(s => s.Id == id);

        if(shipment == null)
        {
            return NotFound();
        }

        if(!shipment.Bags.Any())
        {
            return BadRequest("Shipment must contain atleast one barcel bag or letter bag");
        }

        if(shipment.Status == Status.FINALIZED)
        {
            return BadRequest("Already finalized");
        }
        
        foreach(var bag in shipment.Bags)
        {
            if(bag is BagWithParcels parcelBag && !parcelBag.ListOfParcels.Any())
            {
                return BadRequest("Parcel bag has to contain atleast one pracel");
            }
            else if(bag is BagWithLetters letterBag && letterBag.CountOfLetters < 1)
            {
                return BadRequest("Letter bag must contain atleast one letter");
            }
        }

        shipment.Status = Status.FINALIZED;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Shipment>> GetShipment(int id)
    {
        var shipment = await _context.Shipments.Include(s => s.Bags).ThenInclude(b => (b as BagWithParcels).ListOfParcels).FirstOrDefaultAsync(s => s.Id == id);

        if(shipment == null)
        {
            return NotFound();
        }
        
        return shipment;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
    {
        var shipments = await _context.Shipments.Include(s => s.Bags).ThenInclude(b => (b as BagWithParcels).ListOfParcels).ToListAsync();
        return Ok(shipments);
    }
}
