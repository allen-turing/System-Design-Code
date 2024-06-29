namespace ParkingInfrastructure.Models;

public class Vehicle
{
    public int Id { get; set; }
    public required string VehicleNumber { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
}