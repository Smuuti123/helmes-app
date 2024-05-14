using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;

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
}
