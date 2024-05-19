using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelmesAPI.Controllers;
using HelmesAPI.Data;
using HelmesAPI.Models;
using HelmesAPI.Protocol;

namespace HelmesApp.Tests
{
    public class BagWithParcelsControllerTests
    {
        private readonly BagWithParcelsController _controller;
        private readonly AppDbContext _context;
    }
}