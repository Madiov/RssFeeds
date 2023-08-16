using System.Net;

namespace RSSFeeds.Services.Services
{
{
	public class DuplicateErrorHandle :Exception ,IServiceException
	{
		public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
		public string ErrorMessage => "Email Already Exists";
	}
}
