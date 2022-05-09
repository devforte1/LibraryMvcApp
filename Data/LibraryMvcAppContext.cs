using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryMvcApp.Models;

namespace LibraryMvcApp.Data
{
    public class LibraryMvcAppContext : DbContext
    {
        public LibraryMvcAppContext (DbContextOptions<LibraryMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryMvcApp.Models.RegistrantModel> RegistrantModel { get; set; }

        public DbSet<LibraryMvcApp.Models.MediaModel> InventoryModel { get; set; }
    }
}
