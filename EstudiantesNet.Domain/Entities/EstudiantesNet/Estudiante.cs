// <copyright file="Estudiante.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Domain.Entities.EstudiantesNet;

public partial class Estudiante
{
	public int Id { get; set; }

	public string Nombre { get; set; } = null!;

	public string Email { get; set; } = null!;

	public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; } = new List<EstudianteMaterium>();
}
