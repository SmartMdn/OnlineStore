﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

    }
}
