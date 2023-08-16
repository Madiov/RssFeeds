using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RSSFeeds.Controllers
{
	public class Errorcontroller : ControllerBase
	{
		[Route("/error")]
		public IActionResult Error()
		{
			var statusCode = 500;
			
			Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
			var message = exception.Message;
			switch (exception)
			{
				case IServiceException serviceException:
					statusCode = (int)serviceException.StatusCode;
					message = serviceException.ErrorMessage;
					break;
			}
			return Problem(statusCode : statusCode, title:message) ;
		}
	
	}
}
