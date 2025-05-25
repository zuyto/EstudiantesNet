// <copyright file="UnitOfWorkEstudiantesDb.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Interfaces.Repository;
using EstudiantesNet.Application.Common.Interfaces.Repository.Estudiantes;
using EstudiantesNet.Application.Common.Interfaces.Repository.Materias;
using EstudiantesNet.Application.Common.Interfaces.Repository.Profesores;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Infrastructure.Context;
using EstudiantesNet.Infrastructure.Repositories.Estudiantes;
using EstudiantesNet.Infrastructure.Repositories.Materias;
using EstudiantesNet.Infrastructure.Repositories.Profesores;

namespace EstudiantesNet.Infrastructure.Repositories
{
	[ExcludeFromCodeCoverage]
	public class UnitOfWorkEstudiantesDb : IUnitOfWorkEstudiantesDb
	{
		private readonly ISerilogImplements _serilogImplements;
		private readonly EstudiantesNetDbContext _estudiantesNetDbContext;

		public UnitOfWorkEstudiantesDb(EstudiantesNetDbContext estudiantesNetDbContext, ISerilogImplements serilogImplements)
		{
			_serilogImplements = serilogImplements;
			_estudiantesNetDbContext = estudiantesNetDbContext;
		}

		public IEstudianteRepository EstudianteRepository => new EstudianteRepository(_estudiantesNetDbContext, _serilogImplements);
		public IMateriasRepository MateriasRepository => new MateriasRepository(_estudiantesNetDbContext, _serilogImplements);
		public IProfesoresRepository ProfesoresRepository => new ProfesoresRepository(_estudiantesNetDbContext, _serilogImplements);

		public void SaveChanges()
		{
			_estudiantesNetDbContext.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _estudiantesNetDbContext.SaveChangesAsync();
		}
	}
}
