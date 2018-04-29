using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> AddItem(NewTodoItemViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool sucessful = await _todoItemService.AddItemAsync(newItem);
            if (!sucessful)
            {
                return BadRequest(new { error = "Could not add item" });
            }
            return Ok();
        }

        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            bool successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}