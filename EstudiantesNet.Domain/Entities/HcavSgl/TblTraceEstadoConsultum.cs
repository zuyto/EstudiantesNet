// <copyright file="TblTraceEstadoConsultum.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace EstudiantesNet.Domain.Entities.HcavSgl
{
	[ExcludeFromCodeCoverage]
	public partial class TblTraceEstadoConsultum
	{
		public decimal IdTraceEstado { get; set; }

		public string? RefDocumento { get; set; }

		public string? Sticker { get; set; }

		public decimal? Apikey { get; set; }

		public decimal? IdParada { get; set; }

		public decimal? RefField1Num { get; set; }

		public decimal? RefField2Num { get; set; }

		public decimal? RefField3Num { get; set; }

		public string? RefField1Var { get; set; }

		public string? RefField2Var { get; set; }

		public string? RefField3Var { get; set; }

		public string? UsuarioCreacion { get; set; }

		public DateTime? FechaCreacion { get; set; }

		public string? UsuarioModificacion { get; set; }

		public DateTime? FechaModificacion { get; set; }
	}
}
