using Core.Api.Tests.Mocks;
using Core.Api.Tests.Utils;
using Core.Api.Tests.Utils.V2;
using Core.ApiPipeline.ErrorHandling;
using Core.Contracts.Models;
using Core.Utils;
using FluentAssertions;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Core.Api.Tests.ContollerTests.V2
{
    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ReturnOkWithValidContent()
        {
            var httpResponse = await _client.GetAsync(TestConstValues.Uri);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var streamResponse = await httpResponse.Content.ReadAsStreamAsync();

            var products = await JsonSerializer.DeserializeAsync<Products>(streamResponse, JsonOptions.ReturnOptions());

            products.Items.Should().HaveCount(3);
            var productsSeed = SeedData.GetsProductsSeed();
            foreach (var product in productsSeed)
            {
                products.Items.Should().Contain(x => x.Id == product.Id
                                                     && x.Description == product.Description
                                                     && x.Name == product.Name
                                                     && x.Price == product.Price
                                                     && x.ImgUri == product.ImgUri);

            }
        }


        [Fact]
        public async Task GetProductById()
        {
            var httpResponse = await _client.GetAsync(Path.Combine(TestConstValues.Uri, IdGenerator.GenerateIdFromSeedData()));

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

            var product = await JsonSerializer.DeserializeAsync<Product>(responseStream, JsonOptions.ReturnOptions());

            product.Should().NotBeNull();
        }

        [Fact]
        public async Task GetProductById_NotFound()
        {
            var httpResponse = await _client.GetAsync(Path.Combine(TestConstValues.Uri, IdGenerator.GenerateIdForNotFound()));

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();

            var errorResponse = await JsonSerializer.DeserializeAsync<ErrorResponse>(responseStream, JsonOptions.ReturnOptions());
            errorResponse.Should().NotBeNull();
            errorResponse.Description.Should().Be(ErrorDescription.ProductNotFound);
            errorResponse.ErrorType.Should().Be(ErrorTypes.ProductNotFound);
            errorResponse.RequestId.Should().NotBeNull();

        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {
            var updatedProduct = new Product
            {
                Description = "Test Update Description"
            };

            var product = JsonSerializer.Serialize(updatedProduct);

            var requestContent = new StringContent(product, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync(Path.Combine(TestConstValues.Uri, IdGenerator.GenerateIdForNotFound()), requestContent);
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            var updatedProduct = new Product
            {
                Description = "Test Update Description"
            };

            var product = JsonSerializer.Serialize(updatedProduct);

            var requestContent = new StringContent(product, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PutAsync(Path.Combine(TestConstValues.Uri, IdGenerator.GenerateIdFromSeedData()), requestContent);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateProductBadRequest_LongerDescription()
        {
            var updatedProduct = new Product
            {
                Description = "zAsWfXfnNY9d4tEzkKR6HGXlt1NygAG2c9bfk5c6w0daEMB0KBNZHgcAzaaH1zgzs8KqtLos5vZ6JZ6o7jS0BcIpvNEOjWdVxGoDEqbt6TPGGiu2mvHtsWCxZ7Xb3pHUMHTxQ66EwykAT4abj72cckndKHssUCheMMrErfaFo46uHSVc24ITRlPz4UHXzEfgGx2taEC9h6toGqIcDZ6W8pCRUOlGYTZK0pZX2q7ectYry6DUtOOWF48NYwzBuXGig8UCOwq9yhg61CjILeXEHiZjNg0VGOnmZAuyzy64deG8XcCyRWT5ta059BSsaUtT9kh1g7yCIgXVWFxUydXbSXaXdQAV2Hbhjc0gIRFUYr2yC8vPoC70U2SfsYoUwnvLayg2i3xZqPh7hClSWByEplS53Cb53lbup0WwziJ4RhWv7HR25No4ewENBxBJOpTnE9M0QJbuoJNbSxIPyogFkwwKUH2uoWWvXqtiFId1fnLSLWjqbD3OZ"
            };

            var product = JsonSerializer.Serialize(updatedProduct);

            var requestContent = new StringContent(product, Encoding.UTF8, "application/json");


            var httpResponse = await _client.PutAsync(Path.Combine(TestConstValues.Uri, IdGenerator.GenerateIdFromSeedData()), requestContent);

            await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();


            var errorResponse = await JsonSerializer.DeserializeAsync<ValidationErrorResponse>(responseStream, JsonOptions.ReturnOptions());
            errorResponse.Should().NotBeNull();

            errorResponse.Descriptions.Should().NotBeNull();
            errorResponse.Descriptions.Should().HaveCount(1);
            errorResponse.ErrorType.Should().Be(ErrorTypes.ModelValidationFailure);
            errorResponse.RequestId.Should().NotBeNull();
        }
    }
}
