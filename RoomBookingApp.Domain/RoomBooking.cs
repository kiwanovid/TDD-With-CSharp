namespace RoomBookingApp.Domain;
using RoomBookingApp.Domain.BaseModels;

public class RoomBooking : RoomBookingBase
{
    public int Id { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; }
}
