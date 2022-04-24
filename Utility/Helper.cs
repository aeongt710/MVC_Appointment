using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVC_Appointment.Utility
{
    public static class Helper
    {
        public static string admin = "Admin";
        public static string patient = "Patient";
        public static string doctor = "Doctor";
        public static List<SelectListItem> GetRolesDropDownList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.admin, Text=admin},
                new SelectListItem{Value=Helper.patient, Text=patient},
                new SelectListItem{Value=Helper.doctor, Text=doctor}
            };
        }
    }
}
