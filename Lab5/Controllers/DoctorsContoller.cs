using Lab5.ApiModels.Doctor;
using Lab5.ApiModels.Hospital;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class DoctorsController : Controller
    {
        private ApplicationContext db;
        public DoctorsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Doctors.ToListAsync());
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hospitals = await db.Hospitals.Select(s => new { s.Id, s.Name }).ToListAsync();
            return View(new CreateDoctorApi
            {
                Hospitals = hospitals.Select(s => (s.Id, s.Name))
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorApi api)
        {
            var hospitals = await db.Hospitals.Select(s => new { s.Id, s.Name }).ToListAsync();
            api.Hospitals = hospitals.Select(s => (s.Id, s.Name));
            if (!ModelState.IsValid) return View(api);

            if(api.HospitalId == null)
            {
                ModelState.AddModelError("HospitalId", "Hospital not found");
                return View(api);
            }
            else
            {
                var hospital = await db.Hospitals.FirstOrDefaultAsync(q => q.Id == api.HospitalId);
                if(hospital == null)
                {
                    ModelState.AddModelError("HospitalId", "Hospital not found");
                    return View(api);
                }
            }
            db.Doctors.Add(new DoctorModel
            {
                Id = Guid.NewGuid(),
                Name = api.Name,
                Gender = api.Gender,
                Speciality = api.Speciality,
                HospitalId = api.HospitalId
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var doctor = await db.Doctors.Include(i => i.Hospital).FirstOrDefaultAsync(q => q.Id == id);
            if(doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var doctor = await db.Doctors.FirstOrDefaultAsync(q => q.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            var hospitals = await db.Hospitals.Select(s => new { s.Id, s.Name }).ToListAsync();

            return View(new EditDoctorApi
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Gender = doctor.Gender,
                HospitalId = doctor.HospitalId,
                Hospitals = hospitals.Select(s => (s.Id, s.Name))
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditDoctorApi api)
        {
            var hospitals = await db.Hospitals.Select(s => new { s.Id, s.Name }).ToListAsync();
            api.Hospitals = hospitals.Select(s => (s.Id, s.Name));
            if (!ModelState.IsValid) return View(api);

            if(hospitals.FirstOrDefault(q => q.Id == api.HospitalId) != null)
            {
                db.Doctors.Update(new DoctorModel
                {
                    Id = api.Id,
                    Name = api.Name,
                    Gender = api.Gender,
                    Speciality = api.Speciality,
                    HospitalId = api.HospitalId
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("HospitalId", "Hospital not found");
            return View(api);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var doctor = await db.Doctors.FirstOrDefaultAsync(q => q.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
