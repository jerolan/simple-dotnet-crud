using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using TransactionsEventSourcing;
using System.Net;
using Newtonsoft.Json;

namespace TransactionsEventSourcing.Tests
{
  [TestClass]
  public class ControllerTest
  {
    [TestMethod]
    public void When_GetTransactions_Then_RespondWithAListOfTransactions()
    {
      // arrange
      APIGatewayProxyRequest request = new APIGatewayProxyRequest();

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

      // act
      var response = new Controller().GetTransactionsHandler(request);

      // asset
      var listResponse = JsonConvert.DeserializeObject<List<Transaction>>(response.Body);
      Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);

      for (var i = 0; i < list.Count; i++)
      {
        var target = listResponse[i];
        var source = list[i];
        Assert.AreEqual(target.Amount, source.Amount);
      }
    }


    [TestMethod]
    public void When_CreateTransactions_Then_RespondWithAListWithANewElement()
    {
      // arrange
      Controller controller = new Controller();
      APIGatewayProxyRequest request = new APIGatewayProxyRequest();

      var transaction = new Transaction()
      {
        Amount = 200
      };

      var response = controller.GetTransactionsHandler(request);

      // act
      request.Body = "{\"amount\":200}";
      response = controller.CreateTransactionsHandler(request);

      // asset
      var transactionResponse = JsonConvert.DeserializeObject<Transaction>(response.Body);
      Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.Created);
      Assert.AreEqual(transaction.Amount, transactionResponse.Amount);
    }
  }
}
