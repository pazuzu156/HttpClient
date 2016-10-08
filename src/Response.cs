﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Pazuzu156.HttpClient
{
	public class Response
	{
		private HttpWebResponse _response;
		private Request _request;
		private Stream _requestStream;
		private Stream _responseStream;

		/// <summary>
		/// Response status description
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// Response status code
		/// </summary>
		public HttpStatusCode StatusCode { get; private set; }

		/// <summary>
		/// Creates a new Http Response using given request
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static Response Create(Request request)
		{
			var self = new Response();
			self._request = request;
			self._requestStream = self._request.GetRequestStream();

			self._generateHttpResponse();

			return self;
		}

		/// <summary>
		/// Gets the response body
		/// </summary>
		/// <param name="newline">Whether you want new line characters in the body</param>
		/// <returns></returns>
		public object GetResponseBody(bool newline=false)
		{
			using(var reader = new StreamReader(this._responseStream))
			{
				if(newline)
				{
					string line = "";
					StringBuilder sb = new StringBuilder();
					while((line = reader.ReadLine()) != null)
					{
						sb.Append(line + "\n");
					}
					return sb.ToString();
				}

				return reader.ReadToEnd();
			}
		}

		private void _generateHttpResponse()
		{
			this._response = (HttpWebResponse)this._request.GetResponse();
			this.Status = this._response.StatusDescription;
			this.StatusCode = this._response.StatusCode;
			this._responseStream = this._response.GetResponseStream();
		}
	}
}