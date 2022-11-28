using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentControlApp.Data;
using StudentControlApp.Models;

namespace StudentControlApp.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly DBcontext _context;

        public DisciplinesController(DBcontext context)
        {
            _context = context;
        }

        // GET: Disciplines
        public async Task<IActionResult> Index()
        {
            var dBcontext = _context.Disciplines.Include(d => d.Group).Include(d => d.TeacherData);
            return View(await dBcontext.ToListAsync());
        }

        // GET: Disciplines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .Include(d => d.Group).Include(d => d.TeacherData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // GET: Disciplines/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.TeacherDatas, "Id", "Name");
            return View();
        }

        // POST: Disciplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GroupId,TeacherDataId")] Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", discipline.GroupId);
            return View(discipline);
        }

        // GET: Disciplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", discipline.GroupId);
            ViewData["TeacherdataId"] = new SelectList(_context.TeacherDatas, "Id", "Name", discipline.TeacherDataId);
            return View(discipline);
        }

        // POST: Disciplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GroupId,TeacherDataId")] Discipline discipline)
        {
            if (id != discipline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", discipline.GroupId);
            return View(discipline);
        }

        // GET: Disciplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .Include(d => d.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disciplines == null)
            {
                return Problem("Entity set 'DBcontext.Disciplines'  is null.");
            }
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineExists(int id)
        {
          return _context.Disciplines.Any(e => e.Id == id);
        }
    }
}
