using MVC_Appointment.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Appointment.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> getDoctorList();
        public List<PatientVM> getPatientList();
        public Task<int> AddUpateAppointment(AppointmentVM model);
        public List<AppointmentVM> GetAppointmentByDocId(string Id);
        public List<AppointmentVM> GetAppointmentByPatientId(string Id);
        public AppointmentVM GetAppointmentById(int Id);
    }
}
