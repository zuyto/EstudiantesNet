// <copyright file="DtoMateria.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace EstudiantesNet.Application.Common.Models.DTOs
{
	[ExcludeFromCodeCoverage]
	public class DtoMateria
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int Creditos { get; set; }
	}
}
