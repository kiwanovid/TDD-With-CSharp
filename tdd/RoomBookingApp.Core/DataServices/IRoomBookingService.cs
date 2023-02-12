namespace RoomBookingApp.Core.DataServices;

using RoomBookingApp.Domain;

public interface IRoomBookingService
{
    void Save(RoomBooking roomBooking);

    IEnumerable<Room> GetAvailableRooms(DateTime date);
}
