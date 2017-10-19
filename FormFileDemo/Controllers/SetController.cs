using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace FormFileDemo.Controllers
{
    public class SetController : Controller
    {
        public IHostingEnvironment _env { get; set; }

        public SetController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Set([FromQuery] string someKey, [FromQuery] string someValue)
        {
            ViewBag.SomeKey = someKey;
            ViewBag.SomeValue = someValue;
            TempData[$"{someKey}"] = someValue;

            string UserDataPath = _env.ContentRootPath + "/App_Data/UserData.txt";

            string UserData = someKey + "=" + someValue;

            System.IO.File.Delete(UserDataPath);

            System.IO.File.WriteAllText(UserDataPath, UserData);

            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get([FromQuery] string key)
        {
            ViewBag.SomeKey = key;
            TempData["Value"] = TempData[$"{key}"];

            string UserDataPath = _env.ContentRootPath + "/App_Data/UserData.txt";

            string[] UserData = System.IO.File.ReadAllText(UserDataPath).Split('=');

            ViewBag.FileKey = UserData[0];
            ViewBag.FileValue = UserData[1];

            return View();
        }
    }
}