﻿using EntidadesDePropiedad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesDePropiedad
{
    public class ApplicationDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore_Modulo8_EntidadDePropiedad;Integrated Security=True")
                .EnableSensitiveDataLogging(true)
                // descomenta la siguiente línea para utilizar Carga Perezosa
                //.UseLazyLoadingProxies()
                .UseLoggerFactory(MyLoggerFactory);
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
               .AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information)
               .AddConsole();
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().OwnsOne(x => x.Casa, ad =>
            {
                ad.Property(x => x.Calle).HasColumnName("Calle");
                ad.Property(x => x.Ciudad).HasColumnName("Ciudad");
            });
            modelBuilder.Entity<Estudiante>().OwnsOne(x => x.BillingAddress);
            modelBuilder.Entity<Contacto>().OwnsOne(x => x.Casa, ad =>
            {
                ad.Property(x => x.Calle).HasColumnName("Calle");
                ad.Property(x => x.Ciudad).HasColumnName("Ciudad");
            });


            modelBuilder.Entity<Estudiante>().HasData(new Estudiante() { Id = 1, Nombre = "Felipe" });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
    }
}
