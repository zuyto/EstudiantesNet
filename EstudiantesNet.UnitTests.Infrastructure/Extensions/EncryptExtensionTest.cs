// <copyright file="EncryptExtensionTest.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Azure.Security.KeyVault.Secrets;

using EstudiantesNet.Application.Common.Exceptions;
using EstudiantesNet.Infrastructure.Extensions;

using Moq;

namespace EstudiantesNet.Infrastructure.UnitTest.Extensions
{
	public class EncryptExtensionTest
	{
		[Fact]
		public void Decrypt_ReturnsEmptyString_WhenSecretNameIsNullOrEmpty()
		{
			// Arrange
			string? secretName = null;

			// Act
			var result = EncryptExtension.Decrypt(secretName);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Decrypt_ThrowsGeneralException_WhenExceptionOccurs()
		{
			// Arrange
			var secretName = "invalidSecretName";
			var mockSecretClient = new Mock<SecretClient>();
			mockSecretClient.Setup(client => client.GetSecret(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
							.Throws(new Exception());

			// Act & Assert
			Assert.Throws<GeneralException>(() => EncryptExtension.Decrypt(secretName));
		}
	}
}
