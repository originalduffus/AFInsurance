using System.Net;

namespace API.Models
{
	public class RepositoryResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }

	}
}
