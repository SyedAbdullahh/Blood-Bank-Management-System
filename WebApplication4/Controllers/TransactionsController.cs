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
    public class TransactionsController : Controller
    {
        private readonly ProjDbContext _context;

        public TransactionsController(ProjDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index(bool param1,)
        //{
        //    ViewBag.param = param1;
        //    if (param1)
        //    {


        //        return _context.Transactions != null ?
        //               View(await _context.Transactions.ToListAsync()) :
        //               Problem("Entity set 'ProjDbContext.Transactions'  is null.");



        //    }
        //    return NotFound();


        //}
        public async Task<IActionResult> Index(bool auth,int id,string? searchStringg)
        {
            if (searchStringg == null)
            {
                ViewData["param1"] = "Khaali";
            }
            ViewData["param1"] = searchStringg;
            ViewData["auth"] = auth;
            ViewData["id"]=id;

            //if(param1)
            //{



            //return _context.Transactions != null ?
            //       View(await _context.Transactions.ToListAsync()) :
            //       Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            var movies = from m in _context.Transactions
                         where m.h_id == id
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
            
            return NotFound();


        }

        public async Task<IActionResult> Index_s(string? searchStringg)
        {
            if(searchStringg==null)
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
                   bool flag= searchStringg.All(Char.IsDigit);
                    if(flag)
                    {
                        int num = Int32.Parse(searchStringg);
                        movies = movies.Where(s => s.t_Id==num);
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
        public async Task<IActionResult> Index_u(int wallet,string name,bool auth,int id)
        {
            if(auth)
            {
                ViewData["wallet"] = wallet;
                ViewData["name"] = name;
                ViewData["id"] = id;
                ViewData["auth"] = auth;
                return _context.Transactions != null ?
                          View(await _context.Transactions.Where(x => x.u_id == id).ToListAsync()) :
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

        // GET: Transactions/Create
        public IActionResult Create(int param1,int param2,int u_id,string param4,int wallet,string name,bool auth,int rn)
        {
            //param1=b_id
            //param2=h_id
            var prod = _context.Users.FirstOrDefault(m => m.u_Id == rn);
            var bloodd = _context.Bloods.FirstOrDefault(m => m.b_Id == param1);
            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = rn;
            ViewData["auth"] = auth;

            if (prod == null||bloodd==null)
            {
                return NotFound();
            }

            ViewBag.u_id = rn;
            ViewBag.u_name = prod.u_Name;
            ViewBag.h_id = param2;
            ViewBag.blood = bloodd.b_type;
            ViewBag.t_type = param4;
            ViewBag.b_price = bloodd.b_price;
            ViewBag.av_q = bloodd.b_quantity;
            ViewBag.b_id = bloodd.b_Id;

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("t_Id,u_name,u_id,h_id,t_bloodtype,t_status,t_type,t_bloodquantity,t_bloodprice,t_date,b_id")] Transaction transaction)
        {
            transaction.t_status = "Pending";
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
      
                var blood = _context.Bloods.FirstOrDefault(x => x.b_Id == transaction.b_id);
               
                blood.b_quantity -= transaction.t_bloodquantity;
                _context.Update(blood);
                await _context.SaveChangesAsync();

                return RedirectToAction("Confirmation", "Transactions", new
                {
                   
                    TID = transaction.t_Id,
                    HID= transaction.h_id,
                   

                });
            }
            
            return View(transaction);
            
        }

        public IActionResult Create_d( int param2, int id, string param4,int wallet, string name, bool auth)
        {
            //param2=>Hospital id
            //param3=>User id
            //param4=>Transaction Type
            var prod = _context.Users.FirstOrDefault(m => m.u_Id == id);
            var bloodd = _context.Bloods.FirstOrDefault(m => m.b_type==prod.u_bloodgroup&&m.h_id==param2);
            ViewData["wallet"] = wallet;
            ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["auth"] = auth;

            if (prod == null || bloodd == null)
            {
                return NotFound();
            }

            ViewBag.u_id = id;
            ViewBag.u_name = prod.u_Name;
            ViewBag.h_id = param2;
            ViewBag.blood = bloodd.b_type;
            ViewBag.t_type = param4;
            ViewBag.b_price = bloodd.b_price;
            ViewBag.av_q = bloodd.b_quantity;
            ViewBag.b_id = bloodd.b_Id;

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_d([Bind("t_Id,u_name,u_id,h_id,t_bloodtype,t_status,t_type,t_bloodquantity,t_bloodprice,t_date,b_id")] Transaction transaction)
        {
            var ussr = _context.Users.FirstOrDefault(x => x.u_Id == transaction.u_id);
            ////bool flag = _context.Users.Any(x => x.u_Email == usrr.u_Email && x.u_Password == usrr.u_Password);
            ////bool flagg = (_context.Users?.Any(x => x.u_Email == usrr.u_Email && x.u_Password == usrr.u_Password)).GetValueOrDefault();
            var usr = _context.Users.Where(x => x.u_Id==transaction.u_id).FirstOrDefault();
            transaction.t_status = "Pending";
            if (ModelState.IsValid)
            {
                _context.Add(transaction);

               
                await _context.SaveChangesAsync();
             

                return RedirectToAction("Confirmation", "Transactions", new
                {
                  
                    TID = transaction.t_Id,
                    HID = transaction.h_id,
                    

                });
            }

            return View(transaction);

        }
        public async Task<IActionResult> Confirmation(int TID,int HID)
        {
           
            ViewBag.TID = TID;
            var t = _context.Transactions.FirstOrDefault(x => x.t_Id == TID);

            var ussr =  _context.Users.FirstOrDefault(x => x.u_Id == t.u_id);
            ViewData["wallet"] = ussr.u_wallet;
            ViewData["name"] = ussr.u_Name;
            ViewData["id"] = ussr.u_Id;
            ViewData["auth"] = true;

            var h = _context.Hospitals.FirstOrDefault(x => x.h_Id == HID);
            ViewBag.Hospital = h.h_name;
            return View();   
        }


        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id,bool auth,int h_id)
        {
            
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);

            ViewData["id"] = transaction.h_id;
            ViewData["auth"] = auth;
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
        public async Task<IActionResult> Edit(int? id, [Bind("t_Id,u_name,u_id,h_id,t_bloodtype,t_status,t_type,t_bloodquantity,t_bloodprice,t_date,b_id")] Transaction transaction)
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
                return RedirectToAction("Index", "Transactions", new
                {

                    auth = true,
                    id = transaction.h_id


                }) ;

                
            }
            return View(transaction);
        }
        public async Task<IActionResult> Close(int? id,bool auth,int h_id)
        {
                       if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            var ussr = _context.Users.FindAsync(transaction.u_id);
            ViewData["auth"] = auth;
            ViewData["id"] = transaction.h_id;

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
            transaction.t_status = "Closed";
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
                    var blood = _context.Bloods.FirstOrDefault(x => x.b_Id == transaction.b_id);
                    
                    if(transaction.t_type=="Recieve")
                    {
                        if (usr.u_wallet < (transaction.t_bloodquantity * transaction.t_bloodprice))
                        {
                            _context.Update(transaction);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Closed", "Transactions", new
                            {
                                t = 1,
                                u_id=usr.u_Id,
                                tid=transaction.t_Id,
                                hid=transaction.h_id

                            }) ;

                        }
                        else
                        {
                          
                            usr.u_wallet -= (transaction.t_bloodquantity * transaction.t_bloodprice);
                            _context.Update(usr);
                            await _context.SaveChangesAsync();
                            _context.Update(transaction);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Closed", "Transactions", new
                            {
                                t = 2,
                                u_id = usr.u_Id,
                                tid = transaction.t_Id,
                                hid = transaction.h_id

                            });
                        }
                    }
                    else if (transaction.t_type == "Donate")
                    {
                        usr.u_wallet += (transaction.t_bloodquantity * transaction.t_bloodprice);
                        _context.Update(usr);
                        await _context.SaveChangesAsync();

                        _context.Update(transaction);
                        await _context.SaveChangesAsync();
                        blood.b_quantity += transaction.t_bloodquantity;
                        _context.Update(blood);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Closed", "Transactions", new
                        {
                            t = 3,
                            u_id = usr.u_Id,
                            tid = transaction.t_Id,
                            hid = transaction.h_id


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
                return RedirectToAction("Index", "Transactions", new
                {

                    auth = true,
                    id = transaction.h_id


                });


            }
            return View(transaction);
        }
        public async Task<IActionResult> Closed(int t,int u_id,int tid,int hid)
        {
           
            var transactionn = await _context.Transactions.FindAsync(tid);
            
            var usr = _context.Users.FirstOrDefault(x => x.u_Id == u_id);
            var tr = _context.Transactions.FirstOrDefault(x => x.t_Id == tid);
            ViewBag.TID = tid;
            ViewBag.u_name = usr.u_Name;
            ViewBag.b_type = transactionn.t_bloodtype;
            ViewBag.q = transactionn.t_bloodquantity;
            ViewBag.bill=(transactionn.t_bloodquantity*transactionn.t_bloodprice);
            ViewBag.rem=usr.u_wallet;
            
            ViewData["id"] = hid;
            ViewData["auth"] = true;
            ViewBag.t = t;
            return View();


        }


        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id,bool auth,int h_id)
        {
            ViewData["auth"] = auth;
            
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
            ViewData["auth"] = auth;
            ViewData["id"] = transaction.h_id;

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ProjDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            int s = transaction.h_id;
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Transactions", new
            {

                auth = true,
                id = s


            });
            
        }

        private bool TransactionExists(int? id)
        {
          return (_context.Transactions?.Any(e => e.t_Id == id)).GetValueOrDefault();
        }
    }
}
