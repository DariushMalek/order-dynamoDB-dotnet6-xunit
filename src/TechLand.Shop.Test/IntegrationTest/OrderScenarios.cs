using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TechLand.Shop.Model.Requests;
using TechLand.Shop.Model.Response;
using TechLand.Shop.Test.IntegrationTest.Base;
using Xunit;

namespace TechLand.Shop.Test.IntegrationTest;

public class OrderScenarios : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;

    private readonly HttpClient _client;

    private const string _baseUrl = "api/orders";

    public OrderScenarios(ApiFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
        var requestOrder = new RequestOrder()
        {
            OrderId = 1100,
            Lines = new List<RequestOrderLine>()
            {
                new() {ProductId = 201, Quantity = 1},
                new() {ProductId = 202, Quantity = 2},
                new() {ProductId = 203, Quantity = 1}
            },
            CustomerId = 510
        };

        await CreateAsync(requestOrder);

        var result = await GetAsync(requestOrder.OrderId);

        result.As<ResponseOrder>().OrderId.Should().Be(requestOrder.OrderId);
        result.As<ResponseOrder>().RequiredBinWidthInMillimeters.Should().Be(46.7);
    }

    protected async Task CreateAsync(RequestOrder createRequest)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl);
        request.Configure().WithJsonBody(createRequest);

        using var response = await _client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public async Task<ResponseOrder> GetAsync(int orderId)
    {
        var response = await _client.GetAsync($"{_baseUrl}/{orderId}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        return await response.ReadAsJson<ResponseOrder>();
    }
}