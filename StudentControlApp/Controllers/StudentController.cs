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
using StudentControlApp.enums;

namespace StudentControlApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly DBcontext _context;
        UserManager<IdentityUser> _userManager;

        public StudentController(DBcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var dBcontext = _context.Studentdatas.Include(s => s.Group).Include(s => s.User);
            return View(await dBcontext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Studentdatas == null)
            {
                return NotFound();
            }

            var studentdata = await _context.Studentdatas
                .Include(s => s.Group)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentdata == null)
            {
                return NotFound();
            }

            return View(studentdata);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["Userid"] = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Type,GroupId,Userid")] Studentdata studentdata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentdata);
                await _context.SaveChangesAsync();

                var dbc = _context.Disciplines.Select(q => new Discipline
                {
                    Id = q.Id,
                    Name = q.Name,
                    GroupId = q.GroupId,
                    TeacherDataId = q.TeacherDataId
                }).Where(q => q.GroupId == studentdata.GroupId).ToList();
                foreach (var item in dbc)
                {
                    var subject = new StudentSubject();
                    subject.Name = item.Name;
                    subject.Status = SubjectStatus.Предстоящий;
                    subject.StudentdataId = studentdata.Id;
                    _context.StudentSubjects.Add(subject);
                await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", studentdata.GroupId);
            ViewData["Userid"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", studentdata.Userid);
            return View(studentdata);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Studentdatas == null)
            {
                return NotFound();
            }

            var studentdata = await _context.Studentdatas.FindAsync(id);
            if (studentdata == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", studentdata.GroupId);
            ViewData["Userid"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", studentdata.Userid);
            return View(studentdata);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Type,GroupId,Userid")] Studentdata studentdata)
        {
            if (id != studentdata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentdata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentdataExists(studentdata.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", studentdata.GroupId);
            ViewData["Userid"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", studentdata.Userid);
            return View(studentdata);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Studentdatas == null)
            {
                return NotFound();
            }

            var studentdata = await _context.Studentdatas
                .Include(s => s.Group)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentdata == null)
            {
                return NotFound();
            }

            return View(studentdata);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Studentdatas == null)
            {
                return Problem("Entity set 'DBcontext.Studentdatas'  is null.");
            }
            var studentdata = await _context.Studentdatas.FindAsync(id);
            if (studentdata != null)
            {
                _context.Studentdatas.Remove(studentdata);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentdataExists(int id)
        {
          return _context.Studentdatas.Any(e => e.Id == id);
        }
    }
}
