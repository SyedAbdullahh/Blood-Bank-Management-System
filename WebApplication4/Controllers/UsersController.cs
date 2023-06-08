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
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace WebApplication4.Controllers
{
    public class UsersController : Controller
    {
        private readonly ProjDbContext _context;
        

        public UsersController(ProjDbContext context)
        {
            _context = context;
        }
        public IActionResult Privacy(int wallet, string name, bool auth, int id)
        {


            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["auth"] = auth;
            if (auth)
            {
                return View();
            }
            return NotFound();
        }

        // GET: Users
        public async Task<IActionResult> Index(bool param1)
        {
            ViewData["auth"] = param1;
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'ProjDbContext.Users'  is null.");
        }
        [HttpGet]
        public async Task<IActionResult> SelectCity(int wallet, string name, bool auth, int id)
        {
            var cities = from m in _context.Cities
                            select m;

            if (auth)
            {
                ViewData["wallet"] = wallet;
                ViewData["name"] = name;
                ViewData["id"] = id;
                ViewData["auth"] = auth;
                ViewBag.auth = auth;
                return View(cities);
            }
            else
            {
                return NotFound();
            }
           
            
            
        }
        [HttpPost]
        public async Task<IActionResult> SelectCity(int wallet, string name, int auth, int id, string city)
        {

            return RedirectToAction("Index_d", "Hospitals", new
            {
                wallet=wallet,
                name=name,
                auth=true,
                id=id,
                cityy = city


            }) ;
        }
        // Here we go for Receival of Blood....
        [HttpGet]
       
            public async Task<IActionResult> SelectCity_r(int wallet, string name, bool auth, int id)
            {
            var cities = from m in _context.Cities
                         select m;

            if (auth)
            {
                    ViewData["wallet"] = wallet;
                    ViewData["name"] = name;
                    ViewData["id"] = id;
                    ViewData["auth"] = auth;

                    return View(cities);
            }
            else
            {
                return NotFound();
            }



        }
        [HttpPost]
        public async Task<IActionResult> SelectCity_r(int wallet, string name, bool auth, int id, string city)
        {

            return RedirectToAction("Index_r", "Hospitals", new
            {
                wallet = wallet,
                name = name,
                auth = true,
                id = id,
                cityy = city


            });
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.u_Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Login(string ?v)
        {
           
            if(v!=null)
            {
                ViewData["v"] = true ;
            }
            else
            {
                ViewData["v"] = false;
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(User usrr)
        {
            string a = "Active";
            // Code that verifies the email and password correction and takes user to other page
            bool flag=_context.Users.Any(x=>x.u_Email==usrr.u_Email && x.u_Password==usrr.u_Password);
            bool flagg=(_context.Users?.Any(x => x.u_Email == usrr.u_Email && x.u_Password == usrr.u_Password)).GetValueOrDefault();
            var usr = _context.Users.Where(x => x.u_Email ==usrr.u_Email && x.u_Password == usrr.u_Password).FirstOrDefault();

         string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
        .AddJsonFile("appsettings.json").Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Select * from Users where u_Email ='" + usrr.u_Email + "' and u_Password='" + usrr.u_Password + "'", con);
        SqlDataReader sddr = cmd.ExecuteReader();
            if (usr!=null||sddr.Read()||flag||flagg)
            {
                return RedirectToAction("Dashboard", "Users", new
                {
                    wallet = usr.u_wallet,
                    name = usr.u_Name,
                    auth = true,
                    id=usr.u_Id

                }) ;
            }
            else
            {

                return RedirectToAction("Login", "Users", new
                {
                    v = "Incorrect User Email or Password"
                });

            }


        }
        public IActionResult Dashboard(int wallet,string name,bool auth,int id)
        {
           

            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["auth"] = auth;
           if (auth)
           {
                return View();
            }
           else
            {
                return NotFound();
            }
            
        }
        public IActionResult Donate(int wallet, string name, bool auth, int id)
        {


            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["auth"] = auth;
            if (auth)
            {
                return View();
            }
            else
            {
                return NotFound();
            }

        }


        // GET: Users/Create
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("u_Id,u_Name,u_Email,u_Password,u_PhoneNumber,u_Address,u_bloodgroup,u_wallet,u_status")] User user)
        {
            user.u_wallet = 0;
            user.u_status = "Inactive";
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("u_Id,u_Name,u_Email,u_Password,u_PhoneNumber,u_Address,u_bloodgroup,u_wallet,u_status")] User user)
        {
            if (id != user.u_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.u_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Users", new
                {

                    auth = true

                });
            }
            return View(user);
        }

    
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.u_Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ProjDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Users", new
            {

                auth = true

            });
        }


        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.u_Id == id)).GetValueOrDefault();
        }
    }
}
