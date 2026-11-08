using System.Net.Http.Headers;
using BlazorWasmClient.Components.Auth;
using BlazorWasmClient.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using ProductService.Api.Queries.Dtos;

namespace BlazorWasmClient.Components.Products;

public class ProductsClient
{
    private readonly CustomAuthenticationStateProvider customAuthenticationStateProvider;
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    private readonly string productServiceUrl;

    public ProductsClient
    (
        AuthenticationStateProvider customAuthenticationStateProvider, 
        HttpClient httpClient,
        IConfiguration configuration
    )
    {
        this.customAuthenticationStateProvider = (CustomAuthenticationStateProvider)customAuthenticationStateProvider;
        this.httpClient = httpClient;
        this.baseUrl = configuration["Services:ApiGateway"];
        this.productServiceUrl = configuration["Services:Product"];
    }

    public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var token = await customAuthenticationStateProvider.GetToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var productsString = await GetStringWithFallback("/api/Products");

        return ApiResponse<IEnumerable<ProductDto>>.Success
        (
            JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(productsString)
        );
    }
    
    public async Task<ApiResponse<ProductDto>> GetProduct(string code)
    {
        var token = await customAuthenticationStateProvider.GetToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var productsString = await GetStringWithFallback($"/api/Products/{code}");

        return ApiResponse<ProductDto>.Success
        (
            JsonConvert.DeserializeObject<ProductDto>(productsString)
        );
    }

    private async Task<string> GetStringWithFallback(string path)
    {
        try
        {
            return await httpClient.GetStringAsync($"{baseUrl}{path}");
        }
        catch (HttpRequestException)
        {
            return await httpClient.GetStringAsync($"{productServiceUrl}{path}");
        }
    }
}
