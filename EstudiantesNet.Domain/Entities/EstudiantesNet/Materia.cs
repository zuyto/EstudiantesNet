// <copyright file="Materia.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Domain.Entities.EstudiantesNet;

public partial class Materia
{
	public int Id { get; set; }

	public string Nombre { get; set; } = null!;

	public int Creditos { get; set; }

	public virtual ICollection<EstudianteMaterium> EstudianteMateria { get; set; } = new List<EstudianteMaterium>();

	public virtual ICollection<MateriaProfesor> MateriaProfesors { get; set; } = new List<MateriaProfesor>();
}
