using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Domain;
using Shouldly;
using Xunit;
//arrange act assert
//core tests henne la nshuf aymata we call certain objects
namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingReuestProcessingTest
    {
        private RoomBookingRequestProcessor _processor;
        private RoomBookingRequest _request;
        private Mock<IRoomBookingServcice> _roomBookingServiceMock;
        private List<Room> _availableRooms;

        public RoomBookingReuestProcessingTest()
        {
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2022, 10, 20)
            };

            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingServcice>();

            _roomBookingServiceMock
                .Setup(rbs => rbs.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);

            //if we want the object inside the mock we use .Object
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_request);

            //Assert
            ////Assert.NotNull(result);
            ////Assert.Equal(request.FullName, result.FullName);
            ////Assert.Equal(request.Email, result.Email);
            ////Assert.Equal(request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);

        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            //Should.Throw<ArgumentNullException>(() => processor.BookRoom(null));
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));

            exception.ParamName.ShouldBe("bookingRequest");
            Assert.Equal("bookingRequest", exception.ParamName);
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;
            _roomBookingServiceMock
                .Setup(rbs => rbs.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });
            _processor.BookRoom(_request);

            //Assert
            _roomBookingServiceMock.Verify(rbs => rbs.Save(It.IsAny<RoomBooking>()), Times.Once);
            Assert.NotNull(savedBooking);
            Assert.Equal(savedBooking.FullName, _request.FullName);
            Assert.Equal(savedBooking.Email, _request.Email);
            Assert.Equal(savedBooking.Date, _request.Date);

            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_If_None_Available()
        {
            _availableRooms.Clear();

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(rbs => rbs.Save(It.IsAny<RoomBooking>()), Times.Never);
        }

        //Data driven test
        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag bookingResultFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }

            var result = _processor.BookRoom(_request);

            bookingResultFlag.ShouldBe(result.Flag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _roomBookingServiceMock
                .Setup(rbs => rbs.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    booking.Id = roomBookingId.Value;
                });
            }

            var result = _processor.BookRoom(_request);
            result.RoomBookingId.ShouldBe(roomBookingId);
        }
    }
}
