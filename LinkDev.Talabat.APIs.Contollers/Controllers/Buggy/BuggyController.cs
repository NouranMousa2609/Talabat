using LinkDev.Talabat.APIs.Controllers.Base;
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
			return NotFound(new { StatusCode = 404, Message="Not Found "}); //404

		}

		[HttpGet("servererror")] //Get: /api/buggy/servererror
		public IActionResult GetServerError()
		{
			throw new Exception();//500
		}


		[HttpGet("badrequest")] //Get: /api/buggy/badrequest
		public IActionResult GetBadRequest()
		{
			return BadRequest(new { StatusCode = 400, Message = "Bad Request " }); //400
		}

		[HttpGet("badrequest/{id}")] //Get: /api/buggy/badrequest/five
		public IActionResult GetValidationError()
		{
			return Ok(); //400
		}


		[HttpGet("unautherized")] //Get: /api/buggy/unautherized
		public IActionResult GetUnautherizedError()
		{
			return Unauthorized(new { StatusCode = 401, Message = "Unautherized " }); //401
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
