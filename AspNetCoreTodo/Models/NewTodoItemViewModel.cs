using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class NewTodoItemViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
