using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ProjDbContext _context;
       

        public HospitalsController(ProjDbContext context)
        {
            _context = context;
        }

        // GET: Hospitals
        public async Task<IActionResult> Index_d(int wallet,string name,bool auth,int id,string cityy)
        {


            if (auth)
            {
                ViewData["wallet"] = wallet;
                ViewData["name"] = name;
                ViewData["id"] = id;
                ViewData["auth"] = auth;


                return _context.Hospitals != null ?
                              View(await _context.Hospitals.Where(x => x.h_city == cityy).ToListAsync()) :
                              Problem("Entity set 'ProjDbContext.Hospitals'  is null.");
            }
            return NotFound();

        }
        public async Task<IActionResult> Index(bool auth)
        {
            ViewData["auth"] = auth;

            if (auth)
            {
                return _context.Hospitals != null ?
                     View(await _context.Hospitals.ToListAsync()) :
                     Problem("Entity set 'ProjDbContext.Hospitals'  is null.");

            }
            return NotFound();
       

        }
        public async Task<IActionResult> Index_r(int wallet, string name, bool auth, int id, string cityy)
        {


            if (auth)
            {
                ViewData["wallet"] = wallet;
                ViewData["name"] = name;
                ViewData["id"] = id;
                ViewData["auth"] = auth;

                return _context.Hospitals != null ?
                            View(await _context.Hospitals.Where(x => x.h_city == cityy).ToListAsync()) :
                            Problem("Entity set 'ProjDbContext.Hospitals'  is null.");
            }
            return NotFound();

        }

        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.h_Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospitals/Create
        public IActionResult Create()
        {
            ViewData["auth"] = true;
            var cities = _context.Cities.ToList();
            List<string> s = new List<string>();
            foreach (City c in cities)
            {
                s.Add(c.c_name);
               

            }
            ViewBag.Products = s;
          
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("h_Id,h_name,h_city,h_location,h_loc_url,h_img,h_bloodquantity")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Hospitals", new
                {

                    auth = true


                });
            }
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var cities = _context.Cities.ToList();
            List<string> s = new List<string>();
            foreach (City c in cities)
            {
                s.Add(c.c_name);


            }
            ViewBag.Products = s;

            ViewData["auth"] = true;
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("h_Id,h_name,h_city,h_location,h_loc_url,h_img,h_bloodquantity")] Hospital hospital)
        {
            if (id != hospital.h_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.h_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Hospitals", new
                {

                    auth = true


                });
            }
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.h_Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hospitals == null)
            {
                return Problem("Entity set 'ProjDbContext.Hospitals'  is null.");
            }
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hospitals", new
            {

                auth = true


            });

            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
          return (_context.Hospitals?.Any(e => e.h_Id == id)).GetValueOrDefault();
        }
    }
}
