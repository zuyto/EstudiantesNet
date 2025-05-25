// <copyright file="LogErrorOptions.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Logger.Static;

using Microsoft.Extensions.Logging;

namespace EstudiantesNet.Logger.Models
{
	public class LogErrorOptions
	{
		public EventId EventId { get; set; }
		public Exception? Exception { get; set; }
		public string? Message { get; set; } = ConfigTypeMessage.LOGERGUION;
		public object?[]? Args { get; set; }
		public string MemberName { get; set; } = string.Empty;
		public string SourceFilePath { get; set; } = string.Empty;
		public int SourceLineNumber { get; set; }
	}
}
