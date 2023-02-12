namespace RoomBookingApp.Core.DataServices;

using RoomBookingApp.Domain;

public interface IRoomBookingServcice
{
    void Save(RoomBooking roomBooking);

    IEnumerable<Room> GetAvailableRooms(DateTime date);
}
