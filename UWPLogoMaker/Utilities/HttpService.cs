﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UWPLogoMaker.Utilities
{
    public class HttpService
    {
        public static async Task<HttpResponseMessage> GetResponse(string url, Dictionary<string, string> headers = null, HttpMethod requestMethod = null,
            Dictionary<string, string> inputs = null, bool allowAutoRedirect = true)
        {
            List<KeyValuePair<string, string>> contents = null;

            if (inputs != null)
            {
                contents = inputs.ToList();

                foreach (var content in contents)
                {
                    Debug.WriteLine(content.ToString());
                }
            }

            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip |
                                         DecompressionMethods.Deflate |
                                         DecompressionMethods.None,
                AllowAutoRedirect = allowAutoRedirect
            };


            var httpClient = new HttpClient(handler);

            if (requestMethod == null)
            {
                requestMethod = HttpMethod.Get;
            }

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = requestMethod,
                RequestUri = new Uri(url, UriKind.Absolute)
            };

            if (contents != null)
            {
                requestMessage.Content = new FormUrlEncodedContent(contents);
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in headers)
                {
                    requestMessage.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
                    Debug.WriteLine("Headers: {0}:{1}", keyValuePair.Key, keyValuePair.Value);
                }
            }

            try
            {
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);

                return responseMessage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task<string> SendAsync(string url, Dictionary<string, string> headers = null, HttpMethod requestMethod = null,
            Dictionary<string, string> inputs = null, bool allowAutoRedirect = true)
        {
            var responseMessage = await GetResponse(url, headers, requestMethod, inputs, allowAutoRedirect);
            try
            {
                if (responseMessage == null) return null;
                if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.Found)
                {
                    string result = await responseMessage.Content.ReadAsStringAsync();

                    //Debug.WriteLine("HttpService.SendAsync\nAPI: {0}\nResult: {1}", url, result);

                    return result;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task<bool> GetHeadTask(string url, bool tryGetIfFailed = true)
        {
            //Check Uri
            Uri uriResult;
            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute) ||
                !Uri.TryCreate(url, UriKind.Absolute, out uriResult))
            {
                return false;
            }

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Head,
                RequestUri = uriResult
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (tryGetIfFailed)
                {
                    var response2 = await GetResponse(url);
                    return response2.StatusCode == HttpStatusCode.OK;
                }
                return false;
            }
            return true;
        }
     
    }
}
