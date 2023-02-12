namespace RoomBookingApp.Persistence.Repositories;

using System;
using System.Collections.Generic;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

public class RoomBookingService : IRoomBookingServcice
{
    private readonly RoomBookingAppDbContext _context;

    public RoomBookingService(RoomBookingAppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Room> GetAvailableRooms(DateTime date)
    {
        return _context.Rooms
            .Where(r => !r.RoomBookings.Any(rb => rb.Date == date))
            .ToList();
    }
    public void Save(RoomBooking roomBooking)
    {
        _context.RoomBookings.Add(roomBooking);
        _context.SaveChanges();
    }
}
