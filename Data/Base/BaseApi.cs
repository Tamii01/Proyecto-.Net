﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Data.Base
{
	public class BaseApi : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public BaseApi(IHttpClientFactory httpClientFactory)
        {
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> PostToApi(string metodoController, object model)
		{
			var client = _httpClientFactory.CreateClient("useApi");

			var response = await client.PostAsJsonAsync(metodoController, model);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return Ok(content);
			}

			return BadRequest(response);
		}

        public async Task<IActionResult> GetToApi(string metodoController)
        {
            var client = _httpClientFactory.CreateClient("useApi");

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
