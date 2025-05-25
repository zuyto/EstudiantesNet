// <copyright file="DynamicRepository.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

using EstudiantesNet.Application.Common.Interfaces.Repository;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Static;
using EstudiantesNet.Application.Common.Struct;
using EstudiantesNet.Infrastructure.Context;
using EstudiantesNet.Infrastructure.Extensions;

namespace EstudiantesNet.Infrastructure.Repositories
{
	[ExcludeFromCodeCoverage]
	internal class DynamicRepository : IDynamicRepository
	{
		private readonly DynamicContext context;
		private readonly ISerilogImplements _serilogImplements;

		public DynamicRepository(DynamicContext context, ISerilogImplements serilogImplements)
		{
			this.context = context;
			_serilogImplements = serilogImplements;
		}


		public async Task<List<object>> ExecuteSentenciaOnDatabase(string sentence,
			string secreto)
		{
			_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information,
				MetodosMessage.pingSecretConexion, sentence, null);
			var results = context.DynamicListFromSql(sentence,
				secreto.Decrypt(), new Dictionary<string, object>()).ToList();
			return await Task.FromResult(results);
		}

		public async Task<bool> TestConnectionDynamic(string secreto)
		{
			var results = context.TestConnectionDynamic(secreto.Decrypt());
			return await Task.FromResult(results);
		}
	}
}
