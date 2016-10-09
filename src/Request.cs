using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Pazuzu156.HttpClient
{
	public class Request
	{
		private HttpWebRequest _request;
		private Uri _url;
		private ContentType _contentType;
		private string _method;
		private int _timeout;

		/// <summary>
		/// Request content type
		/// </summary>
		public enum ContentType
		{
			Html,
			Json,
			Text,
			Null // nullable content type (this will throw errors for unsupported content type)
		}

		public bool IsTimedOut { get; private set; }

		/// <summary>
		/// Create a new Http Request
		/// </summary>
		/// <param name="url">URL of the request</param>
		/// <param name="method">HTTP request method</param>
		/// <param name="contentType">Request's content type</param>
		/// <param name="timeout">Request timeout in miliseconds</param>
		/// <returns></returns>
		public static Request Create(string url, string method="GET",
			ContentType contentType = ContentType.Html, int timeout = 5000)
		{
			return Request.Create(new Uri(url), method, contentType, timeout);
		}

		/// <summary>
		/// Create a new Http Request
		/// </summary>
		/// <param name="url">URL of the request</param>
		/// <param name="method">HTTP request method</param>
		/// <param name="contentType">Request's content type</param>
		/// <param name="timeout">Request timeout in miliseconds</param>
		/// <returns></returns>
		public static Request Create(Uri url, string method="GET",
			ContentType contentType = ContentType.Html, int timeout = 5000)
		{
			var self = new Request();
			self._url = url;
			self._method = method;
			self._contentType = contentType;
			self._timeout = timeout;

			self._generateHttpRequest();

			return self;
		}

		/// <summary>
		/// Gets the request stream (Only works with POST requests)
		/// </summary>
		/// <returns></returns>
		public Stream GetRequestStream()
		{
			if (this._method.ToLower() == "post")
				return this._request.GetRequestStream();
			else
				return null;
		}

		/// <summary>
		/// Gets the response from the request
		/// </summary>
		/// <returns></returns>
		public WebResponse GetResponse()
		{
			WebResponse response;

			try
			{
				response = this._request.GetResponse();
				this.IsTimedOut = false;
				return response;
			}
			catch
			{
				this.IsTimedOut = true;
				return null;
			}
		}

		private void _generateHttpRequest()
		{
			this._request = (HttpWebRequest)WebRequest.Create(this._url);
			this._request.ContentType = this._getContentType(this._contentType);
			this._request.Method = this._method;
			this._request.Timeout = this._timeout;
		}

		private string _getContentType(ContentType type)
		{
			switch(type)
			{
				case ContentType.Html:
					return "text/html";
				case ContentType.Json:
					return "application/json";
				case ContentType.Text:
					return "text/plain";
				default:
					return "text/plain";
			}
		}
	}
}
