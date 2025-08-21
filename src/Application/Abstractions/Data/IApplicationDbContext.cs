using Domain.Restaurants;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Restaurant> Restaurants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
