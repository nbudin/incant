using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Collections.Specialized;

namespace incant.HTTP
{
	public class Resource
	{
		public Uri URI { get; set; }
		public Dictionary<string, string> Headers { get; set; }
		
		public Resource (Uri uri, Dictionary<string, string> headers)
		{
			this.URI = uri;
			this.Headers = headers;
		}
		
		public Resource (Uri uri) : this(uri, new Dictionary<string, string>()) {}
		
		public Resource this[string path] {
			get {
				return new Resource(new Uri(URI, path), Headers);
			}
		}
		
		public WebResponse Request(String method, String path, Dictionary<string, string> queryParams, Dictionary<string, string> headers) {
			WebHeaderCollection requestHeaders = new WebHeaderCollection();
			foreach (string key in Headers.Keys) {
				if (headers.ContainsKey(key)) {
					continue;
				}
				
				requestHeaders[key] = Headers[key];
			}
			
			foreach (string key in headers.Keys) {
				requestHeaders[key] = headers[key];
			}
			
			Uri rawUri = new Uri(URI, path);
			
			NameValueCollection requestQuery = HttpUtility.ParseQueryString(rawUri.Query);
			foreach (string key in queryParams.Keys) {
				requestQuery[key] = queryParams[key];
			}
			
			Uri requestUri = new Uri(
				String.Concat(
					rawUri.Scheme,
					"://",
					rawUri.UserInfo,
					"@",
					rawUri.Host,
					":",
					rawUri.Port,
					rawUri.LocalPath,
					"?",
					requestQuery.ToString()
				)
			);
			
			WebRequest request = WebRequest.Create(requestUri);
			request.Method = method;	
			return request.GetResponse();
		}
	}
}

