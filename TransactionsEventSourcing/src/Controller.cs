using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;
using Newtonsoft.Json;

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
      var list = new GetTransactionList(_repository).Handle();
      var response = new APIGatewayProxyResponse()
      {
        StatusCode = (int)HttpStatusCode.OK,
        Body = JsonConvert.SerializeObject(list),
      };

      return response;
    }

    public APIGatewayProxyResponse CreateTransactionsHandler(APIGatewayProxyRequest request)
    {
      var createdTransaction = new CreateTransaction(_repository).
        Handle(new CreateTransactionCommand()
        {
          Amount = 200
        });

      var response = new APIGatewayProxyResponse()
      {
        StatusCode = (int)HttpStatusCode.Created,
        Body = JsonConvert.SerializeObject(createdTransaction),
      };

      return response;
    }
  }
}
