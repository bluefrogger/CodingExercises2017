using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormFileDemo.Models;

namespace FormFileDemo.Controllers
{
    public class MovieController : Controller
    {
        public List<Movie> MyMovies { get; set; }

        public MovieController()
        {
            MyMovies = new List<Movie>()
            {
                new Movie { MovieId = 1, MovieName = "Ben Hur", Genre = "Historical Fiction" },
                new Movie { MovieId = 2, MovieName = "Casablanca", Genre = "Romance" },
            };
        }
        
        // GET: Movie
        public ActionResult Index()
        {
            return View(MyMovies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            Movie Result = MyMovies.Single(mm => mm.MovieId == id);

            return View(Result);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}