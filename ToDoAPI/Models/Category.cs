using System;
using System.Collections.Generic;

namespace ToDoAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            ToDos = new HashSet<ToDo>();
        }

        public int CategoryId { get; set; }
        public string CatName { get; set; } = null!;
        public string? CatDesc { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
