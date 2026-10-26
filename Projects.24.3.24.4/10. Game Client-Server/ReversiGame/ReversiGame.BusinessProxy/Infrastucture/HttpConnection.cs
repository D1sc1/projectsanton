using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReversiGame.BusinessProxy.Infrastucture
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient =>
            new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerConnection"]) };

        /// <summary>
        /// Читает ответ сервера. Сервер отвечает "null" или пустым телом, если объект не найден
        /// </summary>
        public static T ReadReply<T>(HttpResponseMessage response) where T : class
        {
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
