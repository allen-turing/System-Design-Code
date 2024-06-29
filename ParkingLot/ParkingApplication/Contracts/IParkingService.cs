using ParkingApplication.Models;

namespace ParkingApplication;

public interface IParkingService
{
    public Task<int> CreateParkingSlot(ParkingSlotCreationRequest request);
    Task<int> VacateParkingSlot(string vehicleNumber);
    public Task<IEnumerable<FloorWiseAvailableDetail>> GetParkingAvailability();
    public Task<double> GetParkingCost(string vehicleNumber);
}