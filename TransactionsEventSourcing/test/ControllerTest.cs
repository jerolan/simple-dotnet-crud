using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using TransactionsEventSourcing;

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
      APIGatewayProxyResponse response;

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
      response = new Controller().GetTransactionsHandler(request);

      // asset
      Assert.AreEqual(response.Body.ToString(), list.ToString());
    }
  }
}
