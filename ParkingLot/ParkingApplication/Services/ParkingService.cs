﻿using Microsoft.EntityFrameworkCore;
using ParkingApplication.Models;
using ParkingInfrastructure;
using ParkingInfrastructure.Models;

namespace ParkingApplication.Services;

public class ParkingService : IParkingService
{
    private readonly ParkingDbContext _parkingDbContext;
    private readonly double PER_HOUR_COST = 50.0;
    public ParkingService(ParkingDbContext parkingDbContext)
    {
        _parkingDbContext = parkingDbContext;

    }

    public async Task<int> CreateParkingSlot(ParkingSlotCreationRequest request)
    {
        var availabilitySlotDetails = await _parkingDbContext.ParkingSlot
            .Where(_ => _.VehicleId == null && _.FloorId == request.FloorId)
            .FirstOrDefaultAsync();

        if (availabilitySlotDetails is null)
        {
            throw new Exception($"No slot available at floor {request.FloorId} please check differnt floor");
        }
        
        var vehicle = new Vehicle
        {
            VehicleNumber = request.VehicleId,
            EntryTime = DateTime.Now,
        };

        await _parkingDbContext.AddAsync(vehicle);
        await _parkingDbContext.SaveChangesAsync();

        availabilitySlotDetails.VehicleId = vehicle.Id;
        _parkingDbContext.Update(availabilitySlotDetails);
        await _parkingDbContext.SaveChangesAsync();

        return availabilitySlotDetails.ParkingSlotId;
    }
    
    public async Task<IEnumerable<FloorWiseAvailableDetail>> GetParkingAvailability()
    {
        var availabilitySlotDetails = await _parkingDbContext.ParkingSlot
                                                                .Where(_ => _.VehicleId == null)
                                                                .ToListAsync();

        if (availabilitySlotDetails?.Any() is not true)
        {
            throw new Exception("No Parking Slot is available!");
        }

        var floorWiseAvailableDetail = availabilitySlotDetails
            .GroupBy(_ =>  new {_.FloorId,_.SlotType})
            .Select( _ => new FloorWiseAvailableDetail
            {
                Floor = _.Key.FloorId,
                VechicleType = _.Key.SlotType,
                AvailableSlots = _.Count()
            });

        return floorWiseAvailableDetail;
    }
    
    public async Task<double> GetParkingCost(string vehicleNumber)
    {
        var vehicleDetail = await _parkingDbContext.Vehicle
            .Where(_ => _.VehicleNumber == vehicleNumber)
            .FirstOrDefaultAsync();

        if (vehicleDetail is null)
        {
            throw new Exception("Vehicle not found in parking");
        }
        vehicleDetail.ExitTime = DateTime.Now;
        var timeSpend = vehicleDetail.EntryTime - vehicleDetail.ExitTime;

        var cost = timeSpend.Hours * PER_HOUR_COST;
        _parkingDbContext.Update(vehicleDetail);
        await _parkingDbContext.SaveChangesAsync();
        return cost;
    }
}