using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperTix.Data;
using SuperTix.Models;

namespace SuperTix.Controllers
{
    public class GamesController : Controller
    {
        private readonly SuperTixContext _context;
        private readonly ILogger<GamesController> _logger;


        public GamesController(SuperTixContext context, ILogger<GamesController> logger)
        {
            _context = context;
            _logger = logger;

        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var superTixContext = _context.Game.Include(g => g.Category);
            return View(await superTixContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,CategoryId,GameName,Description,GameDate,CreateDate,Owner,Location,PhotoPath,FormFile")] Game game)
        {
            if (ModelState.IsValid)
            {

                if (game.FormFile != null)
                {
                    // Generate unique filename
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(game.FormFile.FileName);

                    game.PhotoPath = "/photos/" + fileName;

                    // Save to wwwroot
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", fileName);
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await game.FormFile.CopyToAsync(stream);
                    }
              
                }

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", game.CategoryId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", game.CategoryId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,CategoryId,GameName,Description,GameDate,CreateDate,Owner,Location,FormFile")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Step 1: save the new file (optionally)
                    if (game.FormFile != null)
                    {
                        // 1. Generate a unique filename
                        var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(game.FormFile.FileName);

                        // 2. Save the new file to wwwroot/photos
                        var newPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", newFileName);

                        using (var stream = new FileStream(newPhotoPath, FileMode.Create))
                        {
                            await game.FormFile.CopyToAsync(stream);
                        }
                  


                        // 3. Delete the old file
                        if (!string.IsNullOrEmpty(game.PhotoPath))
                        {
                            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", game.PhotoPath.TrimStart('/'));


                            if (System.IO.File.Exists(oldFilePath))
                            {
                                _logger.LogInformation("Attempting delete: {Path}", oldFilePath);
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Update photo path
                        game.PhotoPath = "/photos/" + newFileName;

                    }

                    // Step 2: save the record
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", game.CategoryId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                _context.Game.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
