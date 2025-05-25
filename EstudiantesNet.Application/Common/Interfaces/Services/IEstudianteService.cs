// <copyright file="IEstudianteService.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Models.DTOs.DtoBase;

namespace EstudiantesNet.Application.Common.Interfaces.Services
{
	public interface IEstudianteService
	{
		Task<DtoGenericResponse<DtoJsonResponseRegistrar>> ActualizarEstudianteAsync(DtoJsonRequest request);
		Task<DtoGenericResponse<DtoJsonResponseCompaneros>> ObtenerCompanerosPorEstudianteIdAsync(int request);
		Task<DtoGenericResponse<DtoJsonResponseEstudiandesId>> ObtenerEstudiantePorIdAsync(int request);
		Task<DtoGenericResponse<DtoJsonResponseEstudiantes>> ObtenerEstudiantesAsync();
		Task<DtoGenericResponse<DtoJsonResponseRegistrar>> RegistrarEstudianteAsync(DtoJsonRequest request);
	}
}
