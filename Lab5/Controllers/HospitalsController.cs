using Lab5.ApiModels.Hospital;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class HospitalsController : Controller
    {
        private ApplicationContext db;
        public HospitalsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Hospitals.ToListAsync());
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateHospitalApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Hospitals.Add(new HospitalModel
            {
                Id = Guid.NewGuid(),
                Name = api.Name,
                Address = api.Address,
                Phones = api.Phones
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
            var hospital = await db.Hospitals.Include(i => i.Doctors).FirstOrDefaultAsync(q => q.Id == id);
            if(hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var hospital = await db.Hospitals.FirstOrDefaultAsync(q => q.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(new EditHospitalApi
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Phones = hospital.Phones
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditHospitalApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Hospitals.Update(new HospitalModel
            {
                Id = api.Id,
                Name = api.Name,
                Address = api.Address,
                Phones = api.Phones
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
            var hospital = await db.Hospitals.FirstOrDefaultAsync(q => q.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }
            db.Doctors.RemoveRange(db.Doctors.Where(q => q.HospitalId == hospital.Id));
            db.Hospitals.Remove(hospital);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
