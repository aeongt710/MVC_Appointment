using MVC_Appointment.Models.ViewModels;
using System.Collections.Generic;

namespace MVC_Appointment.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> getDoctorList();
        public List<PatientVM> getPatientList();
    }
}
