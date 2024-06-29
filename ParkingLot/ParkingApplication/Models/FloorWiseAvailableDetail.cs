using ParkingInfrastructure.Enums;

namespace ParkingApplication.Models;

public class FloorWiseAvailableDetail
{
    public int Floor { get; set; }
    
    public VehicleType VechicleType { get; set; }
    
    public int AvailableSlots { get; set; }
    // public IEnumerable<VehicleAvailableSlotDetail> VehicleAvailableSlotDetail { get; set; }
}