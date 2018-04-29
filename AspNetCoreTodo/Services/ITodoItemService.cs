﻿using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(NewTodoItemViewModel newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
