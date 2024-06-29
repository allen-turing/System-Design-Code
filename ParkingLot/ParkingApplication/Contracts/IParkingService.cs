using ParkingApplication.Models;

namespace ParkingApplication;

public interface IParkingService
{
    public Task<int> CreateParkingSlot(ParkingSlotCreationRequest request);
    public Task<IEnumerable<FloorWiseAvailableDetail>> GetParkingAvailability();
    public Task<double> GetParkingCost(string vehicleNumber);
}