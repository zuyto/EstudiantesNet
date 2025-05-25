// <copyright file="IEstudiantesDbRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>



using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Domain.Entities.EstudiantesNet;

namespace EstudiantesNet.Application.Common.Interfaces.Repository.Estudiantes
{
	public interface IEstudianteRepository
	{
		Task ActualizarEstudianteAsync(Estudiante estudiante);
		Task GuardarMateriasEstudianteAsync(int estudianteId, List<int> materiaIds);
		Task InsertarEstudianteAsync(Estudiante estudiante);
		Task<List<string>> ObtenerCompanerosAsync(int estudianteId);
		Task<DtoEstudianteDetalle> ObtenerEstudiantePorIdAsync(int request);
		Task<List<int>> ObtenerProfesoresPorMateriasAsync(List<int> materiaIds);
		Task<List<DtoEstudiante>> ObtenerTodosAsync();
	}
}
