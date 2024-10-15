using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
	public class BuggyController : BaseApiController
	{
		[HttpGet("notfound")] //Get: /api/buggy/notfound
		public IActionResult GetNotFoundRequest()
		{
			//throw new NotFoundException();
			return NotFound(new ApiResponse(404)); //404

		}

		[HttpGet("servererror")] //Get: /api/buggy/servererror
		public IActionResult GetServerError()
		{
			
			throw new Exception();//500
		}


		[HttpGet("badrequest")] //Get: /api/buggy/badrequest
		public IActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400)); //400
		}

		[HttpGet("badrequest/{id}")] //Get: /api/buggy/badrequest/five
		public IActionResult GetValidationError(int id )
		{
			
			return Ok(); //400
		}


		[HttpGet("unautherized")] //Get: /api/buggy/unautherized
		public IActionResult GetUnautherizedError()
		{
			return Unauthorized(new ApiResponse(401)); //401
		}

		[HttpGet("forbidden")] //Get: /api/buggy/forbidden
		public IActionResult GetForbiddenRequest()
		{
			return Forbid();
		}
		[Authorize]
		[HttpGet("autherized")] //Get: /api/buggy/autherized
		public IActionResult GetAutherizedRequest()
		{
			return Ok(); //400
		}

	}
}
