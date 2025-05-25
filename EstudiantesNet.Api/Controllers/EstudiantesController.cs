// <copyright file="EstudiantesController.cs" company="MAuro Martinez">
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
	public class EstudiantesController : ControllerBase
	{
		private readonly ISerilogImplements _serilogImplements;
		private readonly IEstudianteService _application;

		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="serilogImplements"></param>
		/// <param name="application"></param>
		public EstudiantesController(ISerilogImplements serilogImplements, IEstudianteService application)
		{
			_serilogImplements = serilogImplements;
			_application = application;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// /// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("Registrar")]
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Registrar([FromBody] DtoJsonRequest request)
		{
			ObjectResult result;
			string methodName = nameof(Registrar);

			try
			{
				if (request == null)
				{
					return StatusCode(StatusCodes.Status404NotFound, ApiResponse<DtoJsonResponseRegistrar>.CreateError(UserTypeMessages.ERROR_REQUEST, new DtoJsonResponseRegistrar()));
				}

				DtoGenericResponse<DtoJsonResponseRegistrar> response = await _application.RegistrarEstudianteAsync(request);


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseRegistrar>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseRegistrar>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}

				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information, JsonConvert.SerializeObject(result, Formatting.Indented), null, string.Format(UserTypeMessages.CONTROLLER_RESPONSE, methodName));

				return result;

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Critical, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, string.Format(UserTypeMessages.ERRCATHCONTROLLER, methodName));
				return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<DtoErrorResponse>.CreateError(UserTypeMessages.ERROR_NO_CONTROLADO, ex.HandleExceptionMessage()));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// /// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("Actualizar")]
		[HttpPut]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseRegistrar>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Actualizar([FromBody] DtoJsonRequest request)
		{
			ObjectResult result;
			string methodName = nameof(Actualizar);

			try
			{
				if (request == null)
				{
					return StatusCode(StatusCodes.Status404NotFound, ApiResponse<DtoJsonResponseRegistrar>.CreateError(UserTypeMessages.ERROR_REQUEST, new DtoJsonResponseRegistrar()));
				}

				DtoGenericResponse<DtoJsonResponseRegistrar> response = await _application.ActualizarEstudianteAsync(request);


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseRegistrar>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseRegistrar>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}

				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information, JsonConvert.SerializeObject(result, Formatting.Indented), null, string.Format(UserTypeMessages.CONTROLLER_RESPONSE, methodName));

				return result;

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Critical, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, string.Format(UserTypeMessages.ERRCATHCONTROLLER, methodName));
				return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<DtoErrorResponse>.CreateError(UserTypeMessages.ERROR_NO_CONTROLADO, ex.HandleExceptionMessage()));
			}
		}




		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// /// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("GetAllEstudiantes")]
		[HttpGet]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiantes>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiantes>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiantes>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAll()
		{
			ObjectResult result;
			string methodName = nameof(GetAll);

			try
			{


				DtoGenericResponse<DtoJsonResponseEstudiantes> response = await _application.ObtenerEstudiantesAsync();


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseEstudiantes>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseEstudiantes>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}

				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information, JsonConvert.SerializeObject(result, Formatting.Indented), null, string.Format(UserTypeMessages.CONTROLLER_RESPONSE, methodName));

				return result;

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Critical, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, string.Format(UserTypeMessages.ERRCATHCONTROLLER, methodName));
				return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<DtoErrorResponse>.CreateError(UserTypeMessages.ERROR_NO_CONTROLADO, ex.HandleExceptionMessage()));
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// /// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("GetCompaneros")]
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseCompaneros>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseCompaneros>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseCompaneros>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetCompaneros([FromBody] int request)
		{
			ObjectResult result;
			string methodName = nameof(GetCompaneros);

			try
			{
				DtoGenericResponse<DtoJsonResponseCompaneros> response = await _application.ObtenerCompanerosPorEstudianteIdAsync(request);


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseCompaneros>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseCompaneros>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}

				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information, JsonConvert.SerializeObject(result, Formatting.Indented), null, string.Format(UserTypeMessages.CONTROLLER_RESPONSE, methodName));

				return result;

			}
			catch (Exception ex)
			{
				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Critical, JsonConvert.SerializeObject(ex.HandleExceptionMessage(true), Formatting.Indented), null, string.Format(UserTypeMessages.ERRCATHCONTROLLER, methodName));
				return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<DtoErrorResponse>.CreateError(UserTypeMessages.ERROR_NO_CONTROLADO, ex.HandleExceptionMessage()));
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <response code="200">OK. Devuelve el objeto solicitado</response>
		/// <response code="409">Error durante el proceso</response>
		/// <response code="500">Error interno en el API</response>
		/// <response code="404">Error controlado cuando el Request es invalido</response>
		/// <response code="400">Error controlado por el flitro del request</response>
		[Route("EstudiantesPorId")]
		[HttpPost]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiandesId>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiandesId>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(ApiResponse<DtoErrorResponse>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(ApiResponse<DtoJsonResponseEstudiandesId>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetEstudiantesPorId([FromBody] int request)
		{
			ObjectResult result;
			string methodName = nameof(GetCompaneros);

			try
			{
				DtoGenericResponse<DtoJsonResponseEstudiandesId> response = await _application.ObtenerEstudiantePorIdAsync(request);


				if (!response.EsExitoso)
				{
					result = StatusCode(StatusCodes.Status409Conflict, ApiResponse<DtoJsonResponseEstudiandesId>.CreateUnsuccessful(response.Resultado!, response.Mensaje!));
				}
				else
				{
					result = Ok(ApiResponse<DtoJsonResponseEstudiandesId>.CreateSuccessful(response.Resultado!, response.Mensaje!));
				}

				_serilogImplements.ObtainMessageDefault(ConfigurationMessageType.Information, JsonConvert.SerializeObject(result, Formatting.Indented), null, string.Format(UserTypeMessages.CONTROLLER_RESPONSE, methodName));

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
