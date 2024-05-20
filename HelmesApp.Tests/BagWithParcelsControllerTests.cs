using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public BagWithParcelsControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _controller = new BagWithParcelsController(_context);
        }

        [Fact]
        public async Task CreateBagWithParcels_DuplicateBagNumber_ReturnsBadRequest()
        {
            var shipment = new Shipment
            {
                Id = 1,
                Status = Status.CREATED,
                ShipmentNumber = "SHIP123",
                FlightNumber = "FL1234"
            };
            _context.Shipments.Add(shipment);
            _context.BagWithParcels.Add(new BagWithParcels { BagNumber = "BAG123" });
            await _context.SaveChangesAsync();

            var request = new CreateBagWithParcelsRequest
            {
                BagNumber = "BAG123"
            };

            var result = await _controller.CreateBagWithParcels(request, shipment.Id);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Bag number must be unique", badRequestResult.Value);
        }
    }
}
