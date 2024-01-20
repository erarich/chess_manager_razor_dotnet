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
    public class TournamentViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TournamentViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TournamentViewModel.ToListAsync());
        }

        // GET: TournamentViewModels/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentViewModel = await _context.TournamentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentViewModel == null)
            {
                return NotFound();
            }

            return View(tournamentViewModel);
        }

        // GET: TournamentViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TournamentViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsFinished,IsStarted,PlayersNumber,RoundsNumber,CreatedDate")] TournamentViewModel tournamentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournamentViewModel);
        }

        // GET: TournamentViewModels/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentViewModel = await _context.TournamentViewModel.FindAsync(id);
            if (tournamentViewModel == null)
            {
                return NotFound();
            }
            return View(tournamentViewModel);
        }

        // POST: TournamentViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsFinished,IsStarted,PlayersNumber,RoundsNumber,CreatedDate")] TournamentViewModel tournamentViewModel)
        {
            if (id != tournamentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentViewModelExists(tournamentViewModel.Id))
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
            return View(tournamentViewModel);
        }

        // GET: TournamentViewModels/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentViewModel = await _context.TournamentViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentViewModel == null)
            {
                return NotFound();
            }

            return View(tournamentViewModel);
        }

        // POST: TournamentViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tournamentViewModel = await _context.TournamentViewModel.FindAsync(id);
            if (tournamentViewModel != null)
            {
                _context.TournamentViewModel.Remove(tournamentViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentViewModelExists(long id)
        {
            return _context.TournamentViewModel.Any(e => e.Id == id);
        }
    }
}
