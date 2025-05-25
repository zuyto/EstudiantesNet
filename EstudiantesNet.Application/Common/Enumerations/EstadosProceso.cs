// <copyright file="EstadosProceso.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace EstudiantesNet.Application.Common.Enumerations
{
	public enum EstadosProceso
	{
		PendientePorProcesar = 1,
		EnProceso = 2,
		Enviado = 3,
		Error = 4,
		Descartado = 5
	}
}
