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
    public class StudentSubjectsController : Controller
    {
        private readonly DBcontext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public StudentSubjectsController(DBcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StudentSubjects
        public async Task<IActionResult> Index(int? id)
        {
            var dBcontext = _context.StudentSubjects.Include(x => x.Studentdata);

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var teacher = await _context.TeacherDatas.FirstOrDefaultAsync(x => x.Userid == user.Id);
            
            ViewData["DisplayCheck"] = 0;
            if (id != null && teacher !=null)
            {
                dBcontext = _context.StudentSubjects.Where(x => x.StudentdataId == id).Include(x => x.Studentdata);
                    ViewData["DisplayCheck"] = 1;
                }
            else 
            { 
                if (user != null)
                {
                    var student = await _context.Studentdatas.FirstOrDefaultAsync(x => x.Userid == user.Id);
                
                    
                    if (student != null)
                    {
                        ViewData["DisplayCheck"] = 2;
                        dBcontext = _context.StudentSubjects.Where(x => x.StudentdataId == student.Id).Include(x => x.Studentdata);
                    }
                    else
                    {
                        ViewData["DisplayCheck"] = 1;
                        dBcontext = _context.StudentSubjects.Include(x => x.Studentdata);
                    }
            }
                }
            }
              return View(await dBcontext.ToListAsync());
        }

        // GET: StudentSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Studentdatas, "Id", "Name");
            return View();
        }

        // POST: StudentSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,StudentdataId")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentSubject);
        }

        // GET: StudentSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }
            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,StudentdataId")] StudentSubject studentSubject)
        {
            if (id != studentSubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSubjectExists(studentSubject.Id))
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
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentSubjects == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentSubjects == null)
            {
                return Problem("Entity set 'DBcontext.StudentSubjects'  is null.");
            }
            var studentSubject = await _context.StudentSubjects.FindAsync(id);
            if (studentSubject != null)
            {
                _context.StudentSubjects.Remove(studentSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSubjectExists(int id)
        {
          return _context.StudentSubjects.Any(e => e.Id == id);
        }
    }
}
