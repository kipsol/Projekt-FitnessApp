using FitnessApp.Data;
using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Exercises
        public async Task<IActionResult> Index()
        {
            var exercises = await _context.Exercises
                .OrderBy(e => e.Name)
                .ToListAsync();

            return View(exercises);
        }

        // GET: /Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                exercise.CreatedAt = DateTime.Now;

                _context.Exercises.Add(exercise);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(exercise);
        }
    }
}