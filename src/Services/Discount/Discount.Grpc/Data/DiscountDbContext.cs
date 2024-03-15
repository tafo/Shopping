using Discount.Grpc.Domain;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public virtual DbSet<CouponEntity> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CouponEntity>().HasData(new List<CouponEntity>
        {
            new CouponEntity
            {
                Id = 1, 
                ProductName = "Samsung", 
                Description = "Phone", Amount = 3
            }
        });
    }
}