using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_1_taskflow.Data;
using project_1_taskflow.Models;

namespace project_1_taskflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Welcome");
            }

            var userId = _userManager.GetUserId(User);

            var totalTasks = _context.TodoItems.Where(t => t.UserId == userId).Count();
            var pendingTasks = _context.TodoItems.Where(t => t.UserId == userId && t.Status == TodoStatus.Pending).Count();
            var inProgressTasks = _context.TodoItems.Where(t => t.UserId == userId && t.Status == TodoStatus.InProgress).Count();
            var completedTasks = _context.TodoItems.Where(t => t.UserId == userId && t.Status == TodoStatus.Completed).Count();

            var upcomingTasks = _context.TodoItems
                .Include(t => t.Category)
                .Where(t => t.UserId == userId
                    && t.DueDate != null
                    && t.DueDate <= DateTime.Now.AddDays(3)
                    && t.Status != TodoStatus.Completed)
                .OrderBy(t => t.DueDate)
                .ToList();

            ViewBag.TotalTasks = totalTasks;
            ViewBag.PendingTasks = pendingTasks;
            ViewBag.InProgressTasks = inProgressTasks;
            ViewBag.CompletedTasks = completedTasks;
            ViewBag.UpcomingTasks = upcomingTasks;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
