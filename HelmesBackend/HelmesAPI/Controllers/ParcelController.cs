using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;

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
}
