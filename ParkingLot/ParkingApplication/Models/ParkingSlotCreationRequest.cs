using ParkingInfrastructure.Enums;

namespace ParkingApplication.Models;

public class ParkingSlotCreationRequest
{
    public required string VehicleId { get; set; }
    public required VehicleType VehicleType { get; set; }
    public required int FloorId { get; set; }
}