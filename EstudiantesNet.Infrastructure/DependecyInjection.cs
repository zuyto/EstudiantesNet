// <copyright file="DependecyInjection.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Reflection;

using AspNetCoreRateLimit;

using EstudiantesNet.Application.Common.Interfaces.Repository;
using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Domain;
using EstudiantesNet.Infrastructure.Repositories;
using EstudiantesNet.Logger;
using EstudiantesNet.Logger.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstudiantesNet.Infrastructure
{
	public static class DependecyInjection
	{
		public static WebApplicationBuilder? AddInfrastructure(
					this WebApplicationBuilder builder)
		{
			var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

			#region[CORS]
			var ListOrigin = Environment.GetEnvironmentVariable(ConfigurationStruct.WithOrigins)
			?.Split(',').ToList();
			ListOrigin = ListOrigin == null ? new List<string>() : ListOrigin;

			builder.Services.AddCors(options =>
			{
				options.AddPolicy(ConfigurationStruct.CorsPolicy,
					builder => builder
						.AllowAnyMethod()
						.AllowAnyHeader()
						.WithOrigins(ListOrigin!.ToArray()));
			});
			#endregion

			#region[_LOGGER]
			builder.Services.AddLogger(new LoggerOptions
			{
				Meta = "v1.1",
				Autor = Environment.GetEnvironmentVariable(ConfigurationStruct.Celula)!,
				Area = Environment.GetEnvironmentVariable(ConfigurationStruct.Gerencia)!,
				Aplicacion = $"{assemblyName}",
				Proceso = $"{assemblyName}",
				Servicio = $"{assemblyName}.Api",
				Endpoint = $"openshift/{assemblyName}.Api"
			});
			#endregion


			builder.Services.Configure<AppSettings>(options => builder.Configuration.Bind(options));
			builder.Services.Configure<AppSettingsConfig>(builder.Configuration.GetSection("AppSettingsConfig"));


			#region[SERVICES]

			builder.Services.AddTransient<IUnitOfWorkDynamic, UnitOfWorkDynamic>();
			builder.Services.AddTransient<IUnitOfWorkEstudiantesDb, UnitOfWorkEstudiantesDb>();


			#endregion

			return builder;
		}



		public static void ConfigureRateLimitiong(this IServiceCollection services)
		{
			services.AddMemoryCache();
			services
				.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
			services.AddInMemoryRateLimiting();

			services.Configure<IpRateLimitOptions>(options =>
			{
				options.EnableEndpointRateLimiting = ConfigurationStruct.EnableEndpointRateLimiting;
				options.StackBlockedRequests = ConfigurationStruct.StackBlockedRequests;
				options.HttpStatusCode = 429;
				options.RealIpHeader = ConfigurationStruct.RealIpHeader;
				options.GeneralRules =
				[
					new() { Endpoint = ConfigurationStruct.Endpoint, Period = ConfigurationStruct.Period, Limit = 100000 }
				];
			});
		}
	}
}
