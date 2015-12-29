using Ivony.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

      var logger = /*new ConsoleLogger() +*/ new TextFileLogger( @"C:\Temp\Logs\1.log" ) + new TextFileLogger( new DirectoryInfo( @"C:\Temp\Logs\Test" ) );



      var watch = new Stopwatch();

      watch.Restart();

      for ( int i = 0; i < 10000; i++ )
      {

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

      watch.Stop();
      Console.WriteLine( watch.Elapsed );



      TextLogFileManager.AutoFlush = false;
      watch.Restart();
      for ( int i = 0; i < 10000; i++ )
      {

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
      TextLogFileManager.Flush();
      watch.Stop();
      Console.WriteLine( watch.Elapsed );

      Console.ReadLine();
    }
  }
}
