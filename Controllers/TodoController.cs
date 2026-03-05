using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_1_taskflow.Data;
using project_1_taskflow.Models;

namespace project_1_taskflow.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var allTasks = _context.TodoItems
                .Include(t => t.Category)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();

            return View(allTasks);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoItem todoItem)
        {
            todoItem.UserId = _userManager.GetUserId(User);
            todoItem.CreatedAt = DateTime.Now;

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.TodoItems.Add(todoItem);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Tạo task thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(todoItem);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", todoItem.CategoryId);
            return View(todoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            todoItem.UserId = userId;

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                var existingTask = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                if (existingTask == null)
                {
                    return NotFound();
                }

                existingTask.Title = todoItem.Title;
                existingTask.Description = todoItem.Description;
                existingTask.DueDate = todoItem.DueDate;
                existingTask.Priority = todoItem.Priority;
                existingTask.Status = todoItem.Status;
                existingTask.CategoryId = todoItem.CategoryId;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật task thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", todoItem.CategoryId);
            return View(todoItem);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var todoItem = _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var todoItem = _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa task thành công!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var userId = _userManager.GetUserId(User);
            var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todoItem != null)
            {
                if (todoItem.Status == TodoStatus.Pending)
                {
                    todoItem.Status = TodoStatus.InProgress;
                }
                else if (todoItem.Status == TodoStatus.InProgress)
                {
                    todoItem.Status = TodoStatus.Completed;
                }
                else
                {
                    todoItem.Status = TodoStatus.Pending;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
