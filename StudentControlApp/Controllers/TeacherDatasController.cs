using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentControlApp.Data;
using StudentControlApp.Models;

namespace StudentControlApp.Controllers
{
    public class TeacherDatasController : Controller
    {
        private readonly DBcontext _context;
        UserManager<IdentityUser> _userManager;

        public TeacherDatasController(DBcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TeacherDatas
        public async Task<IActionResult> Index()
        {
            var dBcontext = _context.TeacherDatas.Include(t => t.User);
            return View(await dBcontext.ToListAsync());
        }

        // GET: TeacherDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherDatas == null)
            {
                return NotFound();
            }

            var teacherData = await _context.TeacherDatas
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherData == null)
            {
                return NotFound();
            }

            return View(teacherData);
        }

        // GET: TeacherDatas/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            return View();
        }

        // POST: TeacherDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,DegreeType,Userid")] TeacherData teacherData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_userManager.Users.ToList(), "Id", "Id", teacherData.Userid);
            return View(teacherData);
        }

        // GET: TeacherDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherDatas == null)
            {
                return NotFound();
            }

            var teacherData = await _context.TeacherDatas.FindAsync(id);
            if (teacherData == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_userManager.Users.ToList(), "Id", "Id", teacherData.Userid);
            return View(teacherData);
        }

        // POST: TeacherDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,DegreeType,Userid")] TeacherData teacherData)
        {
            if (id != teacherData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherDataExists(teacherData.Id))
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
            ViewData["Userid"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", teacherData.Userid);
            return View(teacherData);
        }

        // GET: TeacherDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherDatas == null)
            {
                return NotFound();
            }

            var teacherData = await _context.TeacherDatas
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherData == null)
            {
                return NotFound();
            }

            return View(teacherData);
        }

        // POST: TeacherDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherDatas == null)
            {
                return Problem("Entity set 'DBcontext.TeacherDatas'  is null.");
            }
            var teacherData = await _context.TeacherDatas.FindAsync(id);
            if (teacherData != null)
            {
                _context.TeacherDatas.Remove(teacherData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherDataExists(int id)
        {
          return _context.TeacherDatas.Any(e => e.Id == id);
        }
    }
}
