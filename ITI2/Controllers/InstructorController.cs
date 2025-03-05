using ITI.Data;
using ITI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ITI2_.Controllers
{
    public class InstructorController : Controller
    {
        AppDbContext context = new();
        public IActionResult Index(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            IQueryable<Instructor> inst = context.Instructors
                .Include(i => i.Dept)
                .Include(i => i.Course);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                inst = inst.Where(i => i.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = context.Instructors.Count();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.EntityName = "Instructors";
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            ViewBag.TotalItems = totalItems;
            return View(inst.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }
        public IActionResult Details(int id)
        {
            var inst = context.Instructors.FirstOrDefault(i => i.Id == id);
            return View(inst);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var inst = context.Instructors.FirstOrDefault(i => i.Id == id);
            return inst != null ? View(inst) : RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SaveUpdate(Instructor instFromRequest)
        {
            var instFromDatabase = context.Instructors
                .Include(i => i.Dept)
                .Include(i => i.Course)
                .FirstOrDefault(i => i.Id == instFromRequest.Id);

            instFromDatabase.Name = instFromRequest.Name;
            instFromDatabase.Salary = instFromRequest.Salary;
            instFromDatabase.Address = instFromRequest.Address;
            instFromDatabase.DeptId = instFromRequest.DeptId;
            instFromDatabase.CourseId = instFromRequest.CourseId;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var inst = context.Instructors.FirstOrDefault(i => i.Id == id);
            if (inst != null)
            {
                context.Instructors.Remove(inst);
                context.Database.ExecuteSql($"DBCC CHECKIDENT ('Instructors', RESEED, {inst.Id - 1})");
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Instructor());
        }
        [HttpPost]
        public IActionResult Add(Instructor inst)
        {
            inst.ImageURL = "1.jpg";
            context.Instructors.Add(inst);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
