using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Models;
using MVC_Appointment.Models.ViewModels;
using MVC_Appointment.Services;
using MVC_Appointment.Utility;
using System;
using System.Linq;
using System.Security.Claims;

namespace MVC_Appointment.Controllers.Api
{
    [ApiController]
    [Route("api/Appointment")]
    public class AppointmentApiController : Controller
    {
        public readonly IAppointmentService _appointmentService;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string _loginUserId;
        public readonly string _role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            _loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }



        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpateAppointment(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch(Exception e)
            {
                commonResponse.message=e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}
