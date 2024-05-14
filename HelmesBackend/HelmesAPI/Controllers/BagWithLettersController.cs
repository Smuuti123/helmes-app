using HelmesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using HelmesAPI.Models;

namespace HelmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BagWithLettersController : ControllerBase
{
    private readonly AppDbContext _context;

    public BagWithLettersController(AppDbContext context)
    {
        _context = context;
    }
}
