using System;
using System.Collections.Generic;

namespace ResourceAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Resources = new HashSet<Resource>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
