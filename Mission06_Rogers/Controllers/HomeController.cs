using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission06_Rogers.Models;


namespace Mission06_Rogers.Controllers;

public class HomeController : Controller
{
    private MovieCollectionContext _context;

    public HomeController(MovieCollectionContext temp) //Constructor
    {
        _context = temp;
    }
    
    public IActionResult Index() //displays homepage
    {
        return View();
    }

    public IActionResult GetToKnow() // displays Get To Know Joel page
    {
        return View();
    }
    
    [HttpGet] // gets addmovie view data
    public IActionResult AddMovie()
    {
        ViewBag.Categories = _context.Categories
            .OrderBy(c => c.CategoryId)
            .ToList();

        return View("AddMovie", new Movie());
    }
    
    [HttpPost] // Posting form data to sqlite database
    public IActionResult Create(Movie movie) // Adds Movie Record
    {
    
        
        if (ModelState.IsValid)
        {

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return View("Confirmation", movie);
        }
        else
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(c => c.CategoryId)
                .ToList();

            return View("AddMovie", movie);
        }

    }
    
    [HttpGet]
    public IActionResult MovieList() // Gets Movie List
    {
        var movies = _context.Movies
            .Include(m => m.Category)
            .OrderBy(m => m.Title).ToList();

        return View(movies);
    }
    [HttpGet]
    public IActionResult Edit(int id) //Gets movie record to edit
    {
        var recordToEdit = _context.Movies
            .Single(m => m.MovieId == id);

        ViewBag.Categories = _context.Categories.OrderBy(c => c.CategoryId).ToList();

        return View("AddMovie", recordToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Movie updatedInfo) // updates movie info
    {
        _context.Movies.Update(updatedInfo);
        _context.SaveChanges();
        return RedirectToAction("MovieList");
    }
    
    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var recordToDelete = _context.Movies
            .Single(m => m.MovieId == id);


        return View("ConfirmDelete", recordToDelete);
    }

    [HttpPost]
    public IActionResult Delete(Movie recordToDelete) // Deletes specified movie record
    {
        _context.Movies.Remove(recordToDelete);
        _context.SaveChanges();
        return RedirectToAction("MovieList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

