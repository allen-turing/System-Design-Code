using System.ComponentModel.DataAnnotations;
using ParkingInfrastructure.Enums;

namespace ParkingInfrastructure.Models;

public class ParkingSlot
{
    [Key]
    public int Id { get; set; }
    
    public int ParkingSlotId { get; set; }
    public VehicleType SlotType { get; set; }
    public int FloorId { get; set; }
    public int? VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
}