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

namespace HelmesApp.Tests.Controllers
{
    public class ShipmentsControllerTests
    {
        private readonly ShipmentsController _controller;
        private readonly AppDbContext _context;

        public ShipmentsControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _controller = new ShipmentsController(_context);
        }

        [Fact]
        public async Task PostShipment_InvalidShipmentNumber_ReturnsBadRequest()
        {
            var invalidShipmentRequest = new CreateShipmentRequest
            {
                ShipmentNumber = "invalid",
                KnownAirport = Airport.TLL,
                FlightNumber = "LL1234",
                FlightDate = DateTime.Now.AddDays(1),
            };

            var result = await _controller.CreateShipment(invalidShipmentRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Shipment number must be in format 'XXX-XXXXXX', where X is a letter or digit.", badRequestResult.Value);
        }
        [Fact]
        public async Task PostShipment_PastFlightDate_ReturnsBadRequest()
        {

            var invalidShipmentRequest = new CreateShipmentRequest
            {
                ShipmentNumber = "ABC-123456",
                KnownAirport = Airport.TLL,
                FlightNumber = "LL1234",
                FlightDate = DateTime.Now.AddDays(-1),
            };

            var result = await _controller.CreateShipment(invalidShipmentRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Shipment/flight cannot be in the past", badRequestResult.Value);
        }
    }
}
