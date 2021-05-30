using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Collections.Generic;
using System.Net;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace TransactionsEventSourcing
{
  public class Controller
  {
    public APIGatewayProxyResponse GetTransactionsHandler(APIGatewayProxyRequest request)
    {
      var list = new List<Transaction>();

      list.Add(
        new Transaction()
        {
          Amount = 10
        }
      );

      list.Add(
        new Transaction()
        {
          Amount = 20
        }
      );

      var response = new APIGatewayProxyResponse()
      {
        StatusCode = (int)HttpStatusCode.OK,
        Body = list.ToString(),
      };

      return response;
    }
  }
}
