using Microsoft.AspNetCore.Components;
using ProductTaskFrontEnd.Authentication;
using ProductTaskFrontEnd.Interceptor;
using ProductTaskFrontEnd.Service.Base;
using ProductTaskFrontEnd.Service.Category.Dto;
using ProductTaskFrontEnd.Service.Security;
using System.Net.Http.Json;

namespace ProductTaskFrontEnd.Service.Category;

public class CategoryService : BaseRepo, ICategoryService
{
    public CategoryService(HttpClient httpClient, ITokenRepository tokenRepo, NavigationManager navigationManager, CustomAuthenticationStateProvider myAuthenticationStateProvider, HttpInterceptorService interceptor) : base(httpClient, tokenRepo, navigationManager, myAuthenticationStateProvider, interceptor)
    {
    }
    readonly string BaseUrl = "Category";

    public async Task<GetResult<bool>> AddCategory(AddCategoryDto dto)
    {
        var result = new GetResult<bool>();
        
        await CheckToken();

        var data = await httpClient.PostAsJsonAsync($"{BaseUrl}/AddCategory", dto);
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

    public async Task<GetResult<bool>> DeleteCategory(Guid id)
    {
        var result = new GetResult<bool>();
        
        await CheckToken();

        var data = await httpClient.DeleteAsync($"{BaseUrl}/DeleteCategory?id={id}");
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

    public async Task<GetResult<List<GetCategoryDto>>> GetAllCategory()
    {
        var result = new GetResult<List<GetCategoryDto>>();


        var data = await httpClient.GetAsync($"{BaseUrl}/GetAllCategory");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<List<GetCategoryDto>>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<GetCategoryDto>> GetCategoryById(Guid Id)
    {
        var result = new GetResult<GetCategoryDto>();


        var data = await httpClient.GetAsync($"{BaseUrl}/GetCategoryById?Id={Id}");
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<GetCategoryDto>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<bool>> UpdateCategory(UpdateCategoryDto dto)
    {
        var result = new GetResult<bool>();

        await CheckToken();

        var data = await httpClient.PutAsJsonAsync($"{BaseUrl}/UpdateCategory", dto);
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
