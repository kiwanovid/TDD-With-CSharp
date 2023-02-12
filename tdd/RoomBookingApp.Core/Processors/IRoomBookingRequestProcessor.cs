namespace RoomBookingApp.Core.Processors;

using RoomBookingApp.Core.Models;

public interface IRoomBookingRequestProcessor
{
    RoomBookingResult BookRoom(RoomBookingRequest bookingRequest);
}