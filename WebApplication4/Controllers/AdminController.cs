using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjDbContext _context;


        public AdminController(ProjDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin usrr)
        {
            string a = "Active";
            // Code that verifies the email and password correction and takes user to other page
            bool flag = _context.Admins.Any(x => x.Id == usrr.Id && x.password == usrr.password);
            bool flagg = (_context.Admins?.Any(x => x.Id == usrr.Id && x.password == usrr.password)).GetValueOrDefault();
            var usr = _context.Admins.Where(x => x.Id == usrr.Id && x.password == usrr.password).FirstOrDefault();

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
            .AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Admins where Id ='" + usrr.Id + "' and Password='" + usrr.password + "'", con);
            SqlDataReader sddr = cmd.ExecuteReader();
            if (usr != null || sddr.Read() || flag || flagg)
            {
                return RedirectToAction("Dashboard", "Admin", new
                {
                   
                    auth = true

                });
            }
            else
            {

                return RedirectToAction("Login", "Admin");
            }


        }
        public IActionResult Dashboard(bool auth)
        {


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
        public IActionResult Privacy(bool auth)
        {
            ViewData["auth"] = auth;
            if(auth)
            {
                return View();
            }
            return NotFound();
            
        }
        public async Task<IActionResult> Transaction(bool auth,string? searchStringg)
        {
            ViewData["auth"] = auth;
            if (searchStringg == null)
            {
                ViewData["param1"] = "Khaali";
            }
            ViewData["param1"] = searchStringg;
      
            //if(param1)
            //{



            //return _context.Transactions != null ?
            //       View(await _context.Transactions.ToListAsync()) :
            //       Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            var movies = from m in _context.Transactions
                         select m;

            if (!String.IsNullOrEmpty(searchStringg))
            {
                bool flag = searchStringg.All(Char.IsDigit);
                if (flag)
                {
                    int num = Int32.Parse(searchStringg);
                    movies = movies.Where(s => s.t_Id == num);
                }
                else
                {
                    movies = movies.Where(s => s.u_name!.Contains(searchStringg));
                }

            }

            return View(await movies.ToListAsync());
            //}
            return NotFound();


        }

        public async Task<IActionResult> Index_s(string searchStringg)
        {
            if (searchStringg == null)
            {
                ViewData["param1"] = "Khaali";
            }
            ViewData["param1"] = searchStringg;

            //if(param1)
            //{



            //return _context.Transactions != null ?
            //       View(await _context.Transactions.ToListAsync()) :
            //       Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            var movies = from m in _context.Transactions
                         select m;

            if (!String.IsNullOrEmpty(searchStringg))
            {
                bool flag = searchStringg.All(Char.IsDigit);
                if (flag)
                {
                    int num = Int32.Parse(searchStringg);
                    movies = movies.Where(s => s.t_Id == num);
                }
                else
                {
                    movies = movies.Where(s => s.u_name!.Contains(searchStringg));
                }

            }

            return View(await movies.ToListAsync());
            //}
            return NotFound();


        }

        // GET: Transactions
        public async Task<IActionResult> Index_u(bool param1, int param2)
        {
            if (param1)
            {
                return _context.Transactions != null ?
                          View(await _context.Transactions.Where(x => x.u_id == param2).ToListAsync()) :
                          Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            }
            return NotFound();

        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.t_Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("t_Id,u_name,u_id,t_bloodtype,t_status,t_type,t_bloodquantity,t_bloodprice,t_date")] Transaction transaction)
        {
            if (id != transaction.t_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.t_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        public async Task<IActionResult> Close(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            var ussr = _context.Users.FindAsync(transaction.u_id);

            // Find Async helps us find data by primary key
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(int? id, [Bind("t_Id,u_name,u_id,h_id,t_bloodtype,t_status,t_type,t_bloodquantity,t_bloodprice,t_date,b_id")] Transaction transaction)
        {
           
            

            if (id != transaction.t_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var transactionn = await _context.Transactions.FindAsync(id);
                    var usr = _context.Users.FirstOrDefault(x => x.u_Id == transaction.u_id);
                    var bloods = _context.Bloods.FirstOrDefault(x => x.b_Id == transaction.b_id);
                    var bloodd = await _context.Bloods.FindAsync(transaction.b_id);

                    if (transaction.t_type == "Recieve")
                    {
                        if (usr.u_wallet < (transaction.t_bloodquantity * transaction.t_bloodprice))
                        {
                            _context.Update(transaction);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Closed", "Admin", new
                            {
                                t = 1,
                                u_id = usr.u_Id,
                                tid = transaction.t_Id

                            });

                        }
                        else
                        {

                            usr.u_wallet -= (transaction.t_bloodquantity * transaction.t_bloodprice);
                            _context.Update(usr);
                            await _context.SaveChangesAsync();
                            _context.Update(transaction);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Closed","Admin", new
                            {
                                t = 2,
                                u_id = usr.u_Id,
                                tid = transaction.t_Id

                            });
                        }
                    }
                    else if (transaction.t_type == "Donate")
                    {
                        bloods.b_quantity += transaction.t_bloodquantity;
                        _context.Update(bloods);
                        await _context.SaveChangesAsync();
                        usr.u_wallet += (transaction.t_bloodquantity * transaction.t_bloodprice);
                        _context.Update(usr);
                        await _context.SaveChangesAsync();

                        _context.Update(transaction);
                        await _context.SaveChangesAsync();
                        
                        return RedirectToAction("Closed", "Admin", new
                        {
                            t = 3,
                            u_id = usr.u_Id,
                            tid = transaction.t_Id

                        });

                    }
                    await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.t_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        public async Task<IActionResult> Closed(int t, int u_id, int tid)
        {
            ViewData["auth"] = true;
            var transactionn = await _context.Transactions.FindAsync(tid);
            var usr = _context.Users.FirstOrDefault(x => x.u_Id == u_id);
            ViewBag.TID = tid;
            ViewBag.u_name = usr.u_Name;
            ViewBag.b_type = transactionn.t_bloodtype;
            ViewBag.q = transactionn.t_bloodquantity;
            ViewBag.bill = (transactionn.t_bloodquantity * transactionn.t_bloodprice);
            ViewBag.rem = usr.u_wallet;

            ViewBag.t = t;
            return View();


        }


        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["auth"] = true;
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.t_Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            ViewData["auth"] = true;
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Transaction", "Admin", new
            {
                auth = true
            }) ;
            
        }

        private bool TransactionExists(int? id)
        {
            return (_context.Transactions?.Any(e => e.t_Id == id)).GetValueOrDefault();
        }



    }

}
