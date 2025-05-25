// <copyright file="EstudiantesNetDbContext.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Domain.Entities.EstudiantesNet;

using Microsoft.EntityFrameworkCore;

namespace EstudiantesNet.Infrastructure.Context;

public partial class EstudiantesNetDbContext : DbContext
{
	public EstudiantesNetDbContext()
	{
	}

	public EstudiantesNetDbContext(DbContextOptions<EstudiantesNetDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Estudiante> Estudiantes { get; set; }

	public virtual DbSet<EstudianteMaterium> EstudianteMateria { get; set; }

	public virtual DbSet<Materia> Materias { get; set; }

	public virtual DbSet<MateriaProfesor> MateriaProfesors { get; set; }

	public virtual DbSet<Profesore> Profesores { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Estudiante>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC07B5098224");

			entity.HasIndex(e => e.Nombre, "IX_Estudiantes_Nombre");

			entity.HasIndex(e => e.Email, "UQ__Estudian__A9D10534B8DD8C40").IsUnique();

			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.Nombre).HasMaxLength(100);
		});

		modelBuilder.Entity<EstudianteMaterium>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC078254B51D");

			entity.HasIndex(e => e.EstudianteId, "IX_EstudianteMateria_EstudianteId");

			entity.HasIndex(e => e.MateriaId, "IX_EstudianteMateria_MateriaId");

			entity.HasOne(d => d.Estudiante).WithMany(p => p.EstudianteMateria)
				.HasForeignKey(d => d.EstudianteId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Estudiant__Estud__4316F928");

			entity.HasOne(d => d.Materia).WithMany(p => p.EstudianteMateria)
				.HasForeignKey(d => d.MateriaId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Estudiant__Mater__440B1D61");
		});

		modelBuilder.Entity<Materia>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Materias__3214EC07B035C36E");

			entity.Property(e => e.Creditos).HasDefaultValue(3);
			entity.Property(e => e.Nombre).HasMaxLength(100);
		});

		modelBuilder.Entity<MateriaProfesor>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__MateriaP__3214EC07416BC679");

			entity.ToTable("MateriaProfesor");

			entity.HasIndex(e => e.MateriaId, "IX_MateriaProfesor_MateriaId");

			entity.HasIndex(e => e.ProfesorId, "IX_MateriaProfesor_ProfesorId");

			entity.HasOne(d => d.Materia).WithMany(p => p.MateriaProfesors)
				.HasForeignKey(d => d.MateriaId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__MateriaPr__Mater__3C69FB99");

			entity.HasOne(d => d.Profesor).WithMany(p => p.MateriaProfesors)
				.HasForeignKey(d => d.ProfesorId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__MateriaPr__Profe__3D5E1FD2");
		});

		modelBuilder.Entity<Profesore>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC0781B57301");

			entity.Property(e => e.Nombre).HasMaxLength(100);
		});


	}

}
