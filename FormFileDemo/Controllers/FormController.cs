using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using FormFileDemo.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;

namespace FormFileDemo.Controllers
{
    public class FormController : Controller
    {
        public IHostingEnvironment _env { get; set; }

        public FormController(IHostingEnvironment env)
        {
            _env = env;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Customer customer)
        {
            TempData["CompanyName"] = customer.CompanyName;
            TempData["ContactName"] = customer.ContactName;
            TempData["EmployeeCount"] = customer.EmployeeCount;

            string UserDataPath = _env.ContentRootPath + "/App_Data/UserData.txt";

            string UserData = customer.CompanyName + ','
                + customer.ContactName + ','
                + customer.EmployeeCount.ToString();

            System.IO.File.Delete(UserDataPath);

            System.IO.File.WriteAllText(UserDataPath, UserData);

            return View();
        }
    }
}