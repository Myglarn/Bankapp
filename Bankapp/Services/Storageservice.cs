using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bankapp.Services
{
    /// <summary>
    /// Service for local storage
    /// </summary>
    public class Storageservice : IStorageservice
    {
        private readonly IJSRuntime _jSRuntime;
        JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        public Storageservice(IJSRuntime jSRuntime) => _jSRuntime = jSRuntime;

        /// <summary>
        /// Serializes an object to local storage
        /// </summary>        
        public async Task SetItemAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await _jSRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        /// <summary>
        /// Deserializes an object from local storage
        /// </summary>        
        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
        }
    }
}
