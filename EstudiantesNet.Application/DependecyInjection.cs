// <copyright file="DependecyInjection.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using EstudiantesNet.Application.Common.Interfaces.Services;
using EstudiantesNet.Application.Common.Interfaces.Services.Http;
using EstudiantesNet.Application.Common.Interfaces.Services.Ping;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Profiles;
using EstudiantesNet.Application.Services;
using EstudiantesNet.Application.Services.Http;
using EstudiantesNet.Application.Services.Serilog;

using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

namespace EstudiantesNet.Application
{
	[ExcludeFromCodeCoverage]
	public static class DependecyInjection
	{
		public static IServiceCollection AddApplication(
			this IServiceCollection services)
		{
			_ = services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();

			_ = services.AddAutoMapper(
				Assembly.GetAssembly(typeof(MappingProfile)));

			services.AddTransient<ISerilogImplements, SerilogImplements>();
			services.AddTransient<IGenericServiceAgent, GenericServiceAgent>();
			services.AddTransient<IEstudianteService, EstudianteService>();
			services.AddTransient<IMateriaService, MateriaService>();
			services.AddTransient<IProfesorService, ProfesorService>();

			return services;
		}
	}
}
