using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazuzu156.HttpClient;

namespace HttpClientTests
{
	[TestClass]
	public class HttpStatusCheckingTest
	{
		/// <summary>
		/// Run a status check with string URL
		/// </summary>
		[TestMethod]
		public void StatusCheckTest_StringUrl()
		{
			var url = "http://google.com";
			var sc = new HttpStatus(url);
			Assert.IsTrue(sc.IsConnected);
		}

		/// <summary>
		/// Run a status check with Uri class object URL
		/// </summary>
		[TestMethod]
		public void StatusCheckTest_ClassUrl()
		{
			var url = new Uri("http://google.com");
			var sc = new HttpStatus(url);
			Assert.IsTrue(sc.IsConnected);
		}
	}
}
