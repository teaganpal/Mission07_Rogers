using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Rogers.Models;
using SQLitePCL;

namespace Mission06_Rogers.Controllers;

public class HomeController : Controller
{
    private readonly MovieCollectionContext _context;

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
        return View();
    }
    
    [HttpPost] // Posting form data to sqlite database
    public IActionResult Create(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        
        TempData["SuccessMessage"] = "Movie was added successfully!";

        return View("AddMovie");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

