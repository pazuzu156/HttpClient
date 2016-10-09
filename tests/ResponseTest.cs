using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Web.Script.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazuzu156.HttpClient;

namespace HttpClientTests
{
	[TestClass]
	public class ResponseTest
	{
		private Request _request;

		/// <summary>
		/// Class constructor (create our request)
		/// </summary>
		public ResponseTest()
		{
			this._request = Request.Create("http://cdn.kalebklein.com/kseupdater/test.json");
		}

		/// <summary>
		/// Testing the grabbing of response headers
		/// </summary>
		[TestMethod]
		public void ResponseTest_GetResponseHeaders()
		{
			var response = Response.Create(this._request);

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.IsNotNull(response.GetResponseHeaders());
		}

		/// <summary>
		/// Testing the ability to process headers
		/// </summary>
		[TestMethod]
		public void ResponseTest_ProcessResponseHeaders()
		{
			var response = Response.Create(this._request);
			var headers = response.GetResponseHeaders();

			var contentType = headers.Get(4);

			Assert.AreEqual(response.ConvertToContentType(contentType), Response.ContentType.Json);
		}

		[TestMethod]
		public void ResponseTest_GetBody()
		{
			var response = Response.Create(this._request);
			var body = response.GetResponseBody();

			// assert body is not empty
			Assert.IsNotNull(body);

			// parse body
			var json = new JavaScriptSerializer().Deserialize<JsonData>(body.ToString());

			// assert parsed body pieces are equal to given data
			Assert.AreEqual(json.name, "Kaleb Klein");
			Assert.AreEqual(json.age, 23);
		}
	}

	public class JsonData
	{
		public string name { get; set; }
		public int age { get; set; }
	}
}
