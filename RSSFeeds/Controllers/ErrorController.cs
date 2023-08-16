using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RSSFeeds.Controllers
{
	public class Errorcontroller : ControllerBase
	{
		[Route("/error")]
		public IActionResult Error()
		{
			int statusCode = 500;
			string message = "Unexpected Error ";
			Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
			
			switch (exception)
			{
				case IServiceException serviceException:
					statusCode = (int)serviceException.StatusCode;
					message = exception.Message;
					break;

			}
			return Problem(statusCode : statusCode, title:message) ;
		}
	
	}
}
