using Amazon.Lambda.Core;
using System.Collections.Generic;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace AwsDotnetCsharp
{
  public class Controller
  {
    public List<Transaction> GetTransactionsHandler()
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

      return list;
    }
  }
}
