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
    public class EmployeesController : Controller
    {
        private readonly ProjDbContext _context;

        public EmployeesController(ProjDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(bool param1)
        {
            ViewData["auth"] = param1;
            if (param1)
            {
                return _context.Employees != null ?
                          View(await _context.Employees.ToListAsync()) :
                          Problem("Entity set 'ProjDbContext.Employees'  is null.");
            }
            else
            {
                return NotFound();
            }
              
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["auth"] =true;
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.e_Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            ViewData["auth"] = true;
            var hospitals = _context.Hospitals.ToList();
            List<string> s = new List<string>();
            List<int> id = new List<int>();
            foreach (Hospital i in hospitals)
            {
                s.Add(i.h_name);
                id.Add(i.h_Id);

            }
            ViewBag.Products = s;
            ViewBag.id = id;

            // Pass the data to the view
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("e_Id,e_Name,e_Email,e_Password,status,e_centre_id")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Employees", new
                {

                    param1 = true


                });
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("e_Id,e_Name,e_Email,e_Password,status,e_centre_id")] Employee employee)
        {

            if (id != employee.e_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.e_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Employees", new
                {

                    param1 = true


                });
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.e_Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["auth"] = true;

            if (_context.Employees == null)
            {
                return Problem("Entity set 'ProjDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employees", new
            {

                param1 = true


            });
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.e_Id == id)).GetValueOrDefault();
        }
    }
}
