using Lab5.ApiModels.Hospital;
using Lab5.ApiModels.Lab;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        private ApplicationContext db;
        public LabsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Labs.ToListAsync());
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateLabApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Labs.Add(new LablModel
            {
                Id = Guid.NewGuid(),
                Name = api.Name,
                Address = api.Address
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
            var lab = await db.Labs.FirstOrDefaultAsync(q => q.Id == id);
            if(lab == null)
            {
                return NotFound();
            }
            return View(lab);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var lab = await db.Labs.FirstOrDefaultAsync(q => q.Id == id);
            if (lab == null)
            {
                return NotFound();
            }
            return View(new EditLabApi
            {
                Id = lab.Id,
                Name = lab.Name,
                Address = lab.Address
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditLabApi api)
        {
            if (!ModelState.IsValid) return View(api);
            db.Labs.Update(new LablModel
            {
                Id = api.Id,
                Name = api.Name,
                Address = api.Address
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
            var lab = await db.Labs.FirstOrDefaultAsync(q => q.Id == id);
            if (lab == null)
            {
                return NotFound();
            }
            db.Labs.Remove(lab);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
