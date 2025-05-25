// <copyright file="DependencyInjection.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Api.Filters;
using EstudiantesNet.Api.Middleware;
using EstudiantesNet.Application;
using EstudiantesNet.Infrastructure;

using Newtonsoft.Json.Serialization;

namespace EstudiantesNet.Api.DependecyInjectionGlobal
{
	/// <summary>
	/// Clase encargada de realizar la inyeccion de dependencias
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class DependencyInjection
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
		{

			builder?.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}).ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
			builder?.Services.AddControllersWithViews(option =>
			{
				option.Filters.Add(new ValidationFilter());
				option.Filters.Add(new FluentValidationFilter());
				////option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // Permite recibir el request en null
			});
			builder?.Services.ConfigureRateLimitiong();


			builder?.Services.AddProblemDetails();
			builder?.Services.AddHealthChecks();
			builder?.AddDbContext();
			builder?.AddInfrastructure();
			builder?.Services.AddApplication();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder?.Services.AddEndpointsApiExplorer();
			builder?.AddVersioning();
			builder?.Services.AddSwagger();

			builder?.Services.AddSwaggerGen();

			#region[CONFIGURACION SERVICIOS EXTERNOS HTTP/HTTPS]

			#endregion

			#region [HSTS]
			builder?.Services.AddHsts(options =>
			{
				options.ExcludedHosts.Clear();
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});
			#endregion


			return builder!;
		}

	}
}
