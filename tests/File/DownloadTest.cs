using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazuzu156.HttpClient.File;
using System.Diagnostics;

namespace HttpClientTests.File
{
	[TestClass]
	public class DownloadTest
	{
		[TestMethod]
		public void DownloadTest_GetFileContent_GivenFileName()
		{
			Download d = Download.Create("http://cdn.kalebklein.com/k2s.tar.gz", "k2s.tar.gz");
			d.DownloadProgressChanged += d_DownloadProgressChanged;
			d.DownloadFileCompleted += d_DownloadFileCompleted;
			d.DownloadFile();
		}

		[TestMethod]
		public void DownloadTest_GetFileContent_NoFileName()
		{
			Download d = Download.Create("http://cdn.kalebklein.com/k2s.tar.gz");
			d.DownloadProgressChanged += d_DownloadProgressChanged;
			d.DownloadFileCompleted += d_DownloadFileCompleted;
			d.DownloadFile();
		}

		void d_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			Debug.WriteLine("File download complete!");
		}

		void d_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
		{
			double bytesIn = double.Parse(e.BytesReceived.ToString());
			double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
			double percent = bytesIn / totalBytes * 100;
			string percentString = int.Parse(Math.Truncate(percent).ToString()) + "%";

			Debug.WriteLine(string.Format(
				"Bytes received: {0} out of {1}",
				bytesIn, totalBytes
			));
			Debug.WriteLine("Percent complete: " + percentString);
		}
	}
}
