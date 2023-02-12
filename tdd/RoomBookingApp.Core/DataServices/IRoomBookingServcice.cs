namespace RoomBookingApp.Core.DataServices;

using RoomBookingApp.Core.Domain;

public interface IRoomBookingServcice
{
    void Save(RoomBooking roomBooking);

    IEnumerable<Room> GetAvailableRooms(DateTime date);
}
