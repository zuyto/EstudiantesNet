// <copyright file="IUnitOfWorkEstudiantesDb.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Application.Common.Interfaces.Repository.Estudiantes;
using EstudiantesNet.Application.Common.Interfaces.Repository.Materias;
using EstudiantesNet.Application.Common.Interfaces.Repository.Profesores;

namespace EstudiantesNet.Application.Common.Interfaces.Repository
{
	public interface IUnitOfWorkEstudiantesDb
	{
		void SaveChanges();
		Task<int> SaveChangesAsync();

		#region[REPOSITORIES]
		public IEstudianteRepository EstudianteRepository { get; }
		public IMateriasRepository MateriasRepository { get; }
		public IProfesoresRepository ProfesoresRepository { get; }
		#endregion
	}
}
