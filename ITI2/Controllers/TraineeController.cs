using ITI.Data;
using ITI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI2_.Controllers
{
    public class TraineeController : Controller
    {
        AppDbContext context = new();
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            IQueryable<Trainee> dept = context.Trainees;


            int totalItems = dept.Count();
            ViewBag.EntityName = "Trainees";
            ViewBag.TotalItems = totalItems;
            ViewBag.CurrentPage = Page;
            ViewBag.TotalPages = Math.Ceiling((decimal)totalItems / PageSize);
            ViewBag.PageSize = PageSize;
            return View(dept.Skip((Page - 1) * PageSize).Take(PageSize).ToList());
        }
    }
}
