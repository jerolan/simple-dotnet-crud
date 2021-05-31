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

    public Controller(InMemoryRepository repository = null)
    {
      _repository = repository ?? new InMemoryRepository();
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
      var transaction = JsonConvert.DeserializeObject<Transaction>(request.Body);
      var createdTransaction = new CreateTransaction(_repository).
        Handle(new CreateTransactionCommand()
        {
          Amount = transaction.Amount
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
