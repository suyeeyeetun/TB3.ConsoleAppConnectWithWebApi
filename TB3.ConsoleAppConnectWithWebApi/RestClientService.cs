using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TB3.ConsoleAppConnectWithWebApi.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace TB3.ConsoleAppConnectWithWebApi;

public class RestClientService
{
    private readonly string _baseUrl = "https://localhost:7256";
    public async Task Read()
    {
        Console.WriteLine("Enter the page no. : ");
        int pageNo = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the page size : ");
        int pageSize = Convert.ToInt32(Console.ReadLine());
        //HttpClient client = new HttpClient();
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl+ "/api/Product/{pageNo}/{pageSize}",Method.Get);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var lst = JsonConvert.DeserializeObject<List<ProductDto>>(response.Content!);


            Console.WriteLine("Product List:");
            Console.WriteLine("------------------------------------------");
            foreach (var item in lst)
            {
                Console.WriteLine($"Product Name: {item.ProductName}");
                Console.WriteLine($"Product Price: {item.Price.ToString("n0")}");
                Console.WriteLine($"Product Quantity: {item.Quantity.ToString("n0")}");
                Console.WriteLine("------------------------------------------");

            }
        }
    }

    public async Task Create()
    {
        Console.Write("Please enter product name: ");
        string productName = Console.ReadLine();
        Console.Write("Please enter product quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine()); 
        Console.Write("Please enter product price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        ProductCreateRequestDto requestDto = new ProductCreateRequestDto()
        {
            ProductName = productName,
            Quantity = quantity,
            Price = price
        };//object to json

 
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl+ "/api/Product",Method.Post);
        request.AddJsonBody(requestDto);
        var response = await client.ExecuteAsync( request);
        var message = response.Content;
        Console.WriteLine(message);
    }
    public async Task Update()
    {
        Console.WriteLine("Please enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Please enter product name: ");
        string productName = Console.ReadLine();
        Console.Write("Please enter product quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());
        Console.Write("Please enter product price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());
        ProductUpdateRequestDto productUpdateRequestDto = new ProductUpdateRequestDto()
        {
            ProductName = productName,
            Quantity = quantity,
            Price = price
        };
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl + "/api/Productt/{productId}", Method.Put);
        request.AddJsonBody(productUpdateRequestDto);
        var response = await client.ExecuteAsync(request);
        var message = response.Content;
        Console.WriteLine(message);
    }

    public async Task Patch()
    {
        Console.WriteLine("Please enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Please enter product name: ");
        string productName = Console.ReadLine();

        Console.Write("Please enter price: ");
        string priceStr = Console.ReadLine();
        decimal price = string.IsNullOrEmpty(priceStr) ? 0 : Convert.ToDecimal(priceStr);

        Console.Write("Please enter quantity: ");
        string qtyStr = Console.ReadLine();
        int quantity = string.IsNullOrEmpty(qtyStr) ? 0 : Convert.ToInt32(qtyStr);

        ProductPatchRequestDto productPatchRequestDto = new ProductPatchRequestDto
        {
            ProductName = productName,
            Price = price,
            Quantity = quantity
        };
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl + "/api/Productt/{productId}", Method.Patch);
        request.AddJsonBody(productPatchRequestDto);
        var response = await client.ExecuteAsync(request);
        var message = response.Content;
        Console.WriteLine(message);

    }

    public async Task GetProduct()
    {
        Console.WriteLine("Please enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl + "/api/Productt/{productId}", Method.Get);
        var response = await client.ExecuteAsync(request);
        var message = response.Content;
        Console.WriteLine(message);
    }

    public async Task Delete()
    {
        Console.WriteLine("Please enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());

        RestClient client = new RestClient();
        RestRequest request = new RestRequest(_baseUrl + "/api/Productt/{productId}", Method.Delete);
        var response = await client.ExecuteAsync(request);
        var message = response.Content;
        Console.WriteLine(message);
    }
}
