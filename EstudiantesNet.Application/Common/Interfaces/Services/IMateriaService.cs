// <copyright file="IMateriaService.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>


using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Models.DTOs.DtoBase;

namespace EstudiantesNet.Application.Common.Interfaces.Services
{
	public interface IMateriaService
	{
		Task<DtoGenericResponse<DtoJsonResponseMaterias>> ObtenerMaterias();
	}
}
