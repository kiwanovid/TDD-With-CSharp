using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers;
/*
 Don't forget to add the package Microsoft.EntityFrameworkCore.Design
because we use the roombookingApp.persistence and this project uses it
 */

[Route("api/[controller]")]
[ApiController]
public class RoomBookingController : ControllerBase
{
    private IRoomBookingRequestProcessor _roomBookingProcessor;

    public RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor)
    {
        _roomBookingProcessor = roomBookingProcessor;
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom(RoomBookingRequest request)
    {
        if (ModelState.IsValid)
        {
            var result = _roomBookingProcessor.BookRoom(request);
            if (result.Flag == BookingResultFlag.Success)
            {
                return Ok(result);
            }

            ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No Rooms Available For Given Date");
        }

        return BadRequest(ModelState);
    }
}
