using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_mvc.Data;
using api_mvc.Models;

namespace api_mvc.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayerViewModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlayerViewModel.Include(p => p.Tournament);
            return View(await applicationDbContext.ToListAsync());
        }

        public ActionResult PlayerList(int tournamentId)
        {
            var filteredPlayers = _context.PlayerViewModel.Where(p => p.TournamentId == tournamentId).ToList();

            return View(filteredPlayers);
        }

        // GET: PlayerViewModels/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerViewModel = await _context.PlayerViewModel
                .Include(p => p.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerViewModel == null)
            {
                return NotFound();
            }

            return View(playerViewModel);
        }

        // GET: PlayerViewModels/Create
        public IActionResult Create()
        {
            ViewData["TournamentId"] = new SelectList(_context.TournamentViewModel, "Id", "Id");
            return View();
        }

        // POST: PlayerViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long tournamentId, [Bind("Id,Name,Age, TournamentId")] Player playerViewModel)
        {
            if (ModelState.IsValid)
            {
                playerViewModel.TournamentId = tournamentId;
                _context.Add(playerViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["TournamentId"] = new SelectList(_context.TournamentViewModel, "Id", "Id", playerViewModel.TournamentId);
            return View(playerViewModel);
        }

        // GET: PlayerViewModels/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerViewModel = await _context.PlayerViewModel.FindAsync(id);
            if (playerViewModel == null)
            {
                return NotFound();
            }
            ViewData["TournamentId"] = new SelectList(_context.TournamentViewModel, "Id", "Id", playerViewModel.TournamentId);
            return View(playerViewModel);
        }

        // POST: PlayerViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Age")] Player playerViewModel)
        {
            if (id != playerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerViewModelExists(playerViewModel.Id))
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
            ViewData["TournamentId"] = new SelectList(_context.TournamentViewModel, "Id", "Id", playerViewModel.TournamentId);
            return View(playerViewModel);
        }

        // GET: PlayerViewModels/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerViewModel = await _context.PlayerViewModel
                .Include(p => p.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerViewModel == null)
            {
                return NotFound();
            }

            return View(playerViewModel);
        }

        // POST: PlayerViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var playerViewModel = await _context.PlayerViewModel.FindAsync(id);
            if (playerViewModel != null)
            {
                _context.PlayerViewModel.Remove(playerViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerViewModelExists(long id)
        {
            return _context.PlayerViewModel.Any(e => e.Id == id);
        }
    }
}
