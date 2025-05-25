// <copyright file="ProfesoresController.cs" company="MAuro Martinez">
// 	Copyright (c).
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using EstudiantesNet.Api.Response;
using EstudiantesNet.Application.Common.Helpers;
using EstudiantesNet.Application.Common.Interfaces.Services;
using EstudiantesNet.Application.Common.Interfaces.Services.Serilog;
using EstudiantesNet.Application.Common.Models.DTOs;
using EstudiantesNet.Application.Common.Models.DTOs.DtoBase;
using EstudiantesNet.Application.Common.Static;
using EstudiantesNet.Application.Common.Struct;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace EstudiantesNet.Api.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProfesoresController : ControllerBase
	{
		private readonly ISerilogImplements _serilogImplements;
		private readonly IProfesorService _application;

		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="serilogImplements"></param>
		/// <param name="application"></param>
		public ProfesoresController(ISerilogImplements serilogImplements, IProfesorService application)
		{
			_serilogImplements = serilogImplements;
			_application = application;
		}


		/// <summary>
		/// Endpoint encargado de Procesar Promise Engine Contingencia
		/// </summary>
		/// <returns></returns>
		/// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("GetProfesorPorMateria")]
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseMateriaProfesor>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseMateriaProfesor>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseMateriaProfesor>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetProfesorPorMateria([FromBody] int idMateria)
		{
			ObjectResult result;
			string methodName = nameof(GetProfesorPorMateria);

			try
			{

				DtoGenericResponse<DtoJsonResponseMateriaProfesor> response = await _application.GetProfesorPorMateria(idMateria);


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseMateriaProfesor>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseMateriaProfesor>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}


				return result;

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Critical, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, string.Format(UserTypeMessages.ERRCATHCONTROLLER, methodName));
				return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<DtoErrorResponse>.CreateError(UserTypeMessages.ERROR_NO_CONTROLADO, ex.HandleExceptionMessage()));
			}
		}

	}
}
