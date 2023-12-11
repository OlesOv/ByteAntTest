using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ByteAntTest.Data;
using ByteAntTestTask.Models;

namespace ByteAntTest.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ByteAntTestContext _context;

        public PositionsController(ByteAntTestContext context)
        {
            _context = context;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var byteAntTestContext = _context.Position.Include(p => p.Employee).Include(p => p.ReportsTo).Include(p => p.Role);
            return View(await byteAntTestContext.ToListAsync());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .Include(p => p.Employee)
                .Include(p => p.ReportsTo)
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.PositionID == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            ViewData["ReportsToID"] = new SelectList(_context.Position, "PositionID", "PositionID");
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID");
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionID,EmployeeID,RoleID,ReportsToID")] Position position)
        {
            if (ModelState.IsValid)
            {
                position.PositionID = Guid.NewGuid();
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", position.EmployeeID);
            ViewData["ReportsToID"] = new SelectList(_context.Position, "PositionID", "PositionID", position.ReportsToID);
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", position.RoleID);
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", position.EmployeeID);
            ViewData["ReportsToID"] = new SelectList(_context.Position, "PositionID", "PositionID", position.ReportsToID);
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", position.RoleID);
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PositionID,EmployeeID,RoleID,ReportsToID")] Position position)
        {
            if (id != position.PositionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.PositionID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", position.EmployeeID);
            ViewData["ReportsToID"] = new SelectList(_context.Position, "PositionID", "PositionID", position.ReportsToID);
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", position.RoleID);
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .Include(p => p.Employee)
                .Include(p => p.ReportsTo)
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.PositionID == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Position == null)
            {
                return Problem("Entity set 'ByteAntTestContext.Position'  is null.");
            }
            var position = await _context.Position.FindAsync(id);
            if (position != null)
            {
                _context.Position.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Tree()
        {
            if (_context.Position == null)
            {
                return Problem("Entity set 'ByteAntTestContext.Position'  is null.");
            }
            var positions = _context.Position
                .Include(p => p.ReportsTo).ThenInclude(p => p.Employee)
                .Include(p => p.Role)
                .Include(p => p.Subordinates).ThenInclude(p => p.Employee)
                //.Where(pos => pos.ReportsTo == null)
                .ToList();
            return View("PositionTree", positions.Where(pos => pos.ReportsToID == null));
        }

        private bool PositionExists(Guid id)
        {
          return (_context.Position?.Any(e => e.PositionID == id)).GetValueOrDefault();
        }
    }
}
