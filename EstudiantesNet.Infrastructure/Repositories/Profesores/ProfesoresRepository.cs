// <copyright file="ProfesoresRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Helpers;
using EstudiantesNet.Application.Common.Interfaces.Repository.Profesores;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace EstudiantesNet.Infrastructure.Repositories.Profesores
{
	[ExcludeFromCodeCoverage]
	public class ProfesoresRepository(EstudiantesNetDbContext estudiantesNetDbContext, ISerilogImplements serilogImplements) : IProfesoresRepository
	{
		public readonly EstudiantesNetDbContext _estudiantesNetDbContext = estudiantesNetDbContext;
		public readonly ISerilogImplements _serilogImplements = serilogImplements;

		public async Task<List<DtoProfesor>> GetProfesorPorMateria(int materiaId)
		{
			try
			{
				return await _estudiantesNetDbContext.MateriaProfesors.AsNoTracking().Where(mp => mp.MateriaId == materiaId).Select(mp => new DtoProfesor
				{
					Id = mp.Profesor.Id,
					Nombre = mp.Profesor.Nombre
				}).ToListAsync();

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Error, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, "\n\n **** CATCH ERROR ObtenerMaterias **** \n");
				throw;
			}
		}
	}
}
