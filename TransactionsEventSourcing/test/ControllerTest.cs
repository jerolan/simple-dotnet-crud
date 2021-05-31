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
      var repository = new InMemoryRepository();
      var request = new APIGatewayProxyRequest();
      var createdTransaction = new CreateTransaction(repository);
      var list = new List<Transaction>();

      list.Add(new Transaction()
      {
        Amount = 10
      });

      list.Add(new Transaction()
      {
        Amount = 20
      });

      foreach (var item in list)
      {
        createdTransaction.Handle(new CreateTransactionCommand()
        {
          Amount = item.Amount
        });
      }

      // act
      var response = new Controller(repository).GetTransactionsHandler(request);

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
      var request = new APIGatewayProxyRequest();
      var transaction = new Transaction() { Amount = 299 };
      request.Body = JsonConvert.SerializeObject(transaction);

      // act
      var response = new Controller().CreateTransactionsHandler(request);

      // asset
      var transactionResponse = JsonConvert.DeserializeObject<Transaction>(response.Body);
      Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.Created);
      Assert.AreEqual(transaction.Amount, transactionResponse.Amount);
    }
  }
}
