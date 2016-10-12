using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using Pazuzu156.HttpClient;

namespace Pazuzu156.HttpClient.File
{
	public class Download
	{
		private string _url;
		private string _filename;
		private WebClient _client;

		/// <summary>
		/// DownloadProgressChanged event for WebClient
		/// </summary>
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		/// <summary>
		/// DownloadFileCompleted event for WebClient
		/// </summary>
		public event AsyncCompletedEventHandler DownloadFileCompleted;

		/// <summary>
		/// Creates new Download object
		/// </summary>
		/// <param name="url"></param>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Download Create(string url, string filename="")
		{
			var self = new Download();
			self._url = url;
			self._filename = (filename.Equals("")) ? self._parseFileName(url) : filename;
			System.Diagnostics.Debug.WriteLine(self._url);
			self._client = new WebClient();

			self._client.DownloadProgressChanged += self.DownloadProgressChanged;
			self._client.DownloadFileCompleted += self.DownloadFileCompleted;

			return self;
		}

		/// <summary>
		/// Asyncronously downloads a file
		/// </summary>
		public void DownloadFile()
		{
			var sc = new HttpStatus(this._url);
			if (sc.IsConnected)
				this._client.DownloadFileAsync(new Uri(this._url), this._filename);
			else
				throw new WebException(this._url + " could not be accessed!");
		}

		private string _parseFileName(string filename)
		{
			string[] fs = filename.Split('/');
			return fs[fs.Length - 1];
		}
	}
}
