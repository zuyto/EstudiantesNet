// <copyright file="EstudianteRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using Azure.Core;

using EstudiantesNet.Application.Common.Helpers;
using EstudiantesNet.Application.Common.Interfaces.Repository.Estudiantes;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Domain.Entities.EstudiantesNet;
using EstudiantesNet.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace EstudiantesNet.Infrastructure.Repositories.Estudiantes
{
	[ExcludeFromCodeCoverage]
	public class EstudianteRepository(EstudiantesNetDbContext estudiantesNetDbContext, ISerilogImplements serilogImplements) : IEstudianteRepository
	{
		public readonly EstudiantesNetDbContext _estudiantesNetDbContext = estudiantesNetDbContext;
		public readonly ISerilogImplements _serilogImplements = serilogImplements;

		public async Task ActualizarEstudianteAsync(Estudiante estudiante)
		{
			try
			{
				var estudianteDb = await _estudiantesNetDbContext.Estudiantes.Include(e => e.EstudianteMateria).FirstOrDefaultAsync(e => e.Id == estudiante.Id);

				if (estudianteDb != null)
				{
					// Actualiza propiedades básicas
					estudianteDb.Nombre = estudiante.Nombre;
					estudianteDb.Email = estudiante.Email;

					// Elimina relaciones existentes
					_estudiantesNetDbContext.EstudianteMateria.RemoveRange(estudianteDb.EstudianteMateria);

					// Agrega nuevas relaciones
					estudianteDb.EstudianteMateria = estudiante.EstudianteMateria
						.Select(materiaId => new EstudianteMaterium
						{
							MateriaId = materiaId.MateriaId,
							EstudianteId = estudianteDb.Id
						}).ToList();

					await _estudiantesNetDbContext.SaveChangesAsync();
				}

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR InsertarEstudianteAsync **** \n");
				throw;
			}
		}

		public async Task GuardarMateriasEstudianteAsync(int estudianteId, List<int> materiaIds)
		{
			try
			{
				var relaciones = materiaIds.Select(id => new EstudianteMaterium
				{
					EstudianteId = estudianteId,
					MateriaId = id
				});

				await _estudiantesNetDbContext.EstudianteMateria.AddRangeAsync(relaciones);
				await _estudiantesNetDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR GuardarMateriasEstudianteAsync **** \n");
				throw;
			}
		}

		public async Task InsertarEstudianteAsync(Estudiante estudiante)
		{
			try
			{
				_estudiantesNetDbContext.Estudiantes.Add(estudiante);
				await _estudiantesNetDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR InsertarEstudianteAsync **** \n");
				throw;
			}
		}

		public async Task<List<string>> ObtenerCompanerosAsync(int estudianteId)
		{
			var materias = await _estudiantesNetDbContext.EstudianteMateria
									.Where(em => em.EstudianteId == estudianteId)
									.Select(em => em.MateriaId)
									.ToListAsync();

			return await _estudiantesNetDbContext.EstudianteMateria
							.Where(em => materias.Contains(em.MateriaId) && em.EstudianteId != estudianteId)
							.Select(em => em.Estudiante.Nombre)
							.Distinct()
							.ToListAsync();
		}

		public async Task<DtoEstudianteDetalle> ObtenerEstudiantePorIdAsync(int request)
		{
			try
			{
				return await _estudiantesNetDbContext.Estudiantes.Where(e => e.Id == request)
														.Select(e => new DtoEstudianteDetalle
														{
															Id = e.Id,
															Nombre = e.Nombre,
															Email = e.Email,
															MateriaIds = e.EstudianteMateria.Select(em => em.MateriaId)
																					.ToList()
														}).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR ObtenerEstudiantePorIdAsync **** \n");
				throw;
			}
		}

		public async Task<List<int>> ObtenerProfesoresPorMateriasAsync(List<int> materiaIds)
		{
			try
			{
				return await _estudiantesNetDbContext.MateriaProfesors.AsNoTracking()
								.Where(mp => materiaIds.Contains(mp.MateriaId))
								.Select(mp => mp.ProfesorId)
								.ToListAsync();

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR ObtenerProfesoresPorMateriasAsync **** \n");
				throw;
			}
		}

		public async Task<List<DtoEstudiante>> ObtenerTodosAsync()
		{
			return await _estudiantesNetDbContext.Estudiantes
			.Select(e => new DtoEstudiante
			{
				Id = e.Id,
				Nombre = e.Nombre,
				Email = e.Email
			}).ToListAsync();
		}
	}
}
