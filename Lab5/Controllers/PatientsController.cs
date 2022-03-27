using Lab5.ApiModels.Hospital;
using Lab5.ApiModels.Patient;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class PatientsController : Controller
    {
        private ApplicationContext db;
        public PatientsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Patients.ToListAsync());
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Patients.Add(new PatientModel
            {
                Id = Guid.NewGuid(),
                Name = api.Name,
                Gender = api.Gender,
                BirthDate = api.BirthDate
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
            var patient = await db.Patients.FirstOrDefaultAsync(q => q.Id == id);
            if(patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var patient = await db.Patients.FirstOrDefaultAsync(q => q.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(new EditPatinetApi
            {
                Id = patient.Id,
                Name = patient.Name,
                Gender = patient.Gender,
                BirthDate = patient.BirthDate
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPatinetApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Patients.Update(new PatientModel
            {
                Id = api.Id,
                Name = api.Name,
                Gender = api.Gender,
                BirthDate = api.BirthDate
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var patient = await db.Patients.FirstOrDefaultAsync(q => q.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            db.Patients.Remove(patient);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
