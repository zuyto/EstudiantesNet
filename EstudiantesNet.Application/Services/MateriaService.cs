// <copyright file="MateriaService.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>


using EstudiantesNet.Application.Common.Helpers;
using EstudiantesNet.Application.Common.Interfaces.Repository;
using EstudiantesNet.Application.Common.Interfaces.Services;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Models.DTOs.DtoBase;
using EstudiantesNet.Application.Common.Static;
using EstudiantesNet.Domain.Entities.EstudiantesNet;

namespace EstudiantesNet.Application.Services
{
	public class MateriaService(ISerilogImplements serilogImplements, IUnitOfWorkEstudiantesDb unitOfWorkEstudiantesDb) : IMateriaService
	{
		private readonly ISerilogImplements _serilogImplements = serilogImplements;
		private readonly IUnitOfWorkEstudiantesDb _unitOfWorkEstudiantesDb = unitOfWorkEstudiantesDb;

		public async Task<DtoGenericResponse<DtoJsonResponseMaterias>> ObtenerMaterias()
		{
			List<DtoMateria> materias = await _unitOfWorkEstudiantesDb.MateriasRepository.ObtenerMaterias();

			if (!materias.Any())
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseMaterias>(false, null, UserTypeMessages.NO_DATOS);
			}

			return GenericHelpers.BuildResponse<DtoJsonResponseMaterias>(true, new DtoJsonResponseMaterias { Materias = materias }, UserTypeMessages.OKGEN01);
		}
	}
}

