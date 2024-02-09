using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api_mvc.Data;
using api_mvc.Models;
using Microsoft.AspNetCore.Identity;
using api_mvc.Data.Migrations;


namespace api_mvc.Controllers
{
    [Authorize]
    public class TournamentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TournamentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TournamentViewModels
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TournamentViewModel.ToListAsync());
        }


        // GET: TournamentViewModels/Details/5
        [AllowAnonymous]
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

            var players = await _context.PlayerViewModel
                .Where(p => p.TournamentId == tournamentViewModel.Id)
                .ToListAsync();

            return View(players);
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
        public async Task<IActionResult> Create([Bind("Id,Name,PlayersNumber,RoundsNumber")] Tournament tournamentViewModel)
        {
            if (ModelState.IsValid)
            {
                var ownerUserId = _userManager.GetUserId(User);
                tournamentViewModel.OwnerUserId = ownerUserId;

                tournamentViewModel.IsFinished = false;
                tournamentViewModel.IsStarted = false;
                tournamentViewModel.CreatedDate = DateTime.Now;


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

            var userId = _userManager.GetUserId(User);
            if (tournamentViewModel.OwnerUserId != userId)
            {
                return Forbid();
            }

            return View(tournamentViewModel);
        }


        // POST: TournamentViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsFinished,IsStarted,PlayersNumber,RoundsNumber")] Tournament tournamentViewModel)
        {
            if (id != tournamentViewModel.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (tournamentViewModel.OwnerUserId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ownerUserId = _userManager.GetUserId(User);
                    tournamentViewModel.OwnerUserId = ownerUserId;

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

            var userId = _userManager.GetUserId(User);
            if (tournamentViewModel.OwnerUserId != userId)
            {
                return Forbid();
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


        // GET: TournamentViewModels/MyTournaments
        public async Task<IActionResult> MyTournaments()
        {
            var userId = _userManager.GetUserId(User);

            // Retrieve tournaments where the owner is the current user
            var myTournaments = await _context.TournamentViewModel
                .Where(t => t.OwnerUserId == userId)
                .ToListAsync();

            return View(myTournaments);
        }

        // GET: Tournament/Players/5
        public async Task<IActionResult> Players(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var tournament = await _context.TournamentViewModel
            .FirstOrDefaultAsync(m => m.Id == id);
                if (tournament == null)
                {
                    return NotFound();
                }

            var players = await _context.PlayerViewModel
                .Where(p => p.TournamentId == tournament.Id)
                .ToListAsync();

            return View(players);
        }
    }
}
