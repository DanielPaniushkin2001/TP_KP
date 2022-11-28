using Microsoft.EntityFrameworkCore;
using StudentControlApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace StudentControlApp.Data
{
    public class DBcontext : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups  { get; set; }
        public DbSet<Studentdata> Studentdatas  { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<TeacherData> TeacherDatas  { get; set; }


    public DBcontext(DbContextOptions<DBcontext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
