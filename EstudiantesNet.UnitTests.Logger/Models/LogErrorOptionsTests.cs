// <copyright file="LogErrorOptionsTests.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Logger.Models;
using EstudiantesNet.Logger.Static;

namespace EstudiantesNet.UnitTests.Logger.Models
{
	public class LogErrorOptionsTests
	{
		[Fact]
		public void LogErrorOptions_PropertiesInitializedCorrectly()
		{
			// Arrange
			var logErrorOptions = new LogErrorOptions();

			// Act

			// Assert
			Assert.Equal(0, logErrorOptions.EventId.Id);
			Assert.Null(logErrorOptions.Exception);
			Assert.Equal(ConfigTypeMessage.LOGERGUION, logErrorOptions.Message);
			Assert.Null(logErrorOptions.Args);
			Assert.Equal(string.Empty, logErrorOptions.MemberName);
			Assert.Equal(string.Empty, logErrorOptions.SourceFilePath);
			Assert.Equal(0, logErrorOptions.SourceLineNumber);
		}
	}
}
