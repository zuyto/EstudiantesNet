// <copyright file="ProfesorService.cs" company="MAuro Martinez">
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

namespace EstudiantesNet.Application.Services
{
	public class ProfesorService(ISerilogImplements serilogImplements, IUnitOfWorkEstudiantesDb unitOfWorkEstudiantesDb) : IProfesorService
	{
		private readonly ISerilogImplements _serilogImplements = serilogImplements;
		private readonly IUnitOfWorkEstudiantesDb _unitOfWorkEstudiantesDb = unitOfWorkEstudiantesDb;

		public async Task<DtoGenericResponse<DtoJsonResponseMateriaProfesor>> GetProfesorPorMateria(int idMateria)
		{
			List<DtoProfesor> materiaProfesor = await _unitOfWorkEstudiantesDb.ProfesoresRepository.GetProfesorPorMateria(idMateria);

			if (!materiaProfesor.Any())
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseMateriaProfesor>(false, null, UserTypeMessages.NO_DATOS);
			}

			return GenericHelpers.BuildResponse<DtoJsonResponseMateriaProfesor>(true, new DtoJsonResponseMateriaProfesor { MateriaProfesor = materiaProfesor }, UserTypeMessages.OKGEN01);
		}
	}
}

