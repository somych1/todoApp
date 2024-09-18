using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using todoMVCApp.Data;
using todoMVCApp.Models;

namespace todoMVCApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoRepository _todoRepository;

        public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("-----Index page loaded to list all the tasks");
            return View(await _todoRepository.GetTodos());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            _logger.LogInformation($"-----Details page loaded to show the details of a task with id: {id}");
            if (id == null)
            {
                return NotFound();
            }
            var toDo = await _todoRepository.GetTodoById((int)id);
            if (toDo == null)
            {
                _logger.LogError($"-----Task with id: {id} not found");
                return NotFound();
            }
            return View(toDo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            _logger.LogInformation("-----New task creation page loaded");
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Status)));
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Task,status")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _todoRepository.AddTodoAsync(toDo);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Status)));
            _logger.LogInformation("-----New task created");
            return View(toDo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation($"-----Edit page loaded to edit the task with id: {id}");
            if (id == null)
            {
                return NotFound();
            }
            var toDo = await _todoRepository.FindTodo((int)id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Status)));
            return View(toDo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Task,status")] ToDo toDo)
        {
            if (id != toDo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _todoRepository.UpdateTodo(toDo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _logger.LogInformation($"-----Task with id: {id} updated");
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Status)));
            return View(toDo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var toDo = await _todoRepository.FindTodoToDelete((int)id);
            if (toDo == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"-----Delete page loaded to delete the task with id: {id}");
            return View(toDo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _todoRepository.FindTodo(id);
            if (toDo != null)
            {
                // _context.ToDo.Remove(toDo);
            }

            // await _context.SaveChangesAsync();
            _todoRepository.DeleteTodo(toDo);
            _logger.LogInformation($"-----Task with id: {id} found and deleted");
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            _logger.LogInformation($"-----Checking if task with id: {id} exists");
            // return _context.ToDo.Any(e => e.ID == id);
            return _todoRepository.IsToDoExists(id);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
