using System.Net;

namespace Fanap.MarginParkingPlus.Services.Shared
{
	public interface IServiceException
	{
		public HttpStatusCode StatusCode { get; }

		public string ErrorMessage { get;  }

	}
}
