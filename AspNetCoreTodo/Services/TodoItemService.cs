﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await _context.Items.Where(x => x.IsDone == false && x.OwnerId == user.Id).ToArrayAsync();
        }

        public async Task<bool> AddItemAsync(NewTodoItemViewModel newItem, ApplicationUser user)
        {
            TodoItem item = new TodoItem()
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.UtcNow.AddDays(3),
                OwnerId = user.Id
            };

            _context.Items.Add(item);

            int saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;

        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            TodoItem item = await _context.Items.Where(x => x.Id == id && x.OwnerId == user.Id).SingleOrDefaultAsync();
            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;

        }
    }
}
