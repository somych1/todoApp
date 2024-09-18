using Microsoft.EntityFrameworkCore;
using todoMVCApp.Models;

namespace todoMVCApp.Data;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }

    public async void AddTodoAsync(ToDo todo)
    {
        _context.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async void DeleteTodo(ToDo toDo)
    {
        _context.ToDo.Remove(toDo);
        await _context.SaveChangesAsync();
    }


    public async Task<ToDo> GetTodoById(int id)
    {
        var toDo =  await _context.ToDo
                .FirstOrDefaultAsync(m => m.ID == id);
        return toDo;
    }

    public async Task<IEnumerable<ToDo>> GetTodos()
    {
        return await _context.ToDo.ToListAsync();
    }

    public async Task<ToDo> FindTodo(int id)
    {
        var toDo = await _context.ToDo.FindAsync(id);
        return toDo;
    }

    public async void UpdateTodo(ToDo todo)
    {
        _context.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task<ToDo> FindTodoToDelete(int id)
    {
        var toDo = await _context.ToDo.FirstOrDefaultAsync(m => m.ID == id);
        return toDo;
    }

    public bool IsToDoExists(int id)
    {
        return _context.ToDo.Any(e => e.ID == id);
    }
}
    