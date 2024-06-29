using ParkingInfrastructure.Enums;

namespace ParkingApplication.Models;

public class VehicleAvailableSlotDetail
{
    public VehicleType VehicleType { get; set; }
    public int AvailableSlots { get; set; }
}