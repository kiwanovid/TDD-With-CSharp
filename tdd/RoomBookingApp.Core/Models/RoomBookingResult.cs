namespace RoomBookingApp.Core.Models
{
    using RoomBookingApp.Core.Enums;
    using RoomBookingApp.Domain.BaseModels;

    public class RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }

        public int? RoomBookingId { get; set; }
    }
}
