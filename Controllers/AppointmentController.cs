using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Services;

namespace MVC_Appointment.Controllers
{
    public class AppointmentController : Controller
    {
        public readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService=appointmentService;
        }
        public IActionResult Index()
        {
            ViewBag.docList= _appointmentService.getDoctorList();
            return View();
        }
    }
}
