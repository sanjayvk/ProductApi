using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProductApi.Models;
using System.Net.Http.Json;

namespace ProductApi.Tests;

public class ProductApiIntegrationTests
{
    private HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [Test, Order(1)]
    public async Task CreateProduct_ShouldReturnCreatedProduct()
    {
        var product = new Product
        {
            Name = "IntegrationTest Product",
            Price = 50,
            Description = "Testing",
            StockAvailable = 10
        };

        var response = await _client.PostAsJsonAsync("/api/products", product);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Product>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProductId, Is.GreaterThan(0));
    }

    [Test, Order(2)]
    public async Task GetAllProducts_ShouldReturnList()
    {
        var response = await _client.GetAsync("/api/products");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.That(products, Is.Not.Null);
        Assert.That(products.Count, Is.GreaterThan(0));
    }

    [Test, Order(3)]
    public async Task GetProductById_ShouldReturnProduct()
    {
        var response = await _client.GetAsync("/api/products/100000");
        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Product>();
            Assert.That(product, Is.Not.Null);
            Assert.That(product.ProductId, Is.EqualTo(100000));
        }
    }

    [Test, Order(4)]
    public async Task UpdateProduct_ShouldReturnNoContent()
    {
        var product = new Product
        {
            ProductId = 100000,
            Name = "Updated Product",
            Price = 60,
            Description = "Updated Description",
            StockAvailable = 15
        };

        var response = await _client.PutAsJsonAsync($"/api/products/{product.ProductId}", product);
        Assert.That(response.IsSuccessStatusCode);
    }

    [Test, Order(5)]
    public async Task AdjustStock_Add_ShouldSucceed()
    {
        var response = await _client.PostAsync("/api/products/100000/adjuststock?quantity=5&add=true", null);
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test, Order(6)]
    public async Task AdjustStock_Remove_ShouldFailIfInsufficient()
    {
        var response = await _client.PostAsync("/api/products/100000/adjuststock?quantity=1000&add=false", null);
        Assert.That(response.IsSuccessStatusCode, Is.False);
    }

    [Test, Order(7)]
    public async Task DeleteProduct_ShouldReturnNoContent()
    {
        var response = await _client.DeleteAsync("/api/products/100000");
        Assert.That(response.IsSuccessStatusCode);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}