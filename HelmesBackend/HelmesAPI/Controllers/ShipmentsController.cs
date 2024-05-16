using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;
using HelmesAPI.Protocol;

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

    // [HttpPost("{shipmentId}/AddBag/{bagId}")]
    // public async Task<IActionResult> AddBag()
    // {
    //  // validate that bag is not in the other shipment that is already finalized
    //  // validate that shipment is not finalized   
    // }

    // [HttpPost("{id}/Finalize")]
    // public async Task<IActionResult> FinalizeShipment()
    // {
    //     // validate that bags are not in the other shipment that is already finalized
    //     // validate that bas are not emp
    // }

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
