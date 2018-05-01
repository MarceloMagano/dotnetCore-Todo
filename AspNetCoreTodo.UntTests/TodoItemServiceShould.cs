using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTodo.UntTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            // Set up a context (connection to the DB) for writing
            using (var inMemoryContext = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(inMemoryContext);

                var fakeUser = new ApplicationUser
                {
                    Id = "fake-000",
                    UserName = "fake@fake"
                };

                await service.AddItemAsync(new NewTodoItemViewModel { Title = "Testing?" }, fakeUser);
            }
            // Use a separate context to read the data back from the DB
            using (var inMemoryContext = new ApplicationDbContext(options))
            {
                Assert.Equal(1, await inMemoryContext.Items.CountAsync());

                var item = await inMemoryContext.Items.FirstAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.False(item.IsDone);
                Assert.True(DateTimeOffset.Now.AddDays(3) - item.DueAt < TimeSpan.FromSeconds(1));
            }
        }
    }
}
