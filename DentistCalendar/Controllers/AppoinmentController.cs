using DentistCalendar.Data;
using DentistCalendar.Data.Entity;
using DentistCalendar.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DentistCalendar.Controllers
{
    public class AppoinmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppoinmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult GetAppoinments()
        {
            var model = _context.Appointments
                    .Include(x => x.User)
                    .Select(x => new AppoinmentViewController()
                    {
                        Dentist = x.User.GetFullName(),
                        Patient = x.PatientName + " " + x.PatientSurname,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Description = x.Description,
                        UserId = x.User.Id
                    });

            return Json(model);
        }

        public JsonResult GetAppoinments(string userId = "")
        {
            var model = _context.Appointments
                    .Include(x => x.User).Where(x => x.UserId == userId)
                    .Select(x => new AppoinmentViewController()
                    {
                        Dentist = x.User.GetFullName(),
                        Patient = x.PatientName + " " + x.PatientSurname,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Description = x.Description,
                        UserId = x.User.Id
                    });

            return Json(model);
        }

        public JsonResult AddOrUpdateAppoinment(AddOrUpdateAppointmentModel model)
        {
            if (model.Id == 0)
            {
                Appointment appointment = model.Adapt<Appointment>();
                appointment.CreatedDate = DateTime.Now;

                _context.Add(appointment);
                _context.SaveChanges();
            }
            else
            {
                var appointment = model.Adapt<Appointment>();

                appointment.UpdateDate = DateTime.Now;

                _context.Update(appointment);

                _context.SaveChanges();
            }

            return Json("200");
        }

        public JsonResult DeleteAppointment(int id = 0)
        {
            var entity = _context.Appointments.Find(id);
            if (entity == null)
            {
                return Json("Not Found");
            }
            _context.Remove(entity);
            _context.SaveChanges();

            return Json("200");
        }
    }
}