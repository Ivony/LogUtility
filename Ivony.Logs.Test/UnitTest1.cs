using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Ivony.Logs.Test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Hello()
    {


      var logger = new ConsoleLogger() + new TextFileLogger( @"C:\Temp\Logs\1.log" ) + new TextFileLogger( new DirectoryInfo( @"C:\Temp\Logs\Test" ) );

      logger.LogInfo( "Hello World!" );
      logger.LogInfo( "Hello World!" );
      logger.LogWarning( "Multiline\r\nLogs\r\n" );
      logger.LogError( "This has an error!" );
      logger.LogError( "This has an error!" );
      logger.LogError( "This has an error!" );
      try
      {
        throw new Exception( "Test exception!" );
      }
      catch ( Exception e )
      {

        logger.LogException( e );
      }

      //TextLogFileManager.Flush();

    }
  }
}
