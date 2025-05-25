// <copyright file="IProfesoresRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>


using EstudiantesNet.Application.Common.Models.DTOs;

namespace EstudiantesNet.Application.Common.Interfaces.Repository.Profesores
{
	public interface IProfesoresRepository
	{
		Task<List<DtoProfesor>> GetProfesorPorMateria(int materiaId);
	}
}
