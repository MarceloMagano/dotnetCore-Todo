using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TodoItem> todoItems = await _todoItemService.GetIncompleteItemsAsync();
            TodoViewModel model = new TodoViewModel()
            {
                Items = todoItems
            };
            return View(model);
        }
    }
}