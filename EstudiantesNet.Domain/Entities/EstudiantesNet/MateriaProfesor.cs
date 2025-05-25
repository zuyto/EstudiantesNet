// <copyright file="MateriaProfesor.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Domain.Entities.EstudiantesNet;

public partial class MateriaProfesor
{
	public int Id { get; set; }

	public int MateriaId { get; set; }

	public int ProfesorId { get; set; }

	public virtual Materia Materia { get; set; } = null!;

	public virtual Profesore Profesor { get; set; } = null!;
}
