
using Database.CatalogDb.EFCore.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Core.Api.Tests.ContollerTests
{
    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/products");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            using var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
            // Deserialize and examine results.
            var stgreamResponse = await httpResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductEntity>>(streamResponse, options);
            Assert.Contains(products, p => p.Name == "Jelly beans");
            Assert.Contains(products, p => p.Description == "TEST RECORD: Apple TV");
        }


        [Fact]
        public async Task GetProductById()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/products/2000000");

            // Must be not found.
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            httpResponse = await _client.GetAsync("/api/products/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.

            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var product = await JsonSerializer.DeserializeAsync<ProductEntity>(responseStream, options);

            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task UpdateProduct()
        {

            var updatedProduct = new ProductEntity
            {
                Description = "Test Update Description"
            };

            var company = JsonSerializer.Serialize(updatedProduct);

            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync("/api/products/2000000", requestContent);

            // Must be not found.
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            httpResponse = await _client.PutAsync("/api/products/2", requestContent);

            // Must be NoContent
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
        }

    }

}
