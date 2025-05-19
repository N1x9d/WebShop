using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop;

public partial class WebshopdbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<Category> Categories { get; set; }
    public WebshopdbContext()
    {
    }

    public WebshopdbContext(DbContextOptions<WebshopdbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=database;Port=5432;Database=webshopdb;Username=username;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
