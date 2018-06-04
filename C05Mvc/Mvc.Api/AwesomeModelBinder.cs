using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mvc.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mvc.Api
{
    public class AwesomeModelBinder : IModelBinder
    {
        private const string SUBSCRIPTION_KEY = "42c5f708f02b4ff3829b63a860b38b33";
        private const string SUBSCRIPTION_LOCATION = "westeurope";

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            const string propertyName = "Photo";
            var valueProviderResult = bindingContext.ValueProvider.GetValue(propertyName);
            var base64Value = valueProviderResult.FirstValue;
            if (!string.IsNullOrEmpty(base64Value))
            {
                var bytes = Convert.FromBase64String(base64Value);
                var emotionResult = await GetEmotionResultAsync(bytes);
                var score = emotionResult.First().FaceAttributes.Emotion;
                var result = new EmotionalPhotoDto
                {
                    Contents = bytes,
                    Scores = score
                };
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            await Task.FromResult(Task.CompletedTask);
        }

        private static async Task<FaceDto[]> GetEmotionResultAsync(byte[] byteArray)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SUBSCRIPTION_KEY);
            var uri = $"https://{SUBSCRIPTION_LOCATION}.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceAttributes=gender,age,emotion";
            using (var content = new ByteArrayContent(byteArray))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<FaceDto[]>(responseContent);
                return result;
            }
        }
    }
}
