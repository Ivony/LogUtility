using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Ivony.Logs.Test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Hello()
    {

      var logs = new LogCollection();

      var logger = new ConsoleLogger() + new TextFileLogger( @"C:\Temp\Logs\1.log" ) + new TextFileLogger( new DirectoryInfo( @"C:\Temp\Logs\Test" ) ) + logs;

      logger.LogInfo( "Hello World!" );
      using ( LogScope.EnterScope( "Test" ) )
      {
        logger.LogInfo( "Hello World!" );
      }
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

      Assert.AreEqual( logs.Count(), 7 );

      Assert.AreEqual( logs.ElementAt( 0 ).LogType(), LogType.Info );
      Assert.AreEqual( logs.ElementAt( 1 ).LogType(), LogType.Info );
      Assert.AreEqual( logs.ElementAt( 2 ).LogType(), LogType.Warning );
      Assert.AreEqual( logs.ElementAt( 3 ).LogType(), LogType.Error );
      Assert.AreEqual( logs.ElementAt( 4 ).LogType(), LogType.Error );
      Assert.AreEqual( logs.ElementAt( 5 ).LogType(), LogType.Error );
      Assert.AreEqual( logs.ElementAt( 6 ).LogType(), LogType.Exception );


      Assert.AreEqual( logs.ElementAt( 0 ).MetaData.GetMetaData<LogScope>(), LogScope.RootScope );
      Assert.AreEqual( logs.ElementAt( 1 ).MetaData.GetMetaData<LogScope>().Name, "Test" );
      Assert.AreEqual( logs.ElementAt( 2 ).MetaData.GetMetaData<LogScope>(), LogScope.RootScope );

      //TextLogFileManager.Flush();

    }
  }
}
