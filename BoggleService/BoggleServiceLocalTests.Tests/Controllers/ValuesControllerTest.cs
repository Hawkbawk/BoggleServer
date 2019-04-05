
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoggleService;
using BoggleService.Controllers;
using BoggleService.Models;

namespace BoggleServiceLocalTests.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        //[TestMethod]
        //public void Get()
        //{
        //    // Arrange
        //    BoggleServiceController controller = new BoggleServiceController();

        //    // Act
        //    IEnumerable<string> result = controller.GetGameStatus();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count());
        //    Assert.AreEqual("value1", result.ElementAt(0));
        //    Assert.AreEqual("value2", result.ElementAt(1));
        //}


        [TestMethod]
        public void PostRegisterUser()
        {
            // Arrange
            BoggleServiceController controller = new BoggleServiceController();

            string UserToken = controller.PostMakeUser("joe");
            Assert.AreEqual(36, UserToken.Length);
        }


        //[TestMethod]
        //public void Put()
        //{
        //    BoggleServiceController controller = new BoggleServiceController();

        //    controller.Put(5, "value");

        //}

    }
}
