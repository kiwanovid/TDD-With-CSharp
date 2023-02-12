namespace RoomBookingApp.Core.Models
{
    using RoomBookingApp.Core.Enums;

    public class RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }

        public int? RoomBookingId { get; set; }
    }
}