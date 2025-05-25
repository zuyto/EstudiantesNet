// <copyright file="DtoJsonResponseEstudiantes.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace EstudiantesNet.Application.Common.Models.DTOs
{
	[ExcludeFromCodeCoverage]
	public class DtoJsonResponseEstudiantes
	{
		public List<DtoEstudiante>? Estudiantes { get; set; }
	}
}
