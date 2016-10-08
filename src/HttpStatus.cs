using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Pazuzu156.HttpClient
{
	public class HttpStatus
	{
		/// <summary>
		/// Property used to determine whether or not a connection was established
		/// </summary>
		public bool IsConnected { get; private set; }

		private string _url;

		/// <summary>
		/// Creates new status check and performs the test
		/// </summary>
		/// <param name="url"></param>
		public HttpStatus(string url="http://google.com")
		{
			this._url = url;
			this._testConnection();
		}

		private void _testConnection()
		{
			var request = Request.Create(this._url, "GET", Request.ContentType.Html, 5000);
			var response = Response.Create(request);

			this.IsConnected = (response.StatusCode == HttpStatusCode.OK) ? true : false;
		}
	}
}
