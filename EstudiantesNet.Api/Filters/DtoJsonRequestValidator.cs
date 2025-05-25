// <copyright file="DtoJsonRequestValidator.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Models.DTOs;

using FluentValidation;

namespace EstudiantesNet.Api.Filters
{
	/// <summary>
	/// 
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class DtoJsonRequestValidator : AbstractValidator<DtoJsonRequest>
	{
		/// <summary>
		/// 
		/// </summary>
		public DtoJsonRequestValidator()
		{

			RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido.");
			RuleFor(x => x.MateriaIds)
				.NotNull()
				.Must(x => x.Count == 3).WithMessage("Debes seleccionar exactamente 3 materias.");

		}
	}
}
