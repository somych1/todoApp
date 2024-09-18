using todoMVCApp.Models;

namespace todoMVCApp.Data;

public interface ITodoRepository
{
    Task<IEnumerable<ToDo>> GetTodos();
    Task<ToDo> GetTodoById(int id);
    void AddTodoAsync(ToDo todo);
    void UpdateTodo(ToDo todo);
    void DeleteTodo(ToDo toDo);
    Task<ToDo> FindTodo(int id);
    Task<ToDo> FindTodoToDelete(int id);
    bool IsToDoExists(int id);
}