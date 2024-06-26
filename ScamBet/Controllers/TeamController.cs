﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ScamBet.Entities;

namespace ScamBet.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly BookmacherDBContext _context;

        public TeamController(BookmacherDBContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: Team/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.team_ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("team_ID,name,wins,draws,loses")] Team team)
        {
            team.points = team.wins * 2 + team.draws;
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Team/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Team/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("team_ID,name,wins,draws,loses")] Team team)
        {
            if (id != team.team_ID)
            {
                return NotFound();
            }

            team.points = team.wins * 2 + team.draws;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.team_ID))
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
            return View(team);
        }

        // GET: Team/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.team_ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.team_ID == id);
        }
    }
}
