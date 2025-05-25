// <copyright file="IMateriasRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>


using EstudiantesNet.Application.Common.Models.DTOs;

namespace EstudiantesNet.Application.Common.Interfaces.Repository.Materias
{
	public interface IMateriasRepository
	{
		Task<List<DtoMateria>> ObtenerMaterias();
	}
}
