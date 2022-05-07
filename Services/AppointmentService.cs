using MVC_Appointment.Models;
using MVC_Appointment.Models.ViewModels;
using MVC_Appointment.Utility;
using System.Collections.Generic;
//IMP for select
using System.Linq;

namespace MVC_Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDBContext _db;
        public AppointmentService(ApplicationDBContext db)
        {
            _db = db;
        }
        public List<DoctorVM> getDoctorList()
        {
            var docs = (from user in _db.Users
                        join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                        join netRoles in _db.Roles.Where(a => a.Name == Helper.doctor) on userRoles.RoleId equals netRoles.Id
                        select new DoctorVM
                        {
                            Id = user.Id,
                            Name = user.Name
                        }
                      )
                      .ToList();
            return docs;
        }

        public List<PatientVM> getPatientList()
        {
            var patients = (from user in _db.Users
                        join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                        join netRoles in _db.Roles.Where(a => a.Name == Helper.patient) on userRoles.RoleId equals netRoles.Id
                        select new PatientVM
                        {
                            Id = user.Id,
                            Name = user.Name
                        }
                      )
                      .ToList();
            return patients;
        }
    }
}
