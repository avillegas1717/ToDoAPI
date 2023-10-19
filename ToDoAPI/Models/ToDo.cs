using System;
using System.Collections.Generic;

namespace ToDoAPI.Models
{
    public partial class ToDo
    {
        public int ToDoId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Done { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}
