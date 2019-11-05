﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisSharp.Models;

namespace ChrisSharp.Models
{
    public class FakeProductRepository : IUserRepository
    {
        public IQueryable<User> Users => new List<User> {
        new User { Name = "Football", Price = 25 },
        new User { Name = "Surf board", Price = 179 },
        new User { Name = "Running shoes", Price = 95 }
        }.AsQueryable<User>();
    }
}
