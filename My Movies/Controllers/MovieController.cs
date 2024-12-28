using My_Movies.Dal;
using My_Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Movies.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieContext context;
        public MovieController()
        {
            context=new MovieContext();
        }
        // GET: Movie
        public ActionResult Index()
        {
            return View(context.movies.Include("Genre").ToList());
        }
        [HttpGet]
        public ActionResult create()
        { 
            List<MovieGenre>movieGenres = context.movieGenres.ToList();
            List<SelectListItem>list = new List<SelectListItem>();
            foreach(MovieGenre mg in movieGenres)
            {
                if (mg.GenreId == 104)
                {
                    list.Add(new SelectListItem() { Text = mg.GenreName, Value = mg.GenreId.ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem() { Text = mg.GenreName, Value = mg.GenreId.ToString() });
                }
            }
            ViewBag.GenreId = list;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Movie m)
        {
            if (ModelState.IsValid)
            {
                context.movies.Add(m);
                context.SaveChanges();
                //return RedirectToAction("Index");
                return View("Index", context.movies.Include("Genre").ToList());
            }
            else
            {
                return View(m);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Movie m = context.movies.Find(id);
            List<MovieGenre> movieGenres = context.movieGenres.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (MovieGenre mg in movieGenres)
            {
                if (mg.GenreId == m.GenreId)
                {
                    list.Add(new SelectListItem() { Text = mg.GenreName, Value = mg.GenreId.ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem() { Text = mg.GenreName, Value = mg.GenreId.ToString() });
                }
            }
            ViewBag.GenreId = list;

            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(Movie m)
        {
            Movie x = context.movies.Find(m.MovieId);
            x.MovieName = m.MovieName;
            x.MovieDescription = m.MovieDescription;
            x.GenreId = m.GenreId;
            x.YearOfRelease = m.YearOfRelease;
            context.SaveChanges();

            return View("Index", context.movies.Include("Genre").ToList());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Movie m = context.movies.Include("Genre").First(x => x.MovieId == id);
            return View(m);
        }

        public ActionResult Delete(int id)
        {
            Movie m = context.movies.First(x => x.MovieId == id);
            context.movies.Remove(m);
            context.SaveChanges();

            return View("Index", context.movies.Include("Genre").ToList());
        }
    }

}
