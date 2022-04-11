﻿using DivisionDeTablas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DivisionDeTablas
{
    public class ApplicationDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCore_Modulo8_Division;Integrated Security=True")
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
            modelBuilder.Entity<Estudiante>().HasOne(x => x.Detalle)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<EstudianteDetalle>(x => x.Id);

            modelBuilder.Entity<EstudianteDetalle>().ToTable("Estudiantes");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<EstudianteDetalle> EstudiantesDetalle { get; set; }
    }
}
