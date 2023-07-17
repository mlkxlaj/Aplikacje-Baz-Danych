using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zadanie10.Models;


namespace Zadanie10.Data
{
    public class S24427Context : DbContext
    {
        public S24427Context (DbContextOptions<S24427Context> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}