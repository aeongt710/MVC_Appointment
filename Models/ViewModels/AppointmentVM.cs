using System;

namespace MVC_Appointment.Models.ViewModels
{
    public class AppointmentVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string AdminId { get; set; }
        public bool isDocApproved { get; set; }


        public string DocName { get; set; }
        public string PatientName { get; set; }
        public string AdminName { get; set; }
        public bool isForClient { get; set; }
    }
}
