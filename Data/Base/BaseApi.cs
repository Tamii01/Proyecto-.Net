using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace Data.Base
{
	public class BaseApi : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public BaseApi(IHttpClientFactory httpClientFactory)
        {
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> PostToApi(string metodoController, object model, string token = "")
		{
			var client = _httpClientFactory.CreateClient("useApi");

			if(token != "")
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			}

			var response = await client.PostAsJsonAsync(metodoController, model);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(content);
			}

			return BadRequest(response);
		}

        public async Task<IActionResult> GetToApi(string metodoController, string token = "")
        {
            var client = _httpClientFactory.CreateClient("useApi");

            if (token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }


            var response = await client.GetAsync(metodoController);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return BadRequest(response);
        }


    }
}
