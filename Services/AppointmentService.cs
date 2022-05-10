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

            if (model != null && model.Id > 0)
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

        public List<AppointmentVM> GetAppointmentByDocId(string Id)
        {
            var appointments = _db.Appointments.Where(a => a.DoctorId == Id).Select(
                b => new AppointmentVM
                {
                    Title = b.Title,
                    Id = b.Id,
                    Description = b.Description,
                    StartDate = b.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndDate = b.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Duration = b.Duration,
                    isDocApproved = b.isDocApproved,
                    DoctorId = b.DoctorId,
                    PatientId = b.PatientId,
                    DocName = _db.Users.Where(a => a.Id == b.DoctorId).Select(a => a.Name).FirstOrDefault(),
                    PatientName = _db.Users.Where(a => a.Id == b.PatientId).Select(a => a.Name).FirstOrDefault()

                }).ToList();
            return appointments;
        }

        public AppointmentVM GetAppointmentById(int Id)
        {
            var appointment = _db.Appointments.Where(a => a.Id == Id).Select(b => new AppointmentVM
            {
                Title = b.Title,
                Id = b.Id,
                Description = b.Description,
                StartDate = b.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = b.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = b.Duration,
                isDocApproved = b.isDocApproved,
                DoctorId = b.DoctorId,
                PatientId = b.PatientId,
                DocName = _db.Users.Where(a => a.Id == b.DoctorId).Select(a => a.Name).FirstOrDefault(),
                PatientName = _db.Users.Where(a => a.Id == b.PatientId).Select(a => a.Name).FirstOrDefault()

            }).FirstOrDefault();

            return appointment;
        }

        public List<AppointmentVM> GetAppointmentByPatientId(string Id)
        {
            var appointments = _db.Appointments.Where(a => a.PatientId == Id).Select(b => new AppointmentVM
            {
                Title = b.Title,
                Id = b.Id,
                Description = b.Description,
                StartDate = b.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = b.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = b.Duration,
                isDocApproved = b.isDocApproved,
                DoctorId = b.DoctorId,
                PatientId = b.PatientId,
                DocName = _db.Users.Where(a => a.Id == b.DoctorId).Select(a => a.Name).FirstOrDefault(),
                PatientName = _db.Users.Where(a => a.Id == b.PatientId).Select(a => a.Name).FirstOrDefault()

            }).ToList();
            return appointments;
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
