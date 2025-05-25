// <copyright file="Profesore.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Domain.Entities.EstudiantesNet;

public partial class Profesore
{
	public int Id { get; set; }

	public string Nombre { get; set; } = null!;

	public virtual ICollection<MateriaProfesor> MateriaProfesors { get; set; } = new List<MateriaProfesor>();
}
