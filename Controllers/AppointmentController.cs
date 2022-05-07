using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Services;
using MVC_Appointment.Utility;

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
            ViewBag.patientList = _appointmentService.getPatientList();
            ViewBag.duration = Helper.GetTimeDropDown();
            return View();
        }
    }
}
