// <copyright file="MateriasRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Helpers;
using EstudiantesNet.Application.Common.Interfaces.Repository.Materias;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace EstudiantesNet.Infrastructure.Repositories.Materias
{
	[ExcludeFromCodeCoverage]
	public class MateriasRepository(EstudiantesNetDbContext estudiantesNetDbContext, ISerilogImplements serilogImplements) : IMateriasRepository
	{
		public readonly EstudiantesNetDbContext _estudiantesNetDbContext = estudiantesNetDbContext;
		public readonly ISerilogImplements _serilogImplements = serilogImplements;

		public async Task<List<DtoMateria>> ObtenerMaterias()
		{
			try
			{
				return await _estudiantesNetDbContext.Materias.AsNoTracking().Select(m => new DtoMateria
				{
					Id = m.Id,
					Nombre = m.Nombre,
					Creditos = m.Creditos

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
