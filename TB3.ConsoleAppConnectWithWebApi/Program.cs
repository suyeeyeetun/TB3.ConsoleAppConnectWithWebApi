// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using TB3.ConsoleAppConnectWithWebApi;
using TB3.ConsoleAppConnectWithWebApi.Dtos;

Console.WriteLine("Hello, World!");

Start:
Console.WriteLine("-- Welcome to Product API --");
Console.WriteLine("Choose Menu: 1-Read, 2-Create, 3-Update, 4-Patch, 5-Delete, 6-Get, 7-Exit");
int num = Convert.ToInt32(Console.ReadLine());
HttpClientService client = new HttpClientService();

switch (num)
{
    case 1:
        await client.Read();
        goto Start;
    case 2:
        await client.Create();
        goto Start;
    case 3:
        await client.Update();
        goto Start;
    case 4:
        await client.Patch();
        goto Start;
    case 5:
        await client.Delete();
        goto Start;
    case 6:
        await client.GetProduct();
        goto Start;
    case 7:
    default:
        Console.WriteLine("Exit...");
        goto Exit;

}

Exit:
Console.ReadLine();