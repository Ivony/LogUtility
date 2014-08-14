using Ivony.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
  class Program
  {
    static void Main( string[] args )
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

    }
  }
}
