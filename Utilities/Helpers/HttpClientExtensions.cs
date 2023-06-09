﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeedTunes.Utilities
{
    public static class HttpClientExtensions
    {
        public async static Task<HttpResponseMessage> Post<T>(this HttpClient client, string url, T data, string contentType = "application/json")
        {
            var dataAsString = JsonConvert.SerializeObject(data);

            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return await client.PostAsync(url, content);
        }

        public async static Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(responseAsString);
        }
    }
}
