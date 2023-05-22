using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class Blood_dataController : Controller
    {
        private readonly ProjDbContext _context;

        public Blood_dataController(ProjDbContext context)
        {
            _context = context;
        }

        
    
        // GET: Blood_data
        public async Task<IActionResult> Index_u(int u_id,int param1,int wallet,string name,bool auth)
        {
            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = u_id;
            ViewData["auth"] = auth;
            ViewBag.Id = param1;
            ViewBag.u_id = u_id;
            return _context.Bloods != null ? 
                          View(await _context.Bloods.Where(x=>x.h_id==param1).ToListAsync()) :
                          Problem("Entity set 'ProjDbContext.Bloods'  is null.");
        }
        public async Task<IActionResult> Index(bool param1,int h_id)
        {
            ViewBag.h_id = h_id;
            ViewData["auth"] = param1;
            ViewData["id"] = h_id;
            if(param1)
            {
                return _context.Bloods != null ?
                        View(await _context.Bloods.Where(x=>x.h_id==h_id).ToListAsync()) :
                        Problem("Entity set 'ProjDbContext.Bloods'  is null.");
            }
            return NotFound();
            
        }

        // GET: Blood_data/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bloods == null)
            {
                return NotFound();
            }

            var blood_data = await _context.Bloods
                .FirstOrDefaultAsync(m => m.b_Id == id);
            if (blood_data == null)
            {
                return NotFound();
            }

            return View(blood_data);
        }

        // GET: Blood_data/Create
        public IActionResult Create(int h_id, bool param1)
        {
            ViewBag.h_id = h_id;
            ViewData["auth"] = param1;
            ViewData["id"] = h_id;
         
            if(param1)
            {
                return View();
            }
            return NotFound();
            
        }

        // POST: Blood_data/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("b_Id,h_id,b_type,b_quantity,b_price")] Blood_data blood_data)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blood_data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Blood_data", new
                {
                    param1 = true,
                     h_id = blood_data.h_id
                    
                
                });
            }
            return View(blood_data);
        }

        // GET: Blood_data/Edit/5
        public async Task<IActionResult> Edit(int? id, int h_id, bool param1)
        {
            ViewBag.h_id = h_id;
            ViewData["auth"] = param1;
            ViewData["id"] = h_id;
            if (id == null || _context.Bloods == null)
            {
                return NotFound();
            }

            var blood_data = await _context.Bloods.FindAsync(id);
            if (blood_data == null)
            {
                return NotFound();
            }
            return View(blood_data);
        }

        // POST: Blood_data/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("b_Id,h_id,b_type,b_quantity,b_price")] Blood_data blood_data)
        {
            if (id != blood_data.b_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blood_data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Blood_dataExists(blood_data.b_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Blood_data", new
                {
                    param1 = true,
                    h_id = blood_data.h_id


                });
            }
            return View(blood_data);
        }

        // GET: Blood_data/Delete/5
        public async Task<IActionResult> Delete(int? id, int h_id, bool param1)
        {
            ViewData["auth"] = true;
            ViewData["id"] = h_id;

            if (id == null || _context.Bloods == null)
            {
                return NotFound();
            }

            var blood_data = await _context.Bloods
                .FirstOrDefaultAsync(m => m.b_Id == id);
            if (blood_data == null)
            {
                return NotFound();
            }

            return View(blood_data);
        }

        // POST: Blood_data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bloods == null)
            {
                return Problem("Entity set 'ProjDbContext.Bloods'  is null.");
            }
            var blood_data = await _context.Bloods.FindAsync(id);
            int s = blood_data.h_id;
            if (blood_data != null)
            {
                _context.Bloods.Remove(blood_data);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Blood_data", new
            {
                param1 = true,
                h_id = s


            });
        }

        private bool Blood_dataExists(int id)
        {
          return (_context.Bloods?.Any(e => e.b_Id == id)).GetValueOrDefault();
        }
    };
}
