using FullSDproject.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDproject.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Game> Games { get; }
        IGenericRepository<Copy> Copies { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<News> News { get; }
        IGenericRepository<Stats> Stats { get; }
        IGenericRepository<Achievement> Achievements { get; }
    }
}