using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperTix.Data;
using SuperTix.Migrations;
using SuperTix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperTix.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly SuperTixContext _context;
        private readonly ILogger<GamesController> _logger;
        private readonly BlobContainerClient _containerClient;
        private readonly IConfiguration _configuration;


        public GamesController(SuperTixContext context, ILogger<GamesController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;

            var connectionString = _configuration["AzureStorage"];
            var containerName = "supertix-album-uploads";
            _containerClient = new BlobContainerClient(connectionString, containerName);
           
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
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
         
                    var fileUpload = game.FormFile;

                    // Create a unique name for the blob
                    string blobName = Guid.NewGuid().ToString() + "_" + fileUpload.FileName;

                    var blobClient = _containerClient.GetBlobClient(blobName);

                    using (var stream = fileUpload.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = fileUpload.ContentType });
                    }

                    //url to access the file
                    game.PhotoPath = blobClient.Uri.ToString();

                }
                else
                {
                    game.PhotoPath = "https://nscc0439218storageblob.blob.core.windows.net/supertix-album-uploads/placeholder.png";
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
        public async Task<IActionResult> Edit(int id, [Bind("GameId,CategoryId,GameName,Description,GameDate,CreateDate,Owner,Location,PhotoPath,FormFile")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Game.AsNoTracking().FirstOrDefaultAsync(game => game.GameId == id);
                    if (existing == null) return NotFound();

                    if (game.FormFile != null && game.FormFile.Length > 0)
                    {
                        // Generate unique name
                        var blobName = Guid.NewGuid().ToString() + "_" + game.FormFile.FileName;

                        //  Upload new file
                        var blobClient = _containerClient.GetBlobClient(blobName);
                        using (var stream = game.FormFile.OpenReadStream())
                        {
                            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = game.FormFile.ContentType });
                        }


                        // Delete old file if it exists and isn’t placeholder
                        if (!string.IsNullOrEmpty(existing.PhotoPath) && !existing.PhotoPath.EndsWith("placeholder.png"))
                        {
                            var oldBlobName = Path.GetFileName(new Uri(existing.PhotoPath).LocalPath);
                            var oldBlobClient = _containerClient.GetBlobClient(oldBlobName);
                            await oldBlobClient.DeleteIfExistsAsync();
                        }

                        // Update path to new blob
                        game.PhotoPath = blobClient.Uri.ToString();
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
