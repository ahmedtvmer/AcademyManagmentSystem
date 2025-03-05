using ITI.Data;
using ITI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI2_.Controllers
{
    public class CourseController : Controller
    {
        AppDbContext context = new();
        public IActionResult Index(string searchTerm = "", int Page = 1, int PageSize = 10)
        {
            IQueryable<Course> dept = context.Courses;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                dept = dept.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            int totalItems = dept.Count();
            ViewBag.EntityName = "Courses";
            ViewBag.SearchTerm = searchTerm;
            ViewBag.TotalItems = totalItems;
            ViewBag.CurrentPage = Page;
            ViewBag.TotalPages = Math.Ceiling((decimal)totalItems / PageSize);
            ViewBag.PageSize = PageSize;
            return View(dept.Skip((Page - 1) * PageSize).Take(PageSize).ToList());
        }
    }
}
