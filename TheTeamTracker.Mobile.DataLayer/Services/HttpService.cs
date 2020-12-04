using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamTracker.Mobile.DataLayer.Services
{
	public class HttpService
	{
		private async static Task<Stream> sendRequest(string url, string method, string userAgent, Dictionary<string, string> headers, byte[] data = null, string contentType = null)
		{
			int tries = 3;
			while (tries >= 0)
			{
				try
				{
					var request = (HttpWebRequest)WebRequest.Create(url);
					request.UserAgent = userAgent;
					if (headers != null)
					{
						foreach (var header in headers)
						{
							request.Headers.Add(header.Key, header.Value);
						}
					}

					request.Method = method;
					request.ContentType = contentType;
					if (data != null)
					{
						request.ContentLength = data.Length;
						using (var stream = request.GetRequestStream())
						{
							stream.Write(data, 0, data.Length);
						}
					}

					var response = await request.GetResponseAsync();
					return response.GetResponseStream();
				}
				catch (Exception ex)
				{
					if ((ex.Message.Contains("Connection refused") || ex.Message.Contains("ReceiveFailure")) && tries > 0)
					{
						System.Threading.Thread.Sleep(1000);
						tries--;
						continue;
					}
					throw ex;
				}
			}
			return null;
		}

		private async static Task<string> sendRequestJSON(string url, string method, string userAgent, Dictionary<string, string> headers, byte[] data = null)
		{
			var stream = await sendRequest(url, method, userAgent, headers, data, "application/json");
			if (stream == null) return string.Empty;
			return new StreamReader(stream).ReadToEnd();
		}

		private async static Task<TOut> postPut<TIn, TOut>(string url, TIn obj, bool post, string userAgent, Dictionary<string, string> headers)
			where TOut : class
		{
			var jsonString = JsonConvert.SerializeObject(obj); //, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			var data = Encoding.UTF8.GetBytes(jsonString);
			var responseString = await sendRequestJSON(url, post ? "POST" : "PUT", userAgent, headers, data);
			return string.IsNullOrEmpty(responseString) ? null : JsonConvert.DeserializeObject<TOut>(responseString);
		}

		public static async Task<TOut> Post<TIn, TOut>(string url, TIn obj, string userAgent, Dictionary<string, string> headers = null)
			where TOut : class
		{
			return await postPut<TIn, TOut>(url, obj, true, userAgent, headers);
		}

		public static async Task<TOut> Put<TIn, TOut>(string url, TIn obj, string userAgent, Dictionary<string, string> headers = null)
			where TOut : class
		{
			return await postPut<TIn, TOut>(url, obj, false, userAgent, headers);
		}

		public static async Task<TOut> Get<TOut>(string url, string userAgent, Dictionary<string, string> headers = null)
			where TOut : class
		{
			var responseString = await sendRequestJSON(url, "GET", userAgent, headers);
			return string.IsNullOrEmpty(responseString) ? null : JsonConvert.DeserializeObject<TOut>(responseString);
		}

		public static async Task<Stream> Get(string url, string userAgent, Dictionary<string, string> headers = null, string contentType = null)
		{
			var responseStream = await sendRequest(url, "GET", userAgent, headers, null, contentType);
			return responseStream;
		}

		public static async Task Delete(string url, string userAgent, Dictionary<string, string> headers = null)
		{
			await sendRequestJSON(url, "DELETE", userAgent, headers);
		}

		public static async Task<TObject> SendFiles<TObject>(string url, List<FileToSend> filesToSend, bool put, Dictionary<string, string> headers = null)
		{
			var content = new MultipartFormDataContent();
			foreach (var f in filesToSend)
			{
				var imageContent = new ByteArrayContent(f.Bytes);
				imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(f.ContentType);
				content.Add(imageContent, "file", f.FileName);
			}

			var client = new HttpClient();
			if (headers != null)
			{
				foreach (var kvp in headers)
				{
					content.Headers.Add(kvp.Key, kvp.Value);
				}
			}
			HttpResponseMessage response;
			if (put)
				response = await client.PutAsync(new Uri(url), content);
			else
				response = await client.PostAsync(new Uri(url), content);

			var responseString = await response.Content.ReadAsStringAsync();
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception(responseString);
			}

			return JsonConvert.DeserializeObject<TObject>(responseString);
		}
	}

	public class FileToSend
	{
		public byte[] Bytes { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
	}
}
