using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.DTOs.Respponces;

namespace Application.Interfaces
{
    public interface IServiceStationDContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<Job> Jobs { get; set; }
        DbSet<Manager> Managers { get; set; }
        DbSet<Mechanic> Mechanics { get; set; }
        DbSet<MechanicsTasks> MechanicsTasks { get; set; }
        DbSet<Model> Models { get; set; }
        DbSet<OrderPart> OrderParts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Part> Parts { get; set; }
        DbSet<PartNeeded> PartsNeeded { get; set; }
        DbSet<Vendor> Vendors { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;


    }
}