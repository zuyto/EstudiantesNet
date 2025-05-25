// <copyright file="DependecyInjectionDbContext.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>


using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Infrastructure.Context;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Oracle.EntityFrameworkCore.Diagnostics;

namespace EstudiantesNet.Infrastructure
{
	/// <summary>
	/// Clase encargada de realziar a consiguracion de los DbContext
	/// </summary>
	///
	[ExcludeFromCodeCoverage]
	public static class DependecyInjectionDbContext
	{
		/// <summary>
		/// Metodo extension de WebApplicationBuilder (Program)
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
		{

			AddDynamicContext(builder);
			AddEstudiantesNetDbContext(builder);

			return builder!;
		}


		#region[DB DYMAMIC]
		private static void AddDynamicContext(WebApplicationBuilder builder)
		{
			builder?.Services.AddDbContext<DynamicContext>(options =>
			{
				ConfigureDbContextOptions(builder, options, ConfigurationStruct.PROD_SGL);
			}, ServiceLifetime.Scoped);
		}
		#endregion


		#region[DB ESTUDIANTES_DB]
		private static void AddEstudiantesNetDbContext(WebApplicationBuilder builder)
		{
			builder?.Services.AddDbContext<EstudiantesNetDbContext>(options =>
			{
				ConfigureDbContextOptions(builder, options, ConfigurationStruct.ESTUDIANTES_DB);
			}, ServiceLifetime.Scoped);
		}
		#endregion

		/// <summary>
		/// Metodo encargado de realizar la configuracion de los DbContext
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="options"></param>
		/// <param name="connectionStringName"></param>
		private static void ConfigureDbContextOptions(WebApplicationBuilder builder, DbContextOptionsBuilder options, string connectionStringName)
		{

			options.UseSqlServer(Environment.GetEnvironmentVariable(connectionStringName))
				.ConfigureWarnings(b => b.Ignore(OracleEventId.DecimalTypeKeyWarning));

			if (builder!.Environment.IsDevelopment()!)
			{
				// Configurar el nivel de registro
				options.EnableSensitiveDataLogging(); // Esto habilita la información sensible como parámetros de SQL
				options.LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name]); // Esto redirige los mensajes de registro a la consola
			}
		}
	}
}
