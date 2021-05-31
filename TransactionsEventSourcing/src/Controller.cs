using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Net;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace TransactionsEventSourcing
{
  public class Controller
  {
    private readonly IRepository<Transaction> _repository;

    public Controller()
    {
      _repository = new InMemoryRepository();
    }

    public APIGatewayProxyResponse GetTransactionsHandler(APIGatewayProxyRequest request)
    {
      var list = new GetTransactionList(_repository).Handle(new GetTransactionListQuery());
      var response = new APIGatewayProxyResponse()
      {
        StatusCode = (int)HttpStatusCode.OK,
        Body = list.ToString(),
      };

      return response;
    }
  }
}
