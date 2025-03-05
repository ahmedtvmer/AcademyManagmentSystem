using ITI.Data;
using ITI.Models;
using ITI2_.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITI2_.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext context = new AppDbContext();
        public IActionResult Index(string searchTerm = "", int Page = 1, int PageSize = 10)
        {
            IQueryable<Department> dept = context.Departments;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                dept = dept.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = dept.Count();
            ViewBag.EntityName = "Departments";
            ViewBag.SearchTerm = searchTerm;
            ViewBag.TotalItems = totalItems;
            ViewBag.CurrentPage = Page;
            ViewBag.TotalPages = Math.Ceiling((decimal)totalItems / PageSize);
            ViewBag.PageSize = PageSize;
            return View(dept.Skip((Page - 1) * PageSize).Take(PageSize).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Department());
        }
        [HttpPost]
        public IActionResult SaveAdd(Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var dept = context.Departments
                .Include(d => d.Instructors)
                .Include(d => d.Trainees)
                .Include(d => d.Courses).
                FirstOrDefault(d => d.Id == id);
            if (dept != null)
            {
                return View(dept);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SaveUpdate(Department deptFromRequest)
        {
            var deptFromDatabase = context.Departments
                .Include(d => d.Instructors)
                .Include(d => d.Trainees)
                .Include(d => d.Courses)
                .FirstOrDefault(d => d.Id == deptFromRequest.Id);

            if (deptFromDatabase != null)
            {
                deptFromDatabase.Name = deptFromRequest.Name;
                deptFromDatabase.ManagerName = deptFromRequest.ManagerName;

                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var dept = context.Departments.FirstOrDefault(d => d.Id == id);
            if(dept != null)
            {
                context.Departments.Remove(dept);
                context.Database.ExecuteSql($"DBCC CHECKIDENT ('Departments', RESEED, {dept.Id - 1})");
                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
