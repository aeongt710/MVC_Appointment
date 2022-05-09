using MVC_Appointment.Models;
using MVC_Appointment.Models.ViewModels;
using MVC_Appointment.Utility;
using System;
using System.Collections.Generic;
//IMP for select
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDBContext _db;
        public AppointmentService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpateAppointment(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(model.Duration);

            if(model !=null && model.Id>0)
            {
                return 1;
            }
            else
            {
                Appointment _appointment = new Appointment()
                {
                    Title = model.Title,
                    StartDate = startDate,
                    EndDate = endDate,
                    Description = model.Description,
                    DoctorId = model.DoctorId,
                    Duration = model.Duration,
                    AdminId = model.AdminId,
                    PatientId = model.PatientId,
                    isDocApproved = false
                };
                _db.Appointments.Add(_appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<DoctorVM> getDoctorList()
        {
            var docs = (from user in _db.Users
                        join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                        join netRoles in _db.Roles.Where(a => a.Name == Helper.Doctor) on userRoles.RoleId equals netRoles.Id
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
                        join netRoles in _db.Roles.Where(a => a.Name == Helper.Patient) on userRoles.RoleId equals netRoles.Id
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
