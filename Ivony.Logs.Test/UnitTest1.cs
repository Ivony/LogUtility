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


      var logger = new ConsoleLogger() + new FileLogger( @"C:\Temp\Logs" );

      logger.LogInfo( "Hello World!" );

    }
  }
}
