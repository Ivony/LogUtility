using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ivony.Logs.Test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Hello()
    {


      var logger = new ConsoleLogger();

      logger.LogInfo( "Hello World!" );

    }
  }
}
