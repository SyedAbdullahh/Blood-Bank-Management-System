using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ProjDbContext _context;


        public EmployeeController(ProjDbContext context)
        {
            _context = context;
        }
        public IActionResult Privacy(bool auth,int id)
        {
            ViewData["auth"] = auth;
            ViewData["id"] = id;
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Employee usrr)
        {
            string a = "Active";
            // Code that verifies the email and password correction and takes user to other page
            bool flag = _context.Employees.Any(x => x.e_Id == usrr.e_Id &&x.e_Password == usrr.e_Password);
            bool flagg = (_context.Employees?.Any(x => x.e_Id == usrr.e_Id && x.e_Password == usrr.e_Password)).GetValueOrDefault();
            var usr = _context.Employees.Where(x => x.e_Id == usrr.e_Id && x.e_Password == usrr.e_Password).FirstOrDefault();

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath)
            .AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Employees  where e_Id ='" + usrr.e_Id + "' and e_Password='" + usrr.e_Password + "'", con);
            SqlDataReader sddr = cmd.ExecuteReader();
            if (usr != null || sddr.Read() || flag || flagg)
            {
                return RedirectToAction("Dashboard", "Employee", new
                {

                    auth = true,
                    id = usr.e_centre_id,
                    
                }) ;
            }
            else
            {

                return RedirectToAction("Login", "Employee");
            }


        }
        public IActionResult Dashboard(bool auth,int id)
        {


            ViewData["auth"] = auth;
            ViewData["id"]=id;
            if (auth)
            {
                return View();
            }
            else
            {
                return NotFound();
            }

        }


    }
}
