// <copyright file="EstudianteMaterium.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Domain.Entities.EstudiantesNet;

public partial class EstudianteMaterium
{
	public int Id { get; set; }

	public int EstudianteId { get; set; }

	public int MateriaId { get; set; }

	public virtual Estudiante Estudiante { get; set; } = null!;

	public virtual Materia Materia { get; set; } = null!;
}
