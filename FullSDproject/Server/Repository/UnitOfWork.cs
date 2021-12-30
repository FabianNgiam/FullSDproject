using FullSDproject.Server.Data;
using FullSDproject.Server.IRepository;
using FullSDproject.Server.Models;
using FullSDproject.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FullSDproject.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Game> _games;
        private IGenericRepository<Copy> _Copies;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<User> _users;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<News> _news;
        private IGenericRepository<Stats> _stats;
        private IGenericRepository<Achievement> _achievements;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Game> Games
            => _games ??= new GenericRepository<Game>(_context);
        public IGenericRepository<Copy> Copies
            => _Copies ??= new GenericRepository<Copy>(_context);
        public IGenericRepository<Order> Orders
            => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<User> Users
            => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Payment> Payments
            => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<News> News
            => _news ??= new GenericRepository<News>(_context);
        public IGenericRepository<Stats> Stats
            => _stats ??= new GenericRepository<Stats>(_context);
        public IGenericRepository<Achievement> Achievements
            => _achievements ??= new GenericRepository<Achievement>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}