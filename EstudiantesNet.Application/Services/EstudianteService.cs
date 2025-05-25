// <copyright file="EstudianteService.cs" company="MAuro Martinez">
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
	public class EstudianteService(ISerilogImplements serilogImplements, IUnitOfWorkEstudiantesDb unitOfWorkEstudiantesDb) : IEstudianteService
	{
		private readonly ISerilogImplements _serilogImplements = serilogImplements;
		private readonly IUnitOfWorkEstudiantesDb _unitOfWorkEstudiantesDb = unitOfWorkEstudiantesDb;

		public async Task<DtoGenericResponse<DtoJsonResponseRegistrar>> ActualizarEstudianteAsync(DtoJsonRequest request)
		{
			List<int> profesoresId = await _unitOfWorkEstudiantesDb.EstudianteRepository.ObtenerProfesoresPorMateriasAsync(request.MateriaIds);

			if (profesoresId.Distinct().Count() != 3)
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseRegistrar>(false, null, UserTypeMessages.MENSAJE_MATERIAS);
			}



			var estudiante = new Estudiante
			{
				Id = request.Id,
				Nombre = request.Nombre,
				Email = request.Email,
				EstudianteMateria = request.MateriaIds.Select(id => new EstudianteMaterium
				{
					MateriaId = id
				}).ToList()
			};

			await _unitOfWorkEstudiantesDb.EstudianteRepository.ActualizarEstudianteAsync(estudiante);
			//await _unitOfWorkEstudiantesDb.EstudianteRepository.GuardarMateriasEstudianteAsync(estudiante.Id, request.MateriaIds);

			return GenericHelpers.BuildResponse<DtoJsonResponseRegistrar>(true, new DtoJsonResponseRegistrar { Mensaje = UserTypeMessages.OKGEN01 }, UserTypeMessages.OKGEN01);
		}

		public async Task<DtoGenericResponse<DtoJsonResponseCompaneros>> ObtenerCompanerosPorEstudianteIdAsync(int request)
		{
			List<string> res = await _unitOfWorkEstudiantesDb.EstudianteRepository.ObtenerCompanerosAsync(request);

			if (!res.Any())
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseCompaneros>(false, null, UserTypeMessages.NO_DATOS);
			}
			return GenericHelpers.BuildResponse<DtoJsonResponseCompaneros>(true, new DtoJsonResponseCompaneros { NombresCompaneros = res }, UserTypeMessages.OKGEN01);
		}

		public async Task<DtoGenericResponse<DtoJsonResponseEstudiandesId>> ObtenerEstudiantePorIdAsync(int request)
		{
			DtoEstudianteDetalle res = await _unitOfWorkEstudiantesDb.EstudianteRepository.ObtenerEstudiantePorIdAsync(request);
			if (null == res)
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseEstudiandesId>(false, null, UserTypeMessages.NO_DATOS);
			}
			return GenericHelpers.BuildResponse<DtoJsonResponseEstudiandesId>(true, new DtoJsonResponseEstudiandesId { estudiante = res }, UserTypeMessages.OKGEN01);
		}

		public async Task<DtoGenericResponse<DtoJsonResponseEstudiantes>> ObtenerEstudiantesAsync()
		{

			List<DtoEstudiante> res = await _unitOfWorkEstudiantesDb.EstudianteRepository.ObtenerTodosAsync();

			if (!res.Any())
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseEstudiantes>(false, null, UserTypeMessages.NO_DATOS);
			}

			return GenericHelpers.BuildResponse<DtoJsonResponseEstudiantes>(true, new DtoJsonResponseEstudiantes { Estudiantes = res }, UserTypeMessages.OKGEN01);

		}

		public async Task<DtoGenericResponse<DtoJsonResponseRegistrar>> RegistrarEstudianteAsync(DtoJsonRequest request)
		{
			List<int> profesoresId = await _unitOfWorkEstudiantesDb.EstudianteRepository.ObtenerProfesoresPorMateriasAsync(request.MateriaIds);

			if (profesoresId.Distinct().Count() != 3)
			{
				return GenericHelpers.BuildResponse<DtoJsonResponseRegistrar>(false, null, UserTypeMessages.MENSAJE_MATERIAS);
			}


			var estudiante = new Estudiante
			{
				Nombre = request.Nombre,
				Email = request.Email
			};

			await _unitOfWorkEstudiantesDb.EstudianteRepository.InsertarEstudianteAsync(estudiante);
			await _unitOfWorkEstudiantesDb.EstudianteRepository.GuardarMateriasEstudianteAsync(estudiante.Id, request.MateriaIds);

			return GenericHelpers.BuildResponse<DtoJsonResponseRegistrar>(true, new DtoJsonResponseRegistrar { Mensaje = UserTypeMessages.OKGEN01 }, UserTypeMessages.OKGEN01);
		}



	}
}

