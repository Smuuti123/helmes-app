using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BagWithParcelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BagWithParcelsController(AppDbContext context)
    {
        _context = context;
    }
}
