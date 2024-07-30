using Microsoft.AspNetCore.Components;
using ProductTaskFrontEnd.Authentication;
using ProductTaskFrontEnd.Interceptor;
using ProductTaskFrontEnd.Service.Base;
using ProductTaskFrontEnd.Service.Prodeuct.Dto;
using ProductTaskFrontEnd.Service.Security;
using System.Net.Http.Json;

namespace ProductTaskFrontEnd.Service.Prodeuct;

public class ProductService : BaseRepo, IProductService
{
    public ProductService(HttpClient httpClient, ITokenRepository tokenRepo, NavigationManager navigationManager, CustomAuthenticationStateProvider myAuthenticationStateProvider, HttpInterceptorService interceptor) : base(httpClient, tokenRepo, navigationManager, myAuthenticationStateProvider, interceptor)
    {

    }

    readonly string BaseUrl = "Product";

    public async Task<GetResult<bool>> AddProduct(AddProduct dto)
    {
        var result = new GetResult<bool>();

        await CheckToken();

        var data = await httpClient.PostAsJsonAsync($"{BaseUrl}/AddProduct", dto);
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<bool>> DeleteProduct(Guid Id)
    {
        var result = new GetResult<bool>();

        await CheckToken();

        var data = await httpClient.DeleteAsync($"{BaseUrl}/DeleteProduct?id={Id}");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<List<GetProductsDto>>> GetAllProducts()
    {
        var result = new GetResult<List<GetProductsDto>>();


        var data = await httpClient.GetAsync($"{BaseUrl}/GetAllProducts");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<List<GetProductsDto>>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<List<GetProductsDto>>> GetAllProductsByCategoryId(Guid Id)
    {
        var result = new GetResult<List<GetProductsDto>>();


        var data = await httpClient.GetAsync($"{BaseUrl}/GetAllProductsByCategoryId?Id={Id}");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<List<GetProductsDto>>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<UpdateProduct>> GetProductById(Guid Id)
    {
        var result = new GetResult<UpdateProduct>();


        var data = await httpClient.GetAsync($"{BaseUrl}/GetProductById?Id={Id}");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<UpdateProduct>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<bool>> UpdateProduct(UpdateProduct dto)
    {
        var result = new GetResult<bool>();

        await CheckToken();

        var data = await httpClient.PutAsJsonAsync($"{BaseUrl}/UpdateProduct", dto);
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }
}
