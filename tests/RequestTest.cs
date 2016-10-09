using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazuzu156.HttpClient;

namespace HttpClientTests
{
	[TestClass]
	public class RequestTest
	{
		#region Test case methods
		[TestMethod]
		public void Test_RequestCreation_Base()
		{
			var request = Request.Create("http://cdn.kalebklein.com/request.php");
			
			// GET request | Attempt request stream gathering
			// This should always return null on GET request
			Assert.IsNull(request.GetRequestStream());

			// Get response from request (WebRequest)
			Assert.IsNotNull(request.GetResponse());
		}

		[TestMethod]
		public void Test_RequestCreation_Method()
		{
			// The request will be created twice to test GET/POST requests
			var getRequest = Request.Create("http://cdn.kalebklein.com/request.php", "GET");
			var postRequest = Request.Create("http://cdn.kalebklein.com/request.php", "POST");

			// Testing get request
			Assert.IsNull(getRequest.GetRequestStream());
			Assert.IsNotNull(getRequest.GetResponse());

			// Testing post request
			Assert.IsNotNull(postRequest.GetRequestStream());
			Assert.IsNotNull(postRequest.GetResponse());
		}

		[TestMethod]
		public void Test_RequestCreation_ContentType()
		{
			var request = Request.Create("http://cdn.kalebklein.com/request.php", contentType: Request.Headers.ContentType.Text);

			Assert.IsNull(request.GetRequestStream());
			Assert.IsNotNull(request.GetResponse());
		}

		[TestMethod]
		public void Test_RequestCreation_Timeout()
		{
			var request = Request.Create("http://cdn.kalebklein.com/request_timeout.php", timeout: 1000);

			// Forced timeout, so now let's assert the timeout was true!
			Assert.IsNull(request.GetResponse());
			Assert.IsTrue(request.IsTimedOut);
		}
		#endregion
	}
}
