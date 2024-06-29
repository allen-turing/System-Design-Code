using Microsoft.AspNetCore.Mvc;
using ParkingApplication;
using ParkingApplication.Models;

namespace Parking.API.Controllers;

[ApiController]
[Route("parking")]
public class ParkingLotController: ControllerBase
{
    private readonly IParkingService _parkingService;
    private readonly ILogger<ParkingLotController> _logger;

    public ParkingLotController(IParkingService parkingService, ILogger<ParkingLotController> logger)
    {
        _parkingService = parkingService;
        _logger = logger;
    }

    [HttpPost]
    [Route("book-parking")]
    public async Task<int> CreateParkingSlot(ParkingSlotCreationRequest request)
    {
        return await _parkingService.CreateParkingSlot(request);
    }
    
    [HttpGet]
    [Route("available-slots")]
    public async Task<IEnumerable<FloorWiseAvailableDetail>> GetAllParkingAvailability()
    {
        return await _parkingService.GetParkingAvailability();
    }
    
    [HttpGet]
    [Route("cost/{vehicleNumber}")]
    public async Task<double> GetParkingCost([FromRoute] string vehicleNumber)
    {
        return await _parkingService.GetParkingCost(vehicleNumber);
    }
}