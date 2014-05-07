using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 文件日志记录器
  /// </summary>
  public class TextFileLogger : TextFileLoggerBase
  {

    private string _filepath;


    public TextFileLogger( string logFilepath, Encoding encoding = null ) : this( null, logFilepath, encoding ) { }


    public TextFileLogger( ILogFilter filter, string logFilepath, Encoding encoding = null ) : base( filter, encoding )
    {

      if ( logFilepath == null )
        throw new ArgumentNullException( "logFilepath" );

      if ( !Path.IsPathRooted( logFilepath ) )
      {
        var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        logFilepath = Path.Combine( basePath, logFilepath );

      }

      _filepath = logFilepath;
    }



    protected override string GetFilepath( LogEntry entry )
    {
      return _filepath;
    }

  }
}
