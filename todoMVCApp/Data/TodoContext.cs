using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoMVCApp.Models;

namespace todoMVCApp.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<todoMVCApp.Models.ToDo> ToDo { get; set; } = default!;
    }
}
