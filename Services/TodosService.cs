using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BlazorApp.Services
{
    public class TodosService
    {
        private readonly TodoContext _todoContext;

        public TodosService(TodoContext todoContext)
        {
            this._todoContext = todoContext;
        }

        public async Task<List<Todo>> GetActiveTodosAsync()
        {
            var list = await _todoContext.Todos
                .Where(e => !e.Done) //Retornando apenas os falso
                .OrderBy(e => e.Priority)
                .ToListAsync();
            return list;
        }

        public async Task NewTodoAsync()
        {
            //var todo = new Todo
            //{
            //    Title = $"Tarefa {DateTime.Now}",
            //    Description = $"Tarefa {DateTime.Now}",
            //    CategoryId = 1,
            //};

            _todoContext.Todos.Add(new Todo
            {
                Title = $"Tarefa {DateTime.Now}",
                Description = $"Tarefa {DateTime.Now}",
                CategoryId = 1,
            });
            await _todoContext.SaveChangesAsync();
        }

        public async  Task<Todo> UpdateAsync(Todo todo) { 
            _todoContext.Update(todo);
            await _todoContext.SaveChangesAsync();
            return todo;
        }

        public async Task RemoveAsync(Todo todo)
        {
            _todoContext.Remove(todo);
            await _todoContext.SaveChangesAsync();
        }

    }
}
